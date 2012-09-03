using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using DA.Dinners.Domain;
using DA.Dinners.Domain.Abstract;
using DA.Dinners.Domain.Concrete;
using DA.Dinners.Domain.Entities;
using DA.Dinners.Model;
using Mvc.Mailer;
using UI.Mailers;
using UI.Models;

namespace UI.Controllers.Api
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserMailer _mailer;
        private readonly IContinuousPropositionRepository _continuousPropositionRepository;
        private readonly IDayPropositionRepository _dayPropositionRepository;

        public OrdersController(IOrderRepository orderRepository, IPersonRepository personRepository, IProductRepository productRepository, IContinuousPropositionRepository continuousPropositionRepository, IDayPropositionRepository dayPropositionRepository, IUserMailer mailer)
        {
            this._orderRepository = orderRepository;
            this._personRepository = personRepository;
            this._productRepository = productRepository;
            this._continuousPropositionRepository = continuousPropositionRepository;
            this._dayPropositionRepository = dayPropositionRepository;
            this._mailer = mailer;
        }

        [AcceptVerbs("POST")]
        public void Create(Order order)
        {
            if (order == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            _orderRepository.InsertOrUpdateWithPerson(order, User.Identity.Name);
            _orderRepository.Save();
        }

        [AcceptVerbs("POST")]
        public void CreateFromAdmin(Order order)
        {
            if (order == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            _orderRepository.InsertOrUpdate(order);
            _orderRepository.Save();
        }

        //-----------------------------------------------------------------------------------------------------------

        [AcceptVerbs("POST")]
        public List<ReportModel> GetOrders(DateTime dateStart, DateTime dateEnd, string userName)
        {
            var all = _orderRepository.AllIncluding(o => o.OrderDetail, o => o.Statuses, o => o.Person).
                Where(o => o.Date >= dateStart && o.Date <= dateEnd).
                Where(o => o.Person.DomainName == userName);

            var result = new List<ReportModel>();
            foreach (var order in all)
            {
                var orderDetailsString = new StringBuilder();
                foreach (var detail in order.OrderDetail)
                    orderDetailsString.Append(string.Format("{0}, ", detail.Product.Title));

                var item = new ReportModel { UserName = order.Person.DomainName, DateOfDinner = order.Date, OrderDetails = orderDetailsString.ToString(), Price = order.OrderDetail.Sum(od => od.Product.Price * od.Quantity), UsersAmountOfMoney = order.Person.Balance };
                result.Add(item);
            }
            return result;
        }

        //-----------------------------------------------------------------------------------------------------------

        [AcceptVerbs("GET", "POST")]
        public string GetByDateForCurrentUser(DateTime date)
        {
            if (date == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            Person person = _personRepository.All.SingleOrDefault(p => p.DomainName.Equals(User.Identity.Name));

            Order order = _orderRepository.AllIncluding(or => or.OrderDetail).Where(o => o.Person.Id == person.Id).SingleOrDefault(o => o.Date.Year == date.Year && o.Date.Day == date.Day && o.Date.Month == date.Month);

            if (order == null)
                return null;

            return GetOrderJsonValue(order);
        }

        [AcceptVerbs("GET", "POST")]
        public string GetByDateAndPerson(DateTime date, int personId)
        {
            if (date == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            Person person = _personRepository.Find(personId);

            if (person == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            Order order = _orderRepository.AllIncluding(or => or.OrderDetail).Where(o => o.Person.Id == person.Id).SingleOrDefault(o => o.Date.Year == date.Year && o.Date.Day == date.Day && o.Date.Month == date.Month);

            if (order == null)
                return null;

            return GetOrderJsonValue(order);
        }

        [AcceptVerbs("GET", "POST")]
        public string Get(int orderId)
        {
            Order order = _orderRepository.Find(orderId);

            if (order == null)
                return null;

            return GetOrderJsonValue(order);
        }

        //-------------------------------------------------------------------------------

        [AcceptVerbs("GET", "POST")]
        public void Delete(int orderId)
        {
            _orderRepository.Delete(orderId);
            _orderRepository.Save();
        }

        [AcceptVerbs("GET", "POST")]
        public void SetDayStatus(DateTime date, int statusValue)
        {
            List<Order> allOrders = _orderRepository.AllIncluding(o => o.OrderDetail, o => o.Person, o => o.Statuses).Where(o => o.Date.Day == date.Day && o.Date.Month == date.Month && o.Date.Year == date.Year).ToList();
            foreach (var order in allOrders)
            {
                var singleOrDefault = order.Statuses.SingleOrDefault(s => s.isCurrent);
                if (singleOrDefault != null)
                    singleOrDefault.isCurrent = false;
                order.Statuses.Add(new OrderStatus { StatusValue = statusValue, Date = DateTime.Now, isCurrent = true });

                _orderRepository.InsertOrUpdate(order);
                if (statusValue == (int)OrderStatusValue.Payed)
                {
                    _orderRepository.UpdateAccountOperation(order);
                    if (order.Person.Balance < 0)
                    {
                        var userName = DomainService.Instance.GetEmail(order.Person.DomainName);
                        try
                        {
                            _mailer.NegativeBalance(userName, order.Person.Balance).SendAsync();
                        }
                        catch
                        {
                        }
                    }
                }
                else if (statusValue == (int)OrderStatusValue.SentOrder)
                {
                    var userName = DomainService.Instance.GetEmail(order.Person.DomainName);
                    try
                    {
                        _mailer.OrderSent(userName, order).SendAsync();
                    }
                    catch
                    {
                    }
                }
                _orderRepository.Save();
            }
        }

        [AcceptVerbs("GET", "POST")]
        public string GetDayStatus(DateTime date)
        {
            dynamic jsonDayStatus = new JsonObject();
            var orders = _orderRepository.AllIncluding(o => o.OrderDetail, o => o.Person, o => o.Statuses).Where(o => o.Date.Day == date.Day && o.Date.Month == date.Month && o.Date.Year == date.Year).ToList();
            int commonStatus = GetCommonStatusForOrders(orders);
            jsonDayStatus.Status = commonStatus;
            jsonDayStatus.AvaliableStatuses = new JsonArray();
            foreach (var status in GetAvaliableStatuses(commonStatus, date))
                jsonDayStatus.AvaliableStatuses.Add(status);
            return jsonDayStatus.ToString();
        }

        //---------------------------------------------------------------------

        [AcceptVerbs("GET")]
        public string GetDaysList(int start_date_day, int start_date_month, int start_date_year, int end_date_day, int end_date_month, int end_date_year)
        {
            DateTime startDate = new DateTime(start_date_year, start_date_month, start_date_day);
            DateTime endDate = new DateTime(end_date_year, end_date_month, end_date_day);

            dynamic days = new JsonArray();
            for (int i = 0; i < endDate.Subtract(startDate).Days; i++)
            {
                dynamic day = new JsonObject();
                var date = startDate.AddDays(i);
                day.Date = date;
                day.Orders = new JsonArray();

                List<Order> orders = _orderRepository.AllIncluding(o => o.OrderDetail, o => o.Person, o => o.Statuses).Where(o => o.Date.Day == date.Day && o.Date.Year == date.Year && o.Date.Month == date.Month).ToList();
                foreach (var order in orders)
                    day.Orders.Add(Order_(order));

                day.Status = new JsonObject();
                int commonStatus = GetCommonStatusForOrders(orders);
                day.Status.StatusValue = commonStatus;
                day.Status.AvalibleStatuses = new JsonArray();
                foreach (var status in GetAvaliableStatuses(commonStatus, date))
                    day.Status.AvalibleStatuses.Add(status);

                day.Proposition = new JsonObject();
                day.Proposition.Date = date;
                day.Proposition.Complexes = new JsonArray();
                day.Proposition.Also = new JsonArray();
                day.Proposition.EveryDay = new JsonArray();

                var dayProposition = _dayPropositionRepository.AllIncluding(dp => dp.Products).SingleOrDefault(o => o.Date.Day == date.Day && o.Date.Year == date.Year && o.Date.Month == date.Month);

                if (dayProposition != null)
                {
                    day.Proposition.Date = date;

                    var complexes = dayProposition.Products.Where(dp => dp.isComplex);
                    foreach (var product in complexes)
                    {
                        dynamic complex = new JsonObject();
                        complex.Price = product.Price;
                        complex.Title = product.Title;
                        complex.Summary = product.Summary;
                        complex.Id = product.Id;
                        day.Proposition.Complexes.Add(complex);
                    }
                    var alsos = dayProposition.Products.Where(dp => !dp.isComplex);
                    foreach (var product in alsos)
                    {
                        dynamic also = new JsonObject();
                        also.Price = product.Price;
                        also.Title = product.Title;
                        also.Summary = product.Summary;
                        also.Id = product.Id;
                        day.Proposition.Also.Add(also);
                    }
                }
                ContinuousProposition cp =
                    _continuousPropositionRepository.AllIncluding(cprop => cprop.DayPropositions, cprop => cprop.Products)
                        .SingleOrDefault(prop => prop.EndDate >= date && prop.StartDate <= date);
                if (cp != null)
                {
                    var everyDays = cp.Products;
                    foreach (var product in everyDays)
                    {
                        dynamic everyDay = new JsonObject();
                        everyDay.Price = product.Price;
                        everyDay.Title = product.Title;
                        everyDay.Summary = product.Summary;
                        everyDay.Id = product.Id;
                        day.Proposition.EveryDay.Add(everyDay);
                    }
                }
                days.Add(day);
            }
            return days.ToString();
        }

        //---------------------------------------------------------------------
        private IEnumerable<int> GetAvaliableStatuses(int commonStatus, DateTime orderDate)
        {
            var avaliableStatuses = new List<int> { };
            if (commonStatus == (int)DayOrderStatus.Delivered || commonStatus == (int)DayOrderStatus.NotDelivered)
            {
                avaliableStatuses.Add((int)DayOrderStatus.Payed);
                avaliableStatuses.Add((int)DayOrderStatus.Delivered);
                avaliableStatuses.Add((int)DayOrderStatus.NotDelivered);
            }
            else if (commonStatus == (int)DayOrderStatus.NotSent)
            {
                avaliableStatuses.Add((int)DayOrderStatus.NotSent);
                if (orderDate >= DateTime.Now)
                    avaliableStatuses.Add((int)DayOrderStatus.Sent);
            }
            else if (commonStatus == (int)DayOrderStatus.Sent)
            {
                avaliableStatuses.Add((int)DayOrderStatus.Sent);
                avaliableStatuses.Add((int)DayOrderStatus.Delivered);
                avaliableStatuses.Add((int)DayOrderStatus.NotDelivered);
                avaliableStatuses.Add((int)DayOrderStatus.Payed);
            }
            return avaliableStatuses;
        }

        private int GetCommonStatusForOrders(IEnumerable<Order> allOrders)
        {
            if (!allOrders.Any())
                return (int)DayOrderStatus.NoOrders;

            int currentFirst;
            try
            {
                var firstOrderStatus = allOrders.FirstOrDefault().Statuses.SingleOrDefault(s => s.isCurrent);
                currentFirst = firstOrderStatus.StatusValue;
                if (allOrders.Any(x => x.Statuses.SingleOrDefault(s => s.isCurrent).StatusValue != currentFirst))
                    return (int)DayOrderStatus.Undefined;
            }
            catch (NullReferenceException)
            {
                return (int)DayOrderStatus.Undefined;
            }
            return currentFirst;
        }

        private string GetOrderJsonValue(Order order)
        {
            return Order_(order).ToString();
        }

        private dynamic Order_(Order order)
        {
            dynamic dynamic_order = new JsonObject();
            dynamic_order.Id = order.Id;
            dynamic_order.Date = order.Date;
            dynamic_order.CurrentStatus = order.Statuses.SingleOrDefault(s => s.isCurrent).StatusValue;

            dynamic_order.Person = Person_(order.Person);

            dynamic details = new JsonArray();
            foreach (OrderDetail detail in order.OrderDetail)
            {
                dynamic json_detail = new JsonObject();
                json_detail.Id = detail.Id;
                json_detail.Quantity = detail.Quantity;
                dynamic product = new JsonObject();
                product.Id = detail.Product.Id;
                product.Price = detail.Product.Price;
                product.Summary = detail.Product.Summary;
                product.Title = detail.Product.Title;
                product.isComplex = detail.Product.isComplex;
                json_detail.Product = product;

                details.Add(json_detail);
            }
            dynamic_order.OrderDetail = details;

            dynamic_order.Statuses = new JsonArray();
            foreach (OrderStatus status in order.Statuses)
            {
                dynamic json_status = new JsonObject();
                json_status.Id = status.Id;
                json_status.Date = status.Date;
                json_status.Description = status.Description;
                json_status.isCurrent = status.isCurrent;
                json_status.StatusValue = status.StatusValue;

                dynamic_order.Statuses.Add(json_status);
            }
            return dynamic_order;
        }

        private dynamic Person_(Person person)
        {
            dynamic dynamicPerson = new JsonObject();
            dynamicPerson.Id = person.Id;
            dynamicPerson.DomainName = person.DomainName;
            dynamicPerson.Balance = person.Balance;
            dynamicPerson.FullName = person.GetName();
            return dynamicPerson;
        }
    }
}
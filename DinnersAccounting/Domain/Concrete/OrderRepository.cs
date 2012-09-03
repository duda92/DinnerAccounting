using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;

namespace DA.Dinners.Domain.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<Order> All
        {
            get { return context.Orders; }
        }

        public IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties)
        {
            IQueryable<Order> query = context.Orders;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Order Find(int id)
        {
            return context.Orders.Find(id);
        }

        public void InsertOrUpdateWithPerson(Order order, string domainName)
        {
            if (order.Id == default(int))
            {
                Person person = context.People.SingleOrDefault(p => p.DomainName == domainName);
                order.Person = person;

                foreach (var detail in order.OrderDetail)
                    detail.Product = context.Products.Find(detail.Product.Id);

                order.Statuses.Add(new OrderStatus { Date = DateTime.Now, isCurrent = true, StatusValue = (int)OrderStatusValue.Order });

                context.Orders.Add(order);
            }
            else
            {
                if (order.Person == null)
                {
                    Person person = context.People.SingleOrDefault(p => p.DomainName == domainName);
                    order.Person = person;
                }

                var tempOrder = context.Orders.Include(or => or.OrderDetail).SingleOrDefault(o => o.Id == order.Id);
                if (tempOrder != null) tempOrder.OrderDetail.Clear();
                context.SaveChanges();
                context.Entry(tempOrder).State = EntityState.Detached;

                foreach (var detail in order.OrderDetail)
                {
                    detail.Product = context.Products.Find(detail.Product.Id);
                    detail.Id = 0;
                    detail.Order = context.Orders.SingleOrDefault(o => o.Id == order.Id);
                    context.OrderDetails.Add(detail);
                }
            }
        }

        public void UpdateAccountOperation(Order order)
        {
            CreditOperation operation = context.CreditOperations.SingleOrDefault(o => o.Order.Id == order.Id);
            if (operation == null)
            {
                operation = new CreditOperation { Order = order };
                order.Person.Operations.Add(operation);
            }
            operation.Date = DateTime.Now;
            operation.Amount = order.OrderDetail.Sum(od => od.Product.Price * od.Quantity);
            order.Person.CalculateBalance();
            context.Entry(order.Person).State = EntityState.Modified;
        }

        public void InsertOrUpdate(Order order)
        {
            if (order.Id == default(int))
            {
                foreach (var detail in order.OrderDetail)
                    detail.Product = context.Products.Find(detail.Product.Id);

                order.Statuses.Add(new OrderStatus { Date = DateTime.Now, isCurrent = true, StatusValue = (int)OrderStatusValue.Order });
                context.Orders.Add(order);
            }
            else
            {
                foreach (var detail in order.OrderDetail)
                {
                    detail.Product = context.Products.Find(detail.Product.Id);
                    context.Entry(detail).State = EntityState.Modified;
                    if (detail.Id == default(int))
                        context.OrderDetails.Add(detail);
                }
                context.Entry(order).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var order = context.Orders.Find(id);
            if (order != null)
            {
                CreditOperation oper = context.CreditOperations.SingleOrDefault(o => o.Order.Id == id);
                if (oper != null)
                    context.CreditOperations.Remove(oper);
                for (int i = 0; i < order.OrderDetail.Count; i++)
                    context.OrderDetails.Remove(order.OrderDetail[i]);

                for (int i = 0; i < order.Statuses.Count; i++)
                    context.OrderStatus.Remove(order.Statuses[i]);

                context.Orders.Remove(order);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DA.Dinners.Domain;
using DA.Dinners.Domain.Abstract;

namespace UI.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class PeopleController : ApiController
    {
        private readonly IPersonRepository personRepository;

        public PeopleController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [AcceptVerbs("Get")]
        public void PerformAccountOperation(int personId, decimal amount, string summary)
        {
            Person person = personRepository.Find(personId);
            if (person == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            person.Operations.Add(new DebitOperation { Amount = amount, Date = DateTime.Now, Summary = summary });
            person.Balance += amount;
            personRepository.InsertOrUpdate(person);
            personRepository.Save();
        }

        [AcceptVerbs("Get")]
        public string GetBalance(int personId)
        {
            Person person = personRepository.Find(personId);
            if (person == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return GetJSONValue(person);
        }

        [AcceptVerbs("Get")]
        public string GetHistory(int personId)
        {
            Person person = personRepository.Find(personId);
            if (person == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return GetOperationsJSONValue(person.Operations);
        }

        private string GetJSONValue(Person person)
        {
            dynamic json = new JsonObject();
            json.Id = person.Id;
            json.Balance = person.Balance;
            json.DomainName = person.DomainName;
            return json.ToString();
        }

        private string GetOperationsJSONValue(IEnumerable<AccountOperation> operations)
        {
            dynamic json = new JsonArray();

            foreach (var operation in operations)
            {
                dynamic operation_item = new JsonObject();
                operation_item.Date = operation.Date;
                operation_item.Amount = operation.Amount;
                operation_item.Summary = operation.Summary;
                if (operation is CreditOperation)
                    operation_item.OrderId = ((CreditOperation)operation).Order.Id;
                json.Add(operation_item);
            }
            return json.ToString();
        }
    }
}
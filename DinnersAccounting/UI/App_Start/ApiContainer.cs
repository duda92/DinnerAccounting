using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using DA.Dinners.Domain.Concrete;
using UI.Controllers.Api;
using UI.Mailers;

namespace UI.App_Start
{
    internal class ApiContainer : IDependencyResolver
    {
        private IUserMailer _mailer = new UserMailer();

        public IDependencyScope BeginScope()
        {
            // This example does not support child scopes, so we simply return 'this'.
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(PropositionsController))
            {
                return new PropositionsController(new ContinuousPropositionRepository(), new DayPropositionRepository());
            }
            else if (serviceType == typeof(ProductsController))
            {
                return new ProductsController(new ProductRepository(), new PropositionRepository());
            }
            else if (serviceType == typeof(OrdersController))
            {
                return new OrdersController(new OrderRepository(), new PersonRepository(), new ProductRepository(), new ContinuousPropositionRepository(), new DayPropositionRepository(), new UserMailer());
            }
            else if (serviceType == typeof(PeopleController))
            {
                return new PeopleController(new PersonRepository());
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            // When BeginScope returns 'this', the Dispose method must be a no-op.
        }
    }
}
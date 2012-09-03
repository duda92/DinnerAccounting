using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using System.Web.Security;
using DA.Dinners.Domain.Concrete;
using UI.Controllers.Api;
using System.Web.Http;
using UI.App_Start;
using System.Data.Entity;
using DA.Dinners.Domain.Abstract;

namespace UI
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            UnityDependencyResolver dr = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new ApiContainer();
            DependencyResolver.SetResolver(dr);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>(); 


            container.RegisterType<ICreditOperationRepository, CreditOperationRepository>();
            container.RegisterType<IDebitOperationRepository,  DebitOperationRepository>();
            container.RegisterType<IOrderDetailRepository,     OrderDetailRepository>();
            container.RegisterType<IOrderRepository,           OrderRepository>();
            container.RegisterType<IOrderStatusRepository,     OrderStatusRepository>();
            container.RegisterType<IPersonRepository,          PersonRepository>();
            container.RegisterType<IRoleRepository,            RoleRepository>();
            container.RegisterType<IContinuousPropositionRepository, ContinuousPropositionRepository>();
            container.RegisterType<IDayPropositionRepository, DayPropositionRepository>();
            container.RegisterType<IPropositionRepository, PropositionRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            
            return container;
        }
    }
}
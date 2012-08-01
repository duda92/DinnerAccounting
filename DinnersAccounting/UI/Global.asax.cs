using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using DataModel.Abstract;
using DataModel.Concrete;

namespace UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        private IKernel _kernel = new StandardKernel();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mock<IProductRepository> products_mock = new Mock<IProductRepository>();

            _kernel.Bind<IProductRepository>().To<FakeProductRepository>().InSingletonScope();
            

        }
    }
}
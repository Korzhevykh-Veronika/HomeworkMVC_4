using Homework_mvc.Entities;
using Homework_mvc.Infrastructure.Data;
using Homework_mvc.Util;
using Ninject.Modules;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using NinjectDependencyResolver = Homework_mvc.Util.NinjectDependencyResolver;

namespace Homework_mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new BlogDbInitializer());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<BlogContext>());
           
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

        }
    }
}

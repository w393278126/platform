﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Xn.Platform.Infrastructure.Web;

namespace Xn.Platform.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CustomInitialize();
        }

        private void CustomInitialize()
        {
            Domain.Impl.AutoMappers.Configuration.Configure();
            DependencyInjectionConfig.Register(Assembly.GetExecutingAssembly());
        }
    }
}

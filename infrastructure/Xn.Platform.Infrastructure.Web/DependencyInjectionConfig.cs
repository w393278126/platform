using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace Xn.Platform.Infrastructure.Web
{
    public static class DependencyInjectionConfig
    {
        public static void Register(Assembly assembly)
        {
            var builder = new ContainerBuilder();
            var assemblies = assembly.GetReferencedAssemblies().Where(a => a.Name.StartsWith("Xn", StringComparison.OrdinalIgnoreCase)).Select(Assembly.Load).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => t.Name.EndsWith("Handler") || 
                               t.Name.EndsWith("Model") || 
                               t.Name.EndsWith("Repository") || 
                               t.Name.EndsWith("Service"))
                   .AsSelf().AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterControllers(assembly).PropertiesAutowired();
            builder.RegisterApiControllers(assembly).PropertiesAutowired();
            var container = builder.Build();

            // MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // WebAPI
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

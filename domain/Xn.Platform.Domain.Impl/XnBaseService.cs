using Autofac.Integration.Mvc;
using Logging.Client;
using Xn.Platform.Domain.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Xn.Platform.Infrastructure.Web
{
    public class XnBaseService 
    {
        protected T GetService<T>()
        {
            return AutofacDependencyResolver.Current.GetService<T>();
        }

        protected IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>)AutofacDependencyResolver.Current.GetServices<T>();
        }
    }
}
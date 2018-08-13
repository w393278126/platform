using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.Mvc;

namespace Plu.Platform.Domain.Impl
{
    public class BaseService
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;


namespace Xn.Platform.Infrastructure.Web
{
    public sealed class XnControllerFactory : DefaultControllerFactory, IControllerFactory
    {

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {

                return GetControllerInstance(requestContext, typeof(NotFindController));
            }
            else
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }

        class NotFindController : Controller
        {
            static readonly string NOT_FIND_CONTROLLER_FORMAT = "Not Find The Resource:\"{0}\"";
            protected override void HandleUnknownAction(string actionName)
            {
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                this.Response.StatusCode = 404;
                this.Response.StatusDescription = string.Format(NOT_FIND_CONTROLLER_FORMAT, controllerName + "/" + actionName);
                this.Response.Write(" Illegal Request. " + this.Response.StatusDescription);
                this.Response.End();
            }

        }
    }
}
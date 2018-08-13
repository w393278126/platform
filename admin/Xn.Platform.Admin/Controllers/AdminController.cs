using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using Xn.Platform.Extensions;
using Xn.Platform.Core.Extensions;
using Xn.Platform.Infrastructure.Web;
using Xn.Platform.Domain.Admin;
using Xn.Platform.Abstractions.Domain;
using Plu.Platform.Domain.Impl.Admin;

namespace Xn.Platform.Admin.Controllers
{
    public class AdminController : XnBaseController
    {
        private static LoginService loginService = new LoginService();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Loging(LoginModel admin)
        {
            try
            {
                if (string.IsNullOrEmpty(admin.UserName) || string.IsNullOrEmpty(admin.Password))
                {
                    return Json(Result.Error(ResultCode.ParameterError));
                }
                return Json(loginService.Loging(admin));
            }
            catch (Exception ex)
            {
                return Json(Result.Error(ResultCode.DefaultError));
            }
        }
    }

}

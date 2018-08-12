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

namespace Xn.Platform.Admin.Controllers
{
    public class LoginController : XnBaseController
    {

        public LoginController()
        {
        }
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
                return Json("");
            }
            catch (Exception ex)
            {
                return Json(Result.Error(ResultCode.DefaultError));
            }
        }
    }

}

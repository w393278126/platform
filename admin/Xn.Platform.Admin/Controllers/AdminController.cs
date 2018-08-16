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
using Xn.Platform.Domain.Impl.Admin;
using Xn.Platform.Presentation.Admin.Models;

namespace Xn.Platform.Admin.Controllers
{
    public class AdminController : XnBaseController
    {
        private static AdminService adminService = new AdminService();
        private static ValidateCodeService validateCodeService = new ValidateCodeService();
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
                if (string.IsNullOrEmpty(admin.UserName) || string.IsNullOrEmpty(admin.Password)|| string.IsNullOrEmpty(admin.Code))
                {
                    return Json(Result.Error(ResultCode.ParameterError));
                }
                return Json(adminService.Loging(admin));
            }
            catch (Exception ex)
            {
                return Json(Result.Error(ResultCode.DefaultError));
            }
        }

        [AllowAnonymous]
        public ActionResult GetValidateCode()
        {
            var bytes = validateCodeService.GetValidateCode();
            return File(bytes, @"image/jpeg");
        }


        [AdministratorActionFilterAttribute]
        public ActionResult Add(AdminUserModel admin)
        {
            try
            {
                if (string.IsNullOrEmpty(admin.UserName) || string.IsNullOrEmpty(admin.PassWord)|| admin.Role<=0)
                {
                    return Json(Result.Error(ResultCode.ParameterError));
                }
                return Json(adminService.Add(admin));
            }
            catch (Exception ex)
            {
                return Json(Result.Error(ResultCode.DefaultError));
            }
        }
    }

}

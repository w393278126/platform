using Xn.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xn.Platform.Abstractions.Domain;
using Plu.Platform.Domain.Impl.TourUser;
using Xn.Platform.Presentation.Admin.Models;

namespace Xn.Platform.Admin.Controllers
{
    //[AdministratorActionFilterAttribute]
    public class TourUserController : Controller
    {
        private static TourUserService tourUserService = new TourUserService();
        // GET: TourUsers
        public ActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            var userName = Request.Params["username"];
            var result = tourUserService.PageList(new TourUserRequest.PageResult
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                username = userName,
                OrderBy = "modify_time"
            });
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.UserName = userName;
            return View(result.Data);
        }

        //GET:
        public ActionResult Edit()
        {
            var Id = Request.Params["Id"];
            TourUserModel model = new TourUserModel();
            ///编辑
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                var result = tourUserService.GetInfo(Id);
                if (result.Code == ResultCode.Success)
                {
                    model = result.Data;
                }
            }
            return View(model);
        }

        public JsonResult Delete()
        {
            var Id = Request.Params["Id"];
            var result = tourUserService.Delete(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddOrEdit(TourUserModel entity)
        {
            entity.create_time = DateTime.Now;
            entity.modify_time = DateTime.Now;
            var result = tourUserService.AddOrEdit(entity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
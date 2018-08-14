using Plu.Platform.Domain.Impl.TourUser;
using Plu.Platform.Domain.TourUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xn.Platform.Abstractions.Domain;

namespace Xn.Platform.Admin.Controllers
{
    public class TourUserController : Controller
    {
        private static TourUserService tourUserService = new TourUserService();
        // GET: TourUsers
        public ActionResult Index(int pageIndex = 1, int pageSize = 2)
        {
            var result = tourUserService.PageList(new TourUserRequest.PageResult
            {
                PageSize = pageSize,
                PageIndex = pageIndex
            });
            ViewBag.PageIndex = pageIndex;
            return View(result.Data);
        }

        //GET:
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}
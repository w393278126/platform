using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xn.Platform.Domain.Impl;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Admin.Controllers
{
    public class OrderPlaneController : Controller
    {
        private OrderPlaneService orderPlaneService = new OrderPlaneService();
        // GET: OrderPlane
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RefundTicket(string Id)
        {
            var result = orderPlaneService.GetInfo(Id);
            return View(result.Data);
        }
        /// <summary>
        /// 退款操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult Refund(OrderPlaneRequest.RefundRequest request)
        {
            var result = orderPlaneService.RefundPlane(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 出票操作
        /// </summary>
        /// <param name="Id"></param>
        public JsonResult OutOfPlane(string Id)
        {
            var result = orderPlaneService.OutOfPlane(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xn.Platform.Domain.Impl.Order;

namespace Xn.Platform.Admin.Controllers
{
    public class OrderTicketController : Controller
    {
        private OrderTicketService orderTicketService = new OrderTicketService();
        // GET: OrderTicket
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult RefundTicket(string Id)
        //{
        //    var result = orderPlaneService.GetInfo(Id);
        //    return View(result.Data);
        //}
        /// <summary>
        /// 退款操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult RefundTicket(string Id)
        {
            var result = orderTicketService.RefundTicekt(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 出票操作
        /// </summary>
        /// <param name="Id"></param>
        public JsonResult OutOfTicket(string Id)
        {
            var result = orderTicketService.OutOfTicket(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
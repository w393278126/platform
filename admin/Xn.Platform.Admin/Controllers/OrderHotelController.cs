using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xn.Platform.Domain.Impl.Order;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Admin.Controllers
{
    public class OrderHotelController : Controller
    {
        private OrderHotelService orderHotelService = new OrderHotelService();
        // GET: OrderHotel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RefundHotel(string Id)
        {
            var result = orderHotelService.GetInfo(Id);
            return View(result.Data);
        }
        /// <summary>
        /// 退款操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult Refund(OrderHotelRequest.RefundRequest request)
        {
            var result = orderHotelService.Refund(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 订房操作
        /// </summary>
        /// <param name="Id"></param>
        public JsonResult Reserve(string Id)
        {
            var result = orderHotelService.Reserve(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
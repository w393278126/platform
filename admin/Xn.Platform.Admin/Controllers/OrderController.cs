using Xn.Platform.Domain.Impl.Order;
using Xn.Platform.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Xn.Platform.Admin.Controllers
{
    public class OrderController : Controller
    {
        private OrderMainService orderMainService = new OrderMainService();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 主订单列表
        /// </summary>
        /// <returns></returns>
        public JsonResult MainOrder(int pageIndex = 1, int pageSize = 10)
        {
            var result = orderMainService.PageList(new OrderMainRequest.PageRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
using Xn.Platform.Domain.Impl.Order;
using Xn.Platform.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xn.Platform.Domain;

namespace Xn.Platform.Admin.Controllers
{
    public class OrderController : Controller
    {
        private OrderMainService orderMainService = new OrderMainService();
        // GET: Order
        public ActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            var type = Convert.ToInt32(Request.Params["orderType"] ?? "0");
            var orderId = Request.Params["orderId"];
            var userName = Request.Params["userName"];
            var mobile = Request.Params["mobile"];
            var result = orderMainService.PageList(new OrderMainRequest.PageRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                orderType = type,
                orderId = orderId,
                userName = userName,
                mobile = mobile
            });
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            if (result.Code == 0 && result.Data != null)
            {
                return View(result.Data);

            }
            else
                return View(new PagedEntity<OrderMainResponse.PageResponse>());

        }
        /// <summary>
        /// 详情页面（弹出页面）
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(string orderId, int type)
        {
            var url = "~/Views/Order/Detail/";
            switch (type)
            {
                case 1:
                    url += "Plane.cshtml";

                    break;
                case 2:
                    url += "Hotel.cshtml";
                    break;
                case 4:
                    url += "Ticket.cshtml";
                    break;
                case 3:
                default:
                    url += "Default.cshtml";
                    break;

            }
            var model = orderMainService.GetInfo(orderId, type);
            return View(url, model.Data);
        }
    }
}
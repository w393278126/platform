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
            var s_addDate = Request.Params["s_addDate"];
            var e_addDate = Request.Params["e_addDate"];
            var s_payDate = Request.Params["s_payDate"];
            var e_payDate = Request.Params["e_payDate"];
            var searchModel = new OrderMainRequest.PageRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                orderType = type,
                orderId = orderId,
                userName = userName,
                mobile = mobile,
                SaddDate = s_addDate,
                EaddDate = e_addDate,
                SpayDate = s_payDate,
                EpayDate = e_payDate
            };
            var result = orderMainService.PageList(searchModel);
            ViewBag.SearchModel = searchModel;
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
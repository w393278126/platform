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
        private XnOrderService xnOrderService = new XnOrderService();
        private XnPassengerService xnPassengerService = new XnPassengerService();
        private XnOrderPassengerService xnOrderPassengerService = new XnOrderPassengerService();
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
                OrderType = type,
                OrderId = orderId,
                UserName = userName,
                Mobile = mobile,
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
                return View(new PagedEntity<OrderMainListDTO>());

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
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(int Id = 1)
        {
            var result = xnOrderService.GetInfo(Id);
            return View(result.Data);
        }
        /// <summary>
        /// 改价
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public JsonResult EditPrice()
        {
            int Id = Convert.ToInt32(Request.Params["Id"] ?? "0");
            decimal price = Convert.ToDecimal(Request.Params["price"]);
            var result = xnOrderService.EditPrice(Id, price);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 出行人
        /// </summary>
        /// <returns></returns>
        public ActionResult Passenger()
        {
            int Id = Convert.ToInt32(Request.Params["Id"]);
            int orderId = Convert.ToInt32(Request.Params["orderId"]);
            ViewBag.OrderId = orderId;
            if (Id > 0)
            {
                var result = xnPassengerService.GetInfo(Id);
                return View(result.Data);
            }
            else
            {
                return View(new XnPassengerDTO() { Birthday = DateTime.Now });
            }


        }
        /// <summary>
        /// 新增或者编辑人员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult AddOrEditPassenger(XnPassengerRequest request)
        {
            var result = xnPassengerService.AddOrEdit(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 移除出行人
        /// </summary>
        /// <returns></returns>
        public JsonResult DeletePassenger()
        {
            var orderId = Convert.ToInt32(Request.Params["orderId"]);
            var passengerId = Convert.ToInt32(Request.Params["Id"]);
            var result = xnOrderPassengerService.DeletePassenger(orderId, passengerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
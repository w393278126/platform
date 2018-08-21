using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    /// <summary>
    /// 订单表
    /// </summary>
    [Table("xnorder")]
    public class XnOrderModel
    {

        /// <summary>
        /// 主键、自增
        /// </summary>		

        public int Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>		

        public string OrderCode { get; set; }
        /// <summary>
        /// 订单类型1、机票 2、酒店 3、签证4、门票 5、爆款定制
        /// </summary>		

        public int OrderType { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>		

        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>		

        public string ProductName { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>		

        public int ProductSum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>		

        public decimal ProductPrice { get; set; }
        /// <summary>
        /// 购买人
        /// </summary>		

        public string UserId { get; set; }
        /// <summary>
        /// 爆款skuId
        /// </summary>		

        public int SkuId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>		

        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>		

        public DateTime EndTime { get; set; }
        /// <summary>
        /// 订单总额
        /// </summary>		

        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 最晚支付时间
        /// </summary>		

        public DateTime PayEndTime { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>		

        public decimal PayAmount { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>		

        public int PayType { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>		

        public string PayTSN { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>		

        public DateTime PayTime { get; set; }
        /// <summary>
        /// -1 删除  0待审核 （审核中状态记录在redis） 1待支付  2支付完成 3支付确认（支付回掉完成）  4退款中 5退款完成  6退款失败 7订单关闭
        /// </summary>		

        public int State { get; set; }
        /// <summary>
        /// 退款ID
        /// </summary>		

        public int RefundID { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>		

        public DateTime RefundDate { get; set; }
        /// <summary>
        /// 退款总金额
        /// </summary>		

        public decimal RefundAmout { get; set; }
        /// <summary>
        /// 第三方流水号
        /// </summary>		

        public int ChannelId { get; set; }
        /// <summary>
        /// 惊喜
        /// </summary>		

        public int SupplierId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>		

        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>		

        public string ContactPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		

        public string Remark { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>		

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>		

        public DateTime LastTime { get; set; }
        /// <summary>
        /// 最后更新管理员ID
        /// </summary>		

        public int LastAuthorId { get; set; }
        /// <summary>
        /// 创建管理员ID
        /// </summary>		

        public int CreateAuthorId { get; set; }
    }

    /// <summary>
    /// 输出字段
    /// </summary>
    public class XnOrderDTO
    {
        /// <summary>
        /// 主键、自增
        /// </summary>		

        public int Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>		

        public string OrderCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>		

        public string ProductName { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>		

        public int ProductSum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>		

        public decimal ProductPrice { get; set; }
        /// <summary>
        /// 订单总额
        /// </summary>		

        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>		

        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>		

        public string ContactPhone { get; set; }

        /// <summary>
        /// 爆款skuId
        /// </summary>		

        public int SkuId { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>		

        public DateTime PayTime { get; set; }
        /// <summary>
        /// -1 删除  0待审核 （审核中状态记录在redis） 1待支付  2支付完成 3支付确认（支付回掉完成）  4退款中 5退款完成  6退款失败 7订单关闭
        /// </summary>		

        public int State { get; set; }
        public string StateVal
        {
            get
            {
                string val = "";
                switch (State)
                {
                    case -1:
                        val = "删除";
                        break;
                    case 0:
                        val = "待审核";
                        break;
                    case 1:
                        val = "待支付";
                        break;
                    case 2:
                        val = "支付完成";
                        break;
                    case 3:
                        val = "支付确认（支付回掉完成）";
                        break;
                    case 4:
                        val = "退款中";
                        break;
                    case 5:
                        val = "退款完成";
                        break;
                    case 6:
                        val = "退款失败";
                        break;
                    case 7:
                        val = "订单关闭";
                        break;
                    default:
                        val = "新增";
                        break;
                }
                return val;
            }
        }

        /// <summary>
        /// 供应商名称
        /// </summary>		

        public int SupplierName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		

        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 出行人列表
        /// </summary>
        public List<XnPassengerDTO> orderPassengerDTOs { get; set; }
    }
}

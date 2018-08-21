using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    /// <summary>
    /// 订单跟单表
    /// </summary>
    [Table("xnorderlog")]
    public class XnOrderLogModel
    {
        /// <summary>
        /// 主键，自增
        /// </summary>		

        public int Id { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>		

        public int OrderId { get; set; }
        /// <summary>
        /// 跟单类型 1：创建订单 2：调整项 0：其他
        /// </summary>		

        public int Type { get; set; }
        /// <summary>
        /// 跟单类型
        /// </summary>		

        public string Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>		

        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>		

        public DateTime LastTime { get; set; }
        /// <summary>
        /// 更新管理员ID
        /// </summary>		

        public int LastAuthorId { get; set; }
        /// <summary>
        /// 创建管理员ID
        /// </summary>		

        public int CreateAuthorId { get; set; }
    }
}

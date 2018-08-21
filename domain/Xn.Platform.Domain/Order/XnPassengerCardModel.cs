using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    /// <summary>
    /// 游客证件表
    /// </summary>
    [Table("xnpassengercard")]
    public class XnPassengerCardModel
    {
        /// <summary>
        /// 主键
        /// </summary>		

        public int Id { get; set; }
        /// <summary>
        /// 游客ID
        /// </summary>		

        public int PassengerId { get; set; }
        /// <summary>
        /// 证件类型 1：身份证 2：护照
        /// </summary>		

        public int CardType { get; set; }
        /// <summary>
        /// 证件编号
        /// </summary>		

        public string CardId { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>		

        public DateTime OverTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>		

        public int Status { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>		

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// LastTime
        /// </summary>		

        public DateTime LastTime { get; set; }
        /// <summary>
        /// LastAuthorId
        /// </summary>		

        public int LastAuthorId { get; set; }
        /// <summary>
        /// CreateAuthorId
        /// </summary>		

        public int CreateAuthorId { get; set; }
    }
}

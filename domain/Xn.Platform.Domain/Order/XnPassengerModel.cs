using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Order
{
    /// <summary>
    /// 出行游客表
    /// </summary>
    [Table("xnpassenger")]
    public class XnPassengerModel
    {
        /// <summary>
        /// 主键
        /// </summary>		

        public int Id { get; set; }
        /// <summary>
        /// 姓
        /// </summary>		

        public string Xing { get; set; }
        /// <summary>
        /// 名
        /// </summary>		

        public string Ming { get; set; }
        /// <summary>
        /// 姓（拼音/英文）
        /// </summary>		

        public string EXing { get; set; }
        /// <summary>
        /// 名（拼音/英文）
        /// </summary>		

        public string Eming { get; set; }
        /// <summary>
        /// 管理用户ID
        /// </summary>		

        public string UserId { get; set; }
        /// <summary>
        /// 类型 1.成人  2.儿童
        /// </summary>		

        public int Type { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		

        public int Sex { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		

        public string Phone { get; set; }
        /// <summary>
        /// 生日
        /// </summary>		

        public DateTime Birthday { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>		

        public string Nationality { get; set; }
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
    /// <summary>
    /// 游客信息
    /// </summary>
    public class XnPassengerDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 姓
        /// </summary>		

        public string Xing { get; set; }
        /// <summary>
        /// 名
        /// </summary>		

        public string Ming { get; set; }
        /// <summary>
        /// 姓（拼音/英文）
        /// </summary>		

        public string EXing { get; set; }
        /// <summary>
        /// 名（拼音/英文）
        /// </summary>		

        public string Eming { get; set; }
        /// <summary>
        /// 管理用户ID
        /// </summary>		

        public string UserId { get; set; }
        /// <summary>
        /// 类型 1.成人  2.儿童
        /// </summary>		

        public int Type { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		

        public int Sex { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		

        public string Phone { get; set; }
        /// <summary>
        /// 生日
        /// </summary>		

        public DateTime Birthday { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>		

        public string Nationality { get; set; }
        /// <summary>
        /// 护照号
        /// </summary>
        public string PassportNo { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }
    }

    /// <summary>
    /// 出行人实体请求
    /// </summary>
    public class XnPassengerRequest
    {
        public int Id { get; set; }
        /// <summary>
        /// 姓
        /// </summary>		

        public string Xing { get; set; }
        /// <summary>
        /// 名
        /// </summary>		

        public string Ming { get; set; }
        /// <summary>
        /// 姓（拼音/英文）
        /// </summary>		

        public string EXing { get; set; }
        /// <summary>
        /// 名（拼音/英文）
        /// </summary>		

        public string Eming { get; set; }
        /// <summary>
        /// 管理用户ID
        /// </summary>		

        public string UserId { get; set; }
        /// <summary>
        /// 类型 1.成人  2.儿童
        /// </summary>		

        public int Type { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		

        public int Sex { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		

        public string Phone { get; set; }
        /// <summary>
        /// 生日
        /// </summary>		

        public DateTime Birthday { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>		

        public string Nationality { get; set; }
        /// <summary>
        /// 护照号
        /// </summary>
        public string PassportNo { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderId { get; set; }
    }
}

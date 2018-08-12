using System;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 域名重定向实体
    /// </summary>
    [Table("DomainRedirection")]
    public class DomainRedirection : Entity
    {
        /// <summary>
        /// 跳转域名
        /// </summary>
        public string FromDomain { set; get; }
        /// <summary>
        /// 跳至域名
        /// </summary>
        public string ToDomain { set; get; }
        /// <summary>
        /// 起效时间
        /// </summary>
        public string StartTime { set; get; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public string EndTime { set; get; }
        /// <summary>
        /// 状态 1是启用 0是不启用
        /// </summary>
        [ExcludeStatus]
        public int Status { set; get; }
        /// <summary>
        /// 创建管理员
        /// </summary>
        public string CreateAdmin { get; set; }
        /// <summary>
        /// 最后一次维护的管理员
        /// </summary>
        public string LastUpdateAdmin { get; set; }
        /// <summary>
        /// 最后一次维护的时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建管理员名称
        /// </summary>
        [Computed]
        public string CreateAdminName { get; set; }
        /// <summary>
        /// 最后一次维护的管理员名称
        /// </summary>
        [Computed]
        public string LastUpdateAdminName { get; set; }
    }

    /// <summary>
    /// 重定向列表多条件查询
    /// </summary>
    public class DomainRedirectionCondition : BaseQueryModel
    {
        /// <summary>
        /// 跳转域名
        /// </summary>
        public string FromDomain { set; get; }
        /// <summary>
        /// 跳至域名
        /// </summary>
        public string ToDomain { set; get; }
        /// <summary>
        /// 起效时间
        /// </summary>
        public DateTime Start { set; get; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime End { set; get; }
    }
}

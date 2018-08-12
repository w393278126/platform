using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育赛事直播权限实体
    /// </summary>
    [Table("SportLivePermissions")]
    public class SportLivePermissions : Entity
    {
        /// <summary>
        /// 赛事ID
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 房间ID,0代表所有房间
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 房间域名
        /// </summary>
        [Computed]
        public string Domain { get; set; }
        /// <summary>
        /// 所有的渠道权限和（通过"位或"运算汇总）
        /// </summary>
        public int Channels { get; set; }
        /// <summary>
        /// 状态  1：正常 0废除
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建管理员ID
        /// </summary>
        public int CreateAdmin { get; set; }
        /// <summary>
        /// 创建管理员名称
        /// </summary>
        [Computed]
        public string CreateAdminName { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 最后更新管理员ID
        /// </summary>
        public int LastUpdateAdmin { get; set; }
        /// <summary>
        /// 最后更新管理员名称
        /// </summary>
        [Computed]
        public int LastUpdateAdminName { get; set; }

    }

    /// <summary>
    /// 后台指定赛事的权限集合
    /// </summary>
    public class SportLivePermissionsIndex
    {
        public SportLivePermissionsIndex()
        {
            this.Permissions = new List<SportLivePermissionsInfo>();
        }
        /// <summary>
        /// 主播权限列表
        /// </summary>
        public List<SportLivePermissionsInfo> Permissions { get; set; }
    }
    /// <summary>
    /// 后台指定赛事权限的归类明细
    /// </summary>
    public class SportLivePermissionsInfo
    {
        /// <summary>
        /// 赛事ID
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 主播域名集合
        /// </summary>
        public string Domains { get; set; }
        /// <summary>
        /// 可播渠道集合
        /// </summary>
        public int Channels { get; set; }
        /// <summary>
        /// 可播渠道名称集合
        /// </summary>
        public string ChannelsName { get; set; }
        /// <summary>
        /// 历史域名集合
        /// </summary>
        public string OldDomains { get; set; }

    }

    /// <summary>
    /// 权限类型枚举(通过channel和device归一化结果)
    /// </summary>
    public enum ChannelPermissionsEnum
    {
        /// <summary>
        /// Web
        /// </summary>
        PC = 1,
        /// <summary>
        /// IOS
        /// </summary>
        iOS = 2,
        /// <summary>
        /// Android
        /// </summary>
        Android = 4,
        /// <summary>
        /// H5
        /// </summary>
        H5 = 8,
        /// <summary>
        /// 嗨播
        /// </summary>
        HiPlay = 64,
        /// <summary>
        /// PPTV PC
        /// </summary>
        PPTV = 16,
    }
    
    /// <summary>
    /// 渠道枚举
    /// </summary>
    public class PermissionsTypes
    {
        public string Name { get; set; }

        public int Value { get; set; }
    }
}

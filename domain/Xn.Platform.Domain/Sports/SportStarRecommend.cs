using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    [Table("SportStarRecommend")]
    public class SportStarRecommend : Entity
    {
        /// <summary>
        /// 主播名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主播头像
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 主播域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// 直播地址
        /// </summary>
        public string LiveUrl { get; set; }

        /// <summary>
        /// 主播描述（附标题）
        /// </summary>
        public string NameDes { get; set; }

        /// <summary>
        /// 特殊标记
        /// </summary>
        public string FeatureSign { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态 1-正常 0-删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建管理员
        /// </summary>
        public string CreateAdmin { get; set; }

        /// <summary>
        /// 创建管理员
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建管理员
        /// </summary>
        [Computed]
        public string CreateAdminName { get; set; }

        /// <summary>
        /// 最后更新管理员
        /// </summary>
        public string LastUpdateAdmin { get; set; }


        /// <summary>
        /// 最后更新管理员名称
        /// </summary>
        [Computed]
        public string LastUpdateAdminName { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 明星类型
        /// </summary>
        public StarTypeEnum Type { get; set; }
    }

    /// <summary>
    /// 体育主播推荐查询条件
    /// </summary>
    public class SportStarRecommendCondition : BaseQueryModel
    {
        public SportStarRecommendCondition()
        {
            this.Type = 1;
            this.OrderBy = "id";
            this.PageIndex = 0;
            this.PageSize = 10;
            this.ToSort = true;
        }
        public string Domain { get; set; }
        public string Name { get; set; }

        public int Type { get; set; }

    }

    /// <summary>
    /// 体育主播基础明细
    /// </summary>
    public class SportStarBasic
    {
        /// <summary>
        /// 主播名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主播头像
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 主播域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// 直播地址
        /// </summary>
        public string LiveUrl { get; set; }

        /// <summary>
        /// 主播描述（附标题）
        /// </summary>
        public string NameDes { get; set; }

        /// <summary>
        /// 是否正在直播
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [IgnoreDataMember]
        public int Sort { get; set; }
        /// <summary>
        /// GameId
        /// </summary>
        public int GameId { get; set; }
    }

    /// <summary>
    /// 主播类型枚举
    /// </summary>
    public enum StarTypeEnum
    {
        /// <summary>
        /// 明星推荐
        /// </summary>
        Star = 0,
        /// <summary>
        /// 足球宝贝推荐
        /// </summary>
        Baby = 1
    }
}

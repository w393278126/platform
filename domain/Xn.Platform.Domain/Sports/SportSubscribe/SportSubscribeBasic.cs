using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    [Table("SportSubscribe")]
    public class SportSubscribe
    {
        public SportSubscribe()
        { }

        /// <summary>
        /// 自增长ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  对象ID MatchSubscribe则为 赛事Id,RoomSubscribe则为RoomId
        /// </summary>
        public int ObjectId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 预约类型 0 赛事预约 1 房间预约
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 1正常数据 0删除数据
        /// </summary>
        //public int Status { get; set; }
        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime PushTime { get; set; }
        /// <summary>
        /// 是否已推送 0 否、1是
        /// </summary>
        public int IsPush { get; set; }
        /// <summary>
        /// 状态 1已预约  0已取消
        /// </summary>
        public int Operation { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

    }

    /// <summary>
    /// 预约列表查询对象
    /// </summary>
    public class SportSubscribeCondition: BaseQueryModel
    {
        public SportSubscribeCondition()
        {
            this.PageIndex = 0;
            this.PageSize = 20;
            this.Type = SubscribeCategory.MatchSubscribe;
            this.Operation = 1;
            this.OrderBy = "PushTime";
            this.ToSort = false;
        }
        public SubscribeCategory Type { get; set; }

        public int UserId { get; set; }

        public int Operation { get; set; }
    }

    /// <summary>
    /// 预约列表 基础信息
    /// </summary>
    public class SubscribeBasic
    {
        /// <summary>
        /// 预约总条数
        /// </summary>
        public int TotalItems { get; set; }
    }

    /// <summary>
    /// 添加体育预约对象
    /// </summary>
    public class SubscribeEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 预约类型  SubscribeCategory
        /// </summary>
        public SubscribeCategory Type { get; set; }
        /// <summary>
        /// 对象ID MatchSubscribe则为 赛事Id,RoomSubscribe则为RoomId
        /// </summary>
        public int ObjectId { get; set; }
        /// <summary>
        /// 1添加预约 0取消预约
        /// </summary>
        public int Operation { get; set; }
    }

    /// <summary>
    /// 预约分类 枚举
    /// </summary>
    public enum SubscribeCategory
    {
        MatchSubscribe = 0,
        RoomSubscribe = 1
    }

    /// <summary>
    /// 赛事预约返回实体
    /// </summary>
    public class MatchSubscribe : SubscribeBasic
    {
        public MatchSubscribe()
        {
            this.Matchs = new List<SubscribeMatch>();
            this.TotalItems = 0;

        }
        public List<SubscribeMatch> Matchs { get; set; }
    }

    public class SubscribeMatch : SportMatchBasic
    {
        /// <summary>
        /// 推荐房间列表
        /// </summary>
        public RecommendRoom RecommendRooms { get; set; }
        /// <summary>
        /// 预约列表
        /// </summary>
        public List<SubscribeInfo> Subscribe { get; set; }
    }
}

using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    [Table("SportMatchConfig")]
    public class SportMatchConfig : Entity
    {
        public SportMatchConfig()
        {
            CreateTime = DateTime.Now;
            AllScheduleShow = AllScheduleShowEnum.No;
            AllowForceDel = false;
        }

        public string MatchName { get; set; }
        public int RoundCounts { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateAuthorId { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LastAuthorId { get; set; }
        [ExcludeStatus]
        public int Status { get; set; }
        public string JoinTeams { get; set; }
        public string Image { get; set; }
        public int SortId { get; set; }
        /// <summary>
        /// 对阵类型（0=对抗型，1=非对抗型）
        /// </summary>
        public FightTypeEnum FightType { get; set; }
        /// <summary>
        /// 结束时间=到达此日期后该赛事对应的所有赛程表在web+app自动隐藏，例如该项填写的是2017年9月30日，则到达2017年9月29日23：59后，对应赛程表在前端自动隐藏
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 全部赛程页展示（-1=不展示，0=展示）
        /// </summary>
        public AllScheduleShowEnum AllScheduleShow { get; set; }


        #region

        /// <summary>
        /// 用于管理后台确认强制删除
        /// </summary>
        [Computed]
        public bool AllowForceDel { get; set; }

        #endregion
    }

    /// <summary>
    /// 全部赛程页展示
    /// </summary>
    public enum AllScheduleShowEnum
    {
        No = -1,
        Yes = 0
    }
}

using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports.Zcry
{
    /// <summary>
    /// SportStageActivity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("SportStageActivity")]
    public class SportStageActivity : Entity
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StarTime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 活动阶段名称
        /// </summary>
        public int ActivityStage { get; set; }
        /// <summary>
        /// 活动阶段名称
        /// </summary>
        [Computed]
        public string ActivityStageName { get; set; }
        /// <summary>
        /// A主播roomid
        /// </summary>
        public int AStarRoomId { get; set; }
        /// <summary>
        /// 主播A域名
        /// </summary>
        public string AStarDomain { get; set; }
        /// <summary>
        /// B主播roomid
        /// </summary>
        public int BStarRoomId { get; set; }
        /// <summary>
        /// 主播B域名
        /// </summary>
        public string BStarDomain { get; set; }
        /// <summary>
        /// A主播积分
        /// </summary>
        public long AScores { get; set; }
        /// <summary>
        /// B主播积分
        /// </summary>
        public long BScores { get; set; }
        /// <summary>
        /// 关联PKID 、轮次
        /// </summary>
        public int CoreId { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [Computed]
        public string GroupName { get; set; }
        /// <summary>
        /// 分组编号
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 1正常 0废弃
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// 最后修改管理员
        /// </summary>
        public int OperationAdmin { get; set; }

        [Computed]
        public int ATeamId { get; set; }
        [Computed]
        public int BTeamId { get; set; }
        [Computed]
        public string ATeamName { get; set; }
        [Computed]
        public string BTeamName { get; set; }
        [Computed]
        public string ATeamAvatar { get; set; }
        [Computed]
        public string BTeamAvatar { get; set; }
        [Computed]
        public List<PkActivityInfo> PkAcitivis { get; set; }
        [Computed]
        public List<ActivityTeam> Teams { get; set; }
    }

    /// <summary>
    /// PK活动关联对象
    /// </summary>
    public class PkActivityInfo
    {
        public int id { get; set; }
        public string Title { get; set; }
    }

    /// <summary>
    /// 活动关联队伍
    /// </summary>
    public class ActivityTeam
    {
        public int id { get; set; }
        public string Name { get; set; }
    }




    /// <summary>
    /// 体育主播推荐查询条件
    /// </summary>
    public class SportStageActivityCondition : BaseQueryModel
    {
        public SportStageActivityCondition()
        {
            this.ActivityStage = 0;
            this.OrderBy = "id";
            this.PageIndex = 0;
            this.PageSize = 10;
            this.ToSort = true;
        }
        public string Domain { get; set; }

        public int ActivityStage { get; set; }

    }
}

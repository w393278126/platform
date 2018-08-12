using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    [Table("SportMatchProportion")]
    public class SportMatchProportion : Entity
    {
        /// <summary>
        /// 比赛ID
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 官方分成比例
        /// </summary>
        public float OfficialProportion { get; set; }
        /// <summary>
        /// 主播分成比例
        /// </summary>
        public float StarProportion { get; set; }
        /// <summary>
        /// 比赛价格
        /// </summary>
        public int MatchPrice { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string GameName { get; set; }
        /// <summary>
        /// 对战信息
        /// </summary>
        public string PkInfo { get; set; }
        /// <summary>
        /// 状态 1正常 0废除
        /// </summary>
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime OperationTime { get; set; }
        public int OperationAdmin { get; set; }
        [Computed]
        public string StarTime { get; set; }
        [Computed]
        public string MatchState { get; set; }
    }



    public class MatchProportionConditong: BaseQueryModel
    {
        public string ClubName { get; set; }

        public string GameName { get; set; }

    }

    public class ProportionLoadData
    {
        public ProportionLoadData()
        {
            this.MatchConfig = new List<BaseData>();
            this.MatchClub = new List<ClueBaseData>();
            this.MatchId = new List<MatchBaseData>();
        }
        public List<BaseData> MatchConfig { get; set; }

        public List<ClueBaseData> MatchClub { get; set; }

        public List<MatchBaseData> MatchId { get; set; }
     }

    public class BaseData
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class ClueBaseData : BaseData
    {
        public int ParentId { get; set; }
    }

    public class MatchBaseData : ClueBaseData
    {
        public string PkInfo { get; set; }
        public string StarTime { get; set; }
        public int AClubId { get; set; }
        public int BClubId { get; set; }
    }
}

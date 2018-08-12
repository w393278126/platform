using System;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育档期
    /// </summary>
    [Table("SportSchedules")]
    public class SportSchedule : OpEntity
    {
        public int GameId { set; get; }
        public string Title { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public int SportType { set; get; }
        [ExcludeStatus]
        public int Status { set; get; }
        public int RoundId { set; get; }
    }


    /// <summary>
    /// 体育档期V2
    /// </summary>
    [Table("SportSchedulesV2")]
    public class SportSchedulesV2 : OpEntity
    {
        public SportSchedulesV2() { }
        /// <summary>
        /// 中超，西甲的id
        /// </summary>
        public int GameId { set; get; }
        public string Title { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        [ExcludeStatus]
        public int Status { set; get; }
        public int RoundId { set; get; }
        public FightTypeEnum FightType { get; set; }
    }
}

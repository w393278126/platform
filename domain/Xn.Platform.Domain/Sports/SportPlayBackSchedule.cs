using System;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育回放赛事排期
    /// </summary>
    [Table("SportPlayBackSchedules")]
    public class SportPlayBackSchedule : OpEntity
    {
        public int GameId { set; get; }
        public int RoomId { set; get; }
        public string Title { set; get; }
        public int ReportPlayTimeId { set; get; }
        public DateTime RecordingTime { set; get; }
        public int Duration { set; get; }
        public int VideoState { set; get; }
        [ExcludeStatus]
        public int Status { set; get; }
        public int SortNum { set; get; }
    }
}
using System.Collections.Generic;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 赛事房间回放信息
    /// </summary>
    public class SportRoomPlayBackInfo
    {
        public string PlayBackTitle { set; get; }
        public int GameId { set; get; }
        public int RoomId { set; get; }
        public ICollection<SportPlayBackSchedule> Schedules { set; get; }
    }
}
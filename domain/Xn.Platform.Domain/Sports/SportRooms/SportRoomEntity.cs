using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports.SportRooms
{
    public class SportRoomBasic
    {
        
    }

    public class SportRoomNarrate : SportRoomBasic
    {
        /// <summary>
        /// 房间Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否直播中
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// 房间封面
        /// </summary>
        public string Preview { get; set; }
    }
}

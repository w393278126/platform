using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Abstractions.Domain
{
    /// <summary>
    /// 座驾消息实体
    /// </summary>
    public class VehicleMessageInfo
    {

        /// <summary>
        /// 座驾名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 道具FlashID
        /// </summary>
        public string giftFlashId { get; set; }

        /// <summary>
        /// Web小图Url
        /// </summary>
        public string webChatPicUrl { get; set; }

        /// <summary>
        /// App小图Url
        /// </summary>
        public string appChatPicUrl { get; set; }

        /// <summary>
        /// Web商城Url
        /// </summary>
        public string webMallUrl { get; set; }

        /// <summary>
        /// Web个人中心Url
        /// </summary>
        public string webUserCenterPicUrl { get; set; }

        /// <summary>
        /// 座驾类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 限制类型
        /// </summary>
        public int restrictType { get; set; }
    }
}

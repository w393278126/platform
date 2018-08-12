using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Abstractions.Domain
{
    public class UserPrettyNumber : ICloneable
    {
        /// <summary>
        /// 靓号
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 靓号类型(1:普通永久,2:尊贵,3:尊贵永久)
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 过期时间戳
        /// </summary>
        public long expireTime { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

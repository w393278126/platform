using  Xn.Platform.Abstractions.Domain;
using System;

namespace Xn.Platform.Domain.User
{
    /// <summary>
    /// Redis持久保存对象 用户中心
    /// </summary>
    public class UserMessageInfo
    {
        public int uid { get; set; }
        public string username { get; set; }
        public int grade { get; set; }
        public int newGrade { get; set; }
    }

    /// <summary>
    /// Redis持久保存对象 用户中心
    /// </summary>
    public class UserInfo : UserMessageInfo, ICloneable
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar { get; set; }

        public int sex { get; set; }

        public int geocode { get; set; }

        public UserStealthy stealthy { get; set; }
        public int status { get; set; }

        /// <summary>
        /// 靓号
        /// </summary>
        public UserPrettyNumber prettyNumber { get; set; }

        public object Clone()
        {
            var clone = (UserInfo)this.MemberwiseClone();
            if (this.stealthy != null)
            {
                clone.stealthy = (UserStealthy)this.stealthy.Clone();
            }
            if (this.prettyNumber != null)
            {
                clone.prettyNumber = (UserPrettyNumber)this.prettyNumber.Clone();
            }
            return clone;
        }
    }

    /// <summary>
    /// 隐身信息
    /// </summary>
    public class UserStealthy : ICloneable
    {
        public UserStealthy()
        {
            this.isHide = false;
            this.nickname = string.Empty;
            this.avatar = string.Empty;
        }

        /// <summary>
        /// 是否隐身
        /// </summary>
        public bool isHide { get; set; }
        /// <summary>
        /// 隐身后的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 隐身后头像Icon
        /// </summary>
        public string avatar { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

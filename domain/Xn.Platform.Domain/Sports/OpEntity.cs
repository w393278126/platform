using System;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 后台用户操作记录实体
    /// </summary>
    public class OpEntity : Entity
    {
        public DateTime CreateTime { set; get; }
        public int CreateAuthorId { set; get; }
        public DateTime LastUpdateTime { set; get; }
        public int LastAuthorId { set; get; }
    }
}
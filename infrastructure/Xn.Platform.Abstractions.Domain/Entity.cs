using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xn.Platform.Core.Data;

namespace Xn.Platform.Domain
{ 
    /// <summary>
    /// 继承Entity使用MySql必须有Status属性并赋值为1
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 实体标识
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key, Required,
        DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }

    /// <summary>
    /// 实体操作
    /// </summary>
    public class OperationEntity : Entity
    {
        public int CreateAuthorId { set; get; }
        public DateTime CreateTime { set; get; }
        public int LastAuthorId { set; get; }
        public DateTime LastTime { set; get; }
    }
}

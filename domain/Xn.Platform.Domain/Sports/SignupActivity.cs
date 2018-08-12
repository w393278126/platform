using Xn.Platform.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Sports
{
    /// <summary>
    /// 体育报名活动
    /// </summary>
    [Table("SignupActivity")]
    public class SignupActivity
    {
        [Key]
        public int Id { get; set; }
        [Description("主播的uid")]
        public int Uid { get; set; }
        [Description("主播昵称")]
        public string AnchorName { get; set; }
        [Description("报名时间")]
        public string SignupTime { get; set; }
        [Description("活动名称")]
        public string ActivityName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Domain.Admin
{
    /// <summary>
    /// 登录实体对象
    /// </summary>
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string Code { get; set; }

        public string CodeToken { get; set; }
    }

    /// <summary>
    /// 验证码
    /// </summary>
    public class LoginCode
    {
        public string Code { get; set; }

        public string CodeToken { get; set; }
    }

}



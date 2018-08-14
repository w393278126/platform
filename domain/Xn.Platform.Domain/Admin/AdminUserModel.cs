using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Core.Data;
using Xn.Platform.Domain;

namespace Xn.Platform.Domain.Admin
{
    [Table("XnAdminUser")]
    public class AdminUserModel : OperationEntity
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public string Phone { get; set; }
        public string RealName { get; set; }

        public string NickName { get; set; }

        public int Role { get; set; }
        public int Type { get; set; }

        public int SupplierID { get; set; }

        public string QQ { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public string MacAddress { get; set; }

        public string IpAddress { get; set; }
        public string ICon { get; set; }
    }

    /// <summary>
    /// 登录实体对象
    /// </summary>
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string Code { get; set; }
    }
}

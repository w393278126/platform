using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Admin;
using Xn.Platform.Domain.Admin;

namespace Plu.Platform.Domain.Impl.Admin
{
    public class LoginService
    {
        private static AdminRepository adminRepository = new AdminRepository();

        public ResultWithCodeEntity Loging(LoginModel login)
        {
            var admin = adminRepository.GetInfo(login.UserName, login.Password);
            if (admin == null || admin.Id <= 0)
            {
                return Result.Error(ResultCode.UserNotExist);
            }
            return Result.Success();
        }

    }
}

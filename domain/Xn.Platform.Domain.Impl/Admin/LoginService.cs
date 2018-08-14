using Xn.Platform.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Abstractions.Domain;
using Xn.Platform.Data.MySql.Admin;
using Xn.Platform.Data.Redis.Login;
using Xn.Platform.Infrastructure.Auth;

namespace Xn.Platform.Domain.Impl.Admin
{
    public class LoginService
    {
        private static AdminRepository adminRepository = new AdminRepository();
        private static XnValidateCodeHandler xnValidateCodeHandler = new XnValidateCodeHandler();
        public ResultWithCodeEntity Loging(LoginModel login)
        {
            var token = XnAuthentication.GetValidateCookie();

            if (!xnValidateCodeHandler.IsAuthCode(token, login.Code))
            {
                return Result.Error(ResultCode.ValidateCodeError);
            }

            var admin = adminRepository.GetInfo(login.UserName, login.Password);
            if (admin == null || admin.Id <= 0)
            {
                return Result.Error(ResultCode.UserNotExist);
            }
            if (!string.IsNullOrEmpty(admin.MacAddress))
            {

            }
            if (!string.IsNullOrEmpty(admin.IpAddress))
            {

            }
            //将用户在token 写入cookie
            XnAuthentication.SetAuthCookie(admin.Id.ToString());
            return Result.Success();
        }
    }
}

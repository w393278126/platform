using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Data;
using Xn.Platform.Data.MySql;
using Xn.Platform.Data.MySql.Admin;
using Xn.Platform.Domain.Admin;
using Xn.Platform.Interface.Admin;

namespace Xn.Platform.Domain.Impl.Admin
{
    public class AdminUserService : BaseService, IAdminInterface
    {
        //private static AdminRepository adminRepository = new AdminRepository();
        private IRepository<AdminUserModel> adminRepository;
        public AdminUserService()
        {
            adminRepository = GetService<IRepository<AdminUserModel>>();
        }
        public void TestAdmin()
        {
            var admin = adminRepository.Get(1);
            var son = Mapper.Map<TestAdminSunDTO>(admin);
        }
    }

}

using AutoMapper;
using Xn.Platform.Domain.Admin;

namespace Xn.Platform.Domain.Impl.AutoMappers.Admin
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminUserModel, TestAdminSunDTO>()
               .ForMember(d => d.A, opt => { opt.MapFrom(s => s.UserName); })
               .ForMember(d => d.B, opt => { opt.MapFrom(s => s.SupplierID); })
               .ForMember(d => d.C, opt => { opt.MapFrom(s => s.NickName); })
               .ForMember(d => d.D, opt => { opt.MapFrom(s => s.RealName); })
               .ForMember(d => d.Password, opt => { opt.MapFrom(s => s.Status.ToString()); });
        }
    }
}

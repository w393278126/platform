using AutoMapper;
using Xn.Platform.Domain.TourUser;

namespace Xn.Platform.Domain.Impl.AutoMappers.Admin
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<TourUserModel, TourUserDTO>()
                .ForMember(d => d.Address, opt => { opt.MapFrom(s => s.address); })
                .ForMember(d => d.BirthDay, opt => { opt.MapFrom(s => s.birthday); })
                .ForMember(d => d.City, opt => { opt.MapFrom(s => s.city); })
                .ForMember(d => d.Id, opt => { opt.MapFrom(s => s.id); })
                .ForMember(d => d.Mobile, opt => { opt.MapFrom(s => s.mobile); })
                .ForMember(d => d.Nationality, opt => { opt.MapFrom(s => s.nationality); })
                .ForMember(d => d.NickName, opt => { opt.MapFrom(s => s.nick_name); })
                .ForMember(d => d.Passport, opt => { opt.MapFrom(s => s.passport); })
                .ForMember(d => d.PassWord, opt => { opt.MapFrom(s => s.password); })
                .ForMember(d => d.Prvinice, opt => { opt.MapFrom(s => s.prvinice); })
                .ForMember(d => d.QqNumber, opt => { opt.MapFrom(s => s.qq_number); })
                .ForMember(d => d.RealName, opt => { opt.MapFrom(s => s.real_name); })
                .ForMember(d => d.Sex, opt => { opt.MapFrom(s => s.sex); })
                .ForMember(d => d.UserName, opt => { opt.MapFrom(s => s.username); })
                .ForMember(d => d.Wechat, opt => { opt.MapFrom(s => s.wechat); }).ReverseMap();


            CreateMap<TourUserModel, TourUserListDTO>()
                    .ForMember(d => d.Address, opt => { opt.MapFrom(s => s.address); })
                    .ForMember(d => d.BirthDay, opt => { opt.MapFrom(s => s.birthday); })
                    .ForMember(d => d.City, opt => { opt.MapFrom(s => s.city); })
                    .ForMember(d => d.Id, opt => { opt.MapFrom(s => s.id); })
                    .ForMember(d => d.Mobile, opt => { opt.MapFrom(s => s.mobile); })
                    .ForMember(d => d.Nationality, opt => { opt.MapFrom(s => s.nationality); })
                    .ForMember(d => d.NickName, opt => { opt.MapFrom(s => s.nick_name); })
                    .ForMember(d => d.Passport, opt => { opt.MapFrom(s => s.passport); })
                    .ForMember(d => d.PassWord, opt => { opt.MapFrom(s => s.password); })
                    .ForMember(d => d.Prvinice, opt => { opt.MapFrom(s => s.prvinice); })
                    .ForMember(d => d.QqNumber, opt => { opt.MapFrom(s => s.qq_number); })
                    .ForMember(d => d.RealName, opt => { opt.MapFrom(s => s.real_name); })
                    .ForMember(d => d.Sex, opt => { opt.MapFrom(s => s.sex); })
                    .ForMember(d => d.UserName, opt => { opt.MapFrom(s => s.username); })
                    .ForMember(d => d.Wechat, opt => { opt.MapFrom(s => s.wechat); })
                    .ForMember(d => d.CreateTime, opt => { opt.MapFrom(s => s.create_time); })
                    .ForMember(d => d.PictureUrl, opt => { opt.MapFrom(s => s.picture_url); }).ReverseMap();
        }

    }
}

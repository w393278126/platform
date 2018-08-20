using AutoMapper;
using System.Linq;
using System.Reflection;
using Xn.Platform.Domain.Impl.AutoMappers.Admin;

namespace Xn.Platform.Domain.Impl.AutoMappers
{
    public  class Configuration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AdminProfile>();
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<OrderHotelProfile>();
                cfg.AddProfile<OrderPlaneProfile>();
                cfg.AddProfile<OrderTicketProfile>();
            });
        }
    }
}

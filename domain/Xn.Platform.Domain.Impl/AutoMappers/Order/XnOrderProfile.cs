using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.AutoMappers
{
   public class XnOrderProfile: Profile
    {
        public XnOrderProfile()
        {
            CreateMap<XnOrderModel, XnOrderDTO>();
            CreateMap<XnPassengerModel, XnPassengerDTO>();
            CreateMap<XnPassengerRequest, XnPassengerModel>();
        }
    }
}

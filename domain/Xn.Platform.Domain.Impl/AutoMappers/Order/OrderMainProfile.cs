using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.AutoMappers
{
    public class OrderMainProfile : Profile
    {
        public OrderMainProfile()
        {
            CreateMap<OrderMainModel, OrderMainDTO>()
                          .ForMember(d => d.AddDate, opt => { opt.MapFrom(s => s.addDate); })
                          .ForMember(d => d.Address, opt => { opt.MapFrom(s => s.address); })
                          .ForMember(d => d.AreaID, opt => { opt.MapFrom(s => s.areaID); })
                          .ForMember(d => d.AreaName, opt => { opt.MapFrom(s => s.areaName); })
                          .ForMember(d => d.ChannelID, opt => { opt.MapFrom(s => s.channelID); })
                          .ForMember(d => d.CityID, opt => { opt.MapFrom(s => s.cityID); })
                          .ForMember(d => d.CityName, opt => { opt.MapFrom(s => s.cityName); })
                          .ForMember(d => d.DeviceID, opt => { opt.MapFrom(s => s.deviceID); })
                          .ForMember(d => d.ExpressAmount, opt => { opt.MapFrom(s => s.expressAmount); })
                          .ForMember(d => d.ExpressCode, opt => { opt.MapFrom(s => s.expressCode); })
                          .ForMember(d => d.ExpressName, opt => { opt.MapFrom(s => s.expressName); })
                          .ForMember(d => d.ExpressOdd, opt => { opt.MapFrom(s => s.expressOdd); })
                          .ForMember(d => d.Id, opt => { opt.MapFrom(s => s.id); })
                          .ForMember(d => d.Latitude, opt => { opt.MapFrom(s => s.latitude); })
                          .ForMember(d => d.Longitude, opt => { opt.MapFrom(s => s.longitude); })
                          .ForMember(d => d.Num, opt => { opt.MapFrom(s => s.num); })
                          .ForMember(d => d.OrderAmount, opt => { opt.MapFrom(s => s.orderAmount); })
                          .ForMember(d => d.OrderID, opt => { opt.MapFrom(s => s.orderID); })
                          .ForMember(d => d.OrderType, opt => { opt.MapFrom(s => s.orderType); })
                          .ForMember(d => d.PayAmount, opt => { opt.MapFrom(s => s.payAmount); })
                          .ForMember(d => d.PayDate, opt => { opt.MapFrom(s => s.payDate); })
                          .ForMember(d => d.PayName, opt => { opt.MapFrom(s => s.payName); })
                          .ForMember(d => d.PayTelephone, opt => { opt.MapFrom(s => s.payTelephone); })
                          .ForMember(d => d.PayTSN, opt => { opt.MapFrom(s => s.payTSN); })
                          .ForMember(d => d.PayType, opt => { opt.MapFrom(s => s.payType); })
                          .ForMember(d => d.Platform, opt => { opt.MapFrom(s => s.platform); })
                          .ForMember(d => d.PostType, opt => { opt.MapFrom(s => s.postType); })
                          .ForMember(d => d.ProvinceID, opt => { opt.MapFrom(s => s.provinceID); })
                          .ForMember(d => d.ProvinceName, opt => { opt.MapFrom(s => s.provinceName); })
                          .ForMember(d => d.ReceiverName, opt => { opt.MapFrom(s => s.receiverName); })
                          .ForMember(d => d.ReceiverTelphone, opt => { opt.MapFrom(s => s.receiverTelphone); })
                          .ForMember(d => d.RefundAmout, opt => { opt.MapFrom(s => s.refundAmout); })
                          .ForMember(d => d.RefundDate, opt => { opt.MapFrom(s => s.refundDate); })
                          .ForMember(d => d.RefundID, opt => { opt.MapFrom(s => s.refundID); })
                          .ForMember(d => d.SendDate, opt => { opt.MapFrom(s => s.sendDate); })
                          .ForMember(d => d.States, opt => { opt.MapFrom(s => s.states); })
                          .ForMember(d => d.UserID, opt => { opt.MapFrom(s => s.userID); })
                          .ReverseMap();
        }

    }
}

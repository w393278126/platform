using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain.Order;

namespace Xn.Platform.Domain.Impl.AutoMappers
{
    public class OrderPlaneProfile : Profile
    {
        public OrderPlaneProfile()
        {


            CreateMap<OrderPlaneModel, OrderPlaneDTO>()
                            .ForMember(d => d.Id, opt => { opt.MapFrom(s => s.id); })
                            .ForMember(d => d.OrderId, opt => { opt.MapFrom(s => s.orderID); })
                            .ForMember(d => d.States, opt => { opt.MapFrom(s => s.states); })
                            .ForMember(d => d.StartAddr, opt => { opt.MapFrom(s => s.startAddr); })
                            .ForMember(d => d.StartAirport, opt => { opt.MapFrom(s => s.startAirport); })
                            .ForMember(d => d.StartDate, opt => { opt.MapFrom(s => s.startDate); })
                            .ForMember(d => d.DestAddr, opt => { opt.MapFrom(s => s.destAddr); })
                            .ForMember(d => d.DestAirport, opt => { opt.MapFrom(s => s.destAirport); })
                            .ForMember(d => d.DestDate, opt => { opt.MapFrom(s => s.destDate); })
                            .ForMember(d => d.CustName, opt => { opt.MapFrom(s => s.custName); })
                            .ForMember(d => d.IdCardNo, opt => { opt.MapFrom(s => s.idCardNo); })
                            .ForMember(d => d.PersonType, opt => { opt.MapFrom(s => s.personType); })
                            .ForMember(d => d.Airways, opt => { opt.MapFrom(s => s.airways); })
                            .ForMember(d => d.AirwaysIcon, opt => { opt.MapFrom(s => s.airwaysIcon); })
                            .ForMember(d => d.FlightNumber, opt => { opt.MapFrom(s => s.flightNumber); })
                            .ForMember(d => d.RoomType, opt => { opt.MapFrom(s => s.roomType); })
                            .ForMember(d => d.Price, opt => { opt.MapFrom(s => s.price); })
                            .ForMember(d => d.SalePrice, opt => { opt.MapFrom(s => s.salePrice); })
                            .ForMember(d => d.CancelReason, opt => { opt.MapFrom(s => s.cancelReason); })
                            .ForMember(d => d.ReturnFee, opt => { opt.MapFrom(s => s.returnFee); })
                            .ForMember(d => d.ReturnTotal, opt => { opt.MapFrom(s => s.returnTotal); })
                            .ForMember(d => d.ChannelID, opt => { opt.MapFrom(s => s.channelID); })
                            .ForMember(d => d.NoticeId, opt => { opt.MapFrom(s => s.noticeId); })
                            .ForMember(d => d.AddDate, opt => { opt.MapFrom(s => s.addDate); })
                            .ForMember(d => d.CostPrice, opt => { opt.MapFrom(s => s.costPrice); })
                            .ForMember(d => d.DesAirCode, opt => { opt.MapFrom(s => s.desAirCode); })
                            .ForMember(d => d.TripNo, opt => { opt.MapFrom(s => s.tripNo); })
                            .ForMember(d => d.DepAirCode, opt => { opt.MapFrom(s => s.depAirCode); })
                            .ForMember(d => d.PsgInfo, opt => { opt.MapFrom(s => s.psgInfo); })
                            .ForMember(d => d.SellsPrice, opt => { opt.MapFrom(s => s.sellsPrice); })
                            .ReverseMap();

            CreateMap<OrderPlaneRequest.RefundRequest, OrderPlaneModel>()
                   .ForMember(d => d.id, opt => { opt.MapFrom(s => s.Id); })
                   .ForMember(d => d.cancelReason, opt => { opt.MapFrom(s => s.CancelReason); })
                   .ForMember(d => d.returnFee, opt => { opt.MapFrom(s => s.ReturnFee); })
                   .ForMember(d => d.returnTotal, opt => { opt.MapFrom(s => s.ReturnTotal); })
                   .ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xn.Platform.Domain.Order;
using AutoMapper;

namespace Xn.Platform.Domain.Impl.AutoMappers
{
    public class OrderHotelProfile : Profile
    {
        public OrderHotelProfile()
        {
            CreateMap<OrderHotelModel, OrderHotelDTO>()
                        .ForMember(d => d.AddDate, opt => { opt.MapFrom(s => s.addDate); })
                        .ForMember(d => d.Adult, opt => { opt.MapFrom(s => s.adult); })
                        .ForMember(d => d.BookingID, opt => { opt.MapFrom(s => s.bookingID); })
                        .ForMember(d => d.BookingStates, opt => { opt.MapFrom(s => s.bookingStates); })
                        .ForMember(d => d.Breakfast, opt => { opt.MapFrom(s => s.breakfast); })
                        .ForMember(d => d.CancelAmount, opt => { opt.MapFrom(s => s.cancelAmount); })
                        .ForMember(d => d.CancelReason, opt => { opt.MapFrom(s => s.cancelReason); })
                        .ForMember(d => d.Channel, opt => { opt.MapFrom(s => s.channel); })
                        .ForMember(d => d.CheckInDate, opt => { opt.MapFrom(s => s.checkInDate); })
                        .ForMember(d => d.CheckOutDate, opt => { opt.MapFrom(s => s.checkOutDate); })
                        .ForMember(d => d.Children, opt => { opt.MapFrom(s => s.children); })
                        .ForMember(d => d.ChildrenAge, opt => { opt.MapFrom(s => s.childrenAge); })
                        .ForMember(d => d.CityID, opt => { opt.MapFrom(s => s.cityID); })
                        .ForMember(d => d.CityName, opt => { opt.MapFrom(s => s.cityName); })
                        .ForMember(d => d.ConfirmCode, opt => { opt.MapFrom(s => s.confirmCode); })
                        .ForMember(d => d.CostPrice, opt => { opt.MapFrom(s => s.costPrice); })
                        .ForMember(d => d.Credit, opt => { opt.MapFrom(s => s.credit); })
                        .ForMember(d => d.Currency, opt => { opt.MapFrom(s => s.currency); })
                        .ForMember(d => d.Email, opt => { opt.MapFrom(s => s.email); })
                        .ForMember(d => d.GuestRemarks, opt => { opt.MapFrom(s => s.guestRemarks); })
                        .ForMember(d => d.HotelID, opt => { opt.MapFrom(s => s.hotelID); })
                        .ForMember(d => d.HotelName, opt => { opt.MapFrom(s => s.hotelName); })
                        .ForMember(d => d.HotelPhone, opt => { opt.MapFrom(s => s.hotelPhone); })
                        .ForMember(d => d.Id, opt => { opt.MapFrom(s => s.id); })
                        .ForMember(d => d.Name, opt => { opt.MapFrom(s => s.name); })
                        .ForMember(d => d.Nationality, opt => { opt.MapFrom(s => s.nationality); })
                        .ForMember(d => d.Nights, opt => { opt.MapFrom(s => s.nights); })
                        .ForMember(d => d.OrderID, opt => { opt.MapFrom(s => s.orderID); })
                        .ForMember(d => d.Phone, opt => { opt.MapFrom(s => s.phone); })
                        .ForMember(d => d.RateCode, opt => { opt.MapFrom(s => s.rateCode); })
                        .ForMember(d => d.RoomCount, opt => { opt.MapFrom(s => s.roomCount); })
                        .ForMember(d => d.RoomName, opt => { opt.MapFrom(s => s.roomName); })
                        .ForMember(d => d.Rooms, opt => { opt.MapFrom(s => s.rooms); })
                        .ForMember(d => d.States, opt => { opt.MapFrom(s => s.states); })
                        .ForMember(d => d.TotalAmount, opt => { opt.MapFrom(s => s.totalAmount); })
                        .ForMember(d => d.UserID, opt => { opt.MapFrom(s => s.userID); })
                        .ReverseMap();

            CreateMap<OrderHotelRequest.RefundRequest, OrderHotelModel>()
                .ForMember(d => d.id, opt => { opt.MapFrom(s => s.Id); })
                .ForMember(d => d.cancelAmount, opt => { opt.MapFrom(s => s.CancelAmount); })
                .ForMember(d => d.cancelReason, opt => { opt.MapFrom(s => s.CancelReason); })
                .ForMember(d => d.confirmCode, opt => { opt.MapFrom(s => s.ConfirmCode); })
                .ReverseMap();

        }
    }
}

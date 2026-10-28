using AutoMapper;
using ECommerce.Application.DTO.Order;
using ECommerce.Domain.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(X => X.DeliveryMethod, o => o.MapFrom(X => X.DeliveryMethod.ShortName))
                .ForMember(X => X.DeliveryMethodCost, o => o.MapFrom(X => X.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(X => X.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(X => X.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(X => X.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());


            CreateMap<DeliveryMethod, DeliveryMethodDTO>();







        }
    }
}

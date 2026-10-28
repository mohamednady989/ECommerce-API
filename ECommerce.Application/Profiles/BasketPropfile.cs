using AutoMapper;
using ECommerce.Application.DTO.Baskets;
using ECommerce.Domain.Entites.Basket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Profiles
{
    public class BasketPropfile:Profile
    {
        public BasketPropfile()
        {
            CreateMap<CustomerBasket,BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

        }
    }
}

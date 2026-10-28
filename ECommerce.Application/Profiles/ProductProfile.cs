using AutoMapper;
using ECommerce.Application.DTO.Products;
using ECommerce.Domain.Entites.Prodects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Profiles
{
    public  class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductBrand, opt=>opt.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name));

        }
    }
}

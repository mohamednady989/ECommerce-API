using AutoMapper;
using AutoMapper.Execution;
using ECommerce.Application.DTO.Order;
using ECommerce.Domain.Entites.Order;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Profiles
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly UrlSetting settings;

        public OrderItemPictureUrlResolver(IOptions<UrlSetting> options)
        {
            this.settings = options.Value;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            var baseUrl = settings.BaseUrl?.TrimEnd('/') ?? string.Empty;
            var picturePath = source?.Product?.PictureUrl?.TrimStart('/');
            if (string.IsNullOrEmpty(picturePath))
            {
                return null;
            }

            return string.IsNullOrEmpty(baseUrl)
                ? $"/Files/{picturePath}"
                : $"{baseUrl}/Files/{picturePath}";
        }
    }
    public class UrlSetting
    {
        public string BaseUrl { get; set; }
    }
}

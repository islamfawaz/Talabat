using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;

internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _configuration;

    public OrderItemPictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
            return $"{_configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";

        return string.Empty;
    }
}

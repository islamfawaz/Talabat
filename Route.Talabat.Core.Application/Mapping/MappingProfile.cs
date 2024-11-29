using AutoMapper;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Application.Mapping;
using Route.Talabat.Core.Domain.Entities.Basket;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Core.Domain.Entities.Products;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping between ProductBrand and BrandDto
        CreateMap<ProductBrand, BrandDto>();

        // Mapping between ProductCategory and CategoryDto
        CreateMap<ProductCategory, CategoryDto>();

        // Mapping between Product and ProductReturnDto
        CreateMap<Product, ProductReturnDto>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand!.Name))  // Map the Brand name to the Brand property
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category!.Name))  // Map the Category name to the Category property
            .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>()); // Use a custom resolver to get the Product picture URL

        // Mapping between CustomerBasket and CustomerBasketDto (with reverse mapping)
        CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

        // Mapping between BasketItem and BasketItemDto (with reverse mapping)
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        // Mapping between Order and OrderToReturnDto
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod!.ShortName)) // Map DeliveryMethod to ShortName
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items ?? new List<OrderItem>())); // If Items is null, return an empty list

        // Mapping between OrderItem and OrderItemDto
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>()); // Use a custom resolver to get the OrderItem picture URL

        // Mapping between Order and OrderToReturnDto (redundant mapping, similar to above)
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod!.ShortName)) // Map DeliveryMethod to ShortName
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items ?? new List<OrderItem>())); // Handle null Items

        // Mapping between OrderItem to itself (OrderItem -> OrderItem)
        CreateMap<OrderItem, OrderItem>()
            .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.ProductId)) // Map ProductId to ProductId
            .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ProductName)); // Map ProductName to ProductName

        // Mapping between DeliveryMethod and DeliveryMethodDto
        CreateMap<DeliveryMethod, DeliveryMethodDto>();
    }
}

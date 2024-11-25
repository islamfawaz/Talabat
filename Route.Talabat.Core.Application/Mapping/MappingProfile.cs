using AutoMapper;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Application.Abstraction.Order.Models;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Entities.Basket;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Core.Domain.Entities.Products;

namespace Route.Talabat.Core.Application.Mapping
{
    internal class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<Product, ProductReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand!.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category!.Name))
               // .ForMember(d=>d.PictureUrl,o=>o.MapFrom(s=> $"{s.PictureUrl}"));
               .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>().ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod!.ShortName));


            CreateMap<OrderItem, OrderItem>().ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.ProductId));
            CreateMap<OrderItem, OrderItem>().ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ProductName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

        }
    }
}

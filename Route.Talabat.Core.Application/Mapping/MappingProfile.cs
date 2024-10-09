﻿using AutoMapper;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }
    }
}

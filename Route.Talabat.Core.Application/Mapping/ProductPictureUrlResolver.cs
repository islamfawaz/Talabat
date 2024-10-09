using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Mapping
{
    internal class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductReturnDto, string?>
    {
        private readonly IConfiguration configuration = configuration;

        public string? Resolve(Product source, ProductReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl)) 
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";

            return string.Empty;
        }
    }
}

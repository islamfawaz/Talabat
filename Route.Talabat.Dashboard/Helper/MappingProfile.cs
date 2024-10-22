using AutoMapper;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Dashboard.Models;

namespace Route.Talabat.Dashboard.Helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}

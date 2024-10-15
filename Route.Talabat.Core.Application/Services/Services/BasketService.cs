using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Application.Abstraction.Basket;
using Route.Talabat.Application.Abstraction.Basket.Models;
using Route.Talabat.Core.Domain.Contract.Infrastructure;
using Route.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Route.Talabat.Controllers.Exception;

namespace Route.Talabat.Core.Application.Services.Services
{
    internal class BasketService(IBasketRepository basketRepo, IMapper mapper, IConfiguration configuration) : IBasketService
    {
        private readonly IBasketRepository basketRepo = basketRepo;
        private readonly IMapper mapper = mapper;

        public async Task DeleteCustomerBasketAsync(string basketId)
        {
           var deleted= await basketRepo.DeleteAsync(basketId);
            if (!deleted)
                throw new Exception();

                
        }

        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket=await basketRepo.GetAsync(basketId);

            if (basket is null)
            {
                throw new Exception();
            }
            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);

            var timeSpan = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSetting")["TimeToLiveIn"]!));

            var updatedBasket = await basketRepo.UpdateAsync(basket, timeSpan);
            if(updatedBasket is null) throw new Exception();

            return basketDto;
        }

    }
}

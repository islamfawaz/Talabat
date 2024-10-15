using Route.Talabat.Core.Domain.Contract.Infrastructure;
using Route.Talabat.Core.Domain.Entities.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Basket_Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer radis)
        {
            _database = radis.GetDatabase();
        }

        public async Task<bool> Delete(string id)
        {
            var deleted = await _database.KeyDeleteAsync(id);
            return deleted;
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket=await _database.StringGetAsync(id);

            return basket.IsNullOrEmpty ?null :JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(basket);
            var updated = await _database.StringSetAsync(basket.Id, value,timeToLive);

            if (updated)
                return basket;
            return null;
        }
    }
}

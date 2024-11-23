using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Contract.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.OrderAggregate;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Infrastructure.Persistance.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Data
{
    internal sealed class StoreDbInitializer :DbInitializer, IStoreDbInitializer
    {
        private readonly StoreDbContext _dbcontext;

        public StoreDbInitializer(StoreDbContext dbcontext):base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
     

        public override async Task SeedAsnc()
        {
            if (!_dbcontext.Brands.Any())
            {

                var brandData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/_Data/Seeds/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands?.Count > 0)
                {
                    await _dbcontext.Brands.AddRangeAsync(brands);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.Categories.Any())
            {

                var categoriesData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/_Data/Seeds/categories.json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);


                if (categories?.Count > 0)
                {
                    await _dbcontext.Categories.AddRangeAsync(categories);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.Products.Any())
            {

                var productsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/_Data/Seeds/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _dbcontext.Products.AddRangeAsync(products);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.Products.Any())
            {

                var deliveryMethodData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/_Data/Seeds/delivery.json");

                var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodData);

                if (deliveryMethod?.Count > 0)
                {
                    await _dbcontext.DeliveryMethods.AddRangeAsync(deliveryMethod);
                    await _dbcontext.SaveChangesAsync();
                }
            }

        }
    }
}

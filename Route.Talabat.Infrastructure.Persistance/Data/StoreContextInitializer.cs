using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistance.Data
{
    internal class StoreContextInitializer : IStoreContextInitializer
    {
        private readonly StoreContext _dbcontext;

        public StoreContextInitializer(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task InitializerAsync()
        {
            var pendingMigration = _dbcontext.Database.GetPendingMigrations();
            if (pendingMigration.Any())
                await _dbcontext.Database.MigrateAsync();
        }

        public async Task SeedAsnc()
        {
            if (!_dbcontext.Brands.Any())
            {

                var brandData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/Data/Seeds/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands?.Count > 0)
                {
                    await _dbcontext.Brands.AddRangeAsync(brands);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.Categories.Any())
            {

                var categoriesData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/Data/Seeds/categories.json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);


                if (categories?.Count > 0)
                {
                    await _dbcontext.Categories.AddRangeAsync(categories);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.Products.Any())
            {

                var productsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/Data/Seeds/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _dbcontext.Products.AddRangeAsync(products);
                    await _dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}

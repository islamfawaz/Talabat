using Route.Talabat.Core.Domain.Entities.Products;
using System.Text.Json;

namespace Route.Talabat.Infrastructure.Persistance.Data
{
    public class StoreContextSeed
    {
       public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.Brands.Any())
            {

                var brandData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/Data/Seeds/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands?.Count > 0)
                {
                    await dbContext.Brands.AddRangeAsync(brands);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())
            {

                var categoriesData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/Data/Seeds/categories.json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);


                if (categories?.Count > 0)
                {
                    await dbContext.Categories.AddRangeAsync(categories);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {

                var productsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistance/Data/Seeds/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await dbContext.Products.AddRangeAsync(products);
                    await dbContext.SaveChangesAsync();
                }
            }
    }
    }
}

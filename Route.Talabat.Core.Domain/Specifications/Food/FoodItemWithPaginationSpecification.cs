using Route.Talabat.Core.Domain.Entities.Food;
using System;

namespace Route.Talabat.Core.Domain.Specifications.Food
{
    public class FoodItemWithPaginationSpecification : BaseSpecifications<FoodItem, int>
    {
        public FoodItemWithPaginationSpecification(string? sort, int pageSize, int pageIndex, string? search)
            : base(
                f => (string.IsNullOrEmpty(search) || f.NameFood.Contains(search)) 
                  
            )
        {
            AddInclude();
            AddOrderBy(f => f.NameFood);  // By default order by name

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "NameDesc":
                        AddOrderByDesc(f => f.NameFood);
                        break;
        
                    default:
                        AddOrderBy(f => f.NameFood);
                        break;
                }
            }

            // Apply pagination: skip is calculated by pageSize and pageIndex
            AddPagination(pageSize * (pageIndex - 1), pageSize);
            Console.WriteLine($"Skip: {pageSize * (pageIndex - 1)}, Take: {pageSize}");


        }

        public FoodItemWithPaginationSpecification(int id) : base(id)
        {
            AddInclude();
        }

        private protected override void AddInclude()
        {
            base.AddInclude();
        }
    }
}

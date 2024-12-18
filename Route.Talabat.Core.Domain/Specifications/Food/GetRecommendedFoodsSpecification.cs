using Route.Talabat.Core.Domain.Entities.Food;

namespace Route.Talabat.Core.Domain.Specifications.Food
{
    public class GetRecommendedFoodsSpecification : BaseSpecifications<ClassifiedFood, int>
    {
        // Constructor to specify criteria for recommended foods for a specific user
        public GetRecommendedFoodsSpecification(int userId, int numberOfRecommendations)
            : base(food => food.UserId != userId) // Exclude foods the user has rated
        {
            AddOrderBy(f => f.Rating);          // Sort by rating in descending order
            AddPagination(0, numberOfRecommendations); // Add pagination based on number of recommendations
        }
    }
}

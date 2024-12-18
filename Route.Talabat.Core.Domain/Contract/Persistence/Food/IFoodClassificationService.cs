using Route.Talabat.Core.Domain.Entities.Food;

namespace Route.Talabat.Core.Domain.Contract.Persistence.Food
{
    public interface IFoodClassificationService
    {
        void TrainAndClassify(string filePath);
        List<FoodItem> RecommendFoods(int userId, int numberOfRecommendations);
        Task ImportClassifiedFoodsAsync(string filePath);

        Task<List<ClassifiedFood>> GetRecommendationsAsync(int userId, int numberOfRecommendations);

    }
}

using Route.Talabat.Application.Abstraction.Food.Models;

namespace Route.Talabat.Application.Abstraction.Food
{
    public interface IFoodUserService
    {
        Task AddRatingAsync(FoodRatingDto rating);
        Task AddToFavoritesAsync(FavoriteDto favorite);
        Task<List<FavoriteDto>> GetFavoritesAsync(string userId);
    }

}

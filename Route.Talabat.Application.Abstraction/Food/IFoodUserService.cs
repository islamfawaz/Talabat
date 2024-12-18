using Route.Talabat.Application.Abstraction.Food.Models;

public interface IFoodUserService
{
    Task AddRatingAsync(FoodRatingDto rating);
    Task<FoodItemDto> GetFoodItemByIdAsync(int foodId);
    Task<FoodItemDto> AddFoodItemFromCsvAsync(int foodId);
    Task ImportAllFoodItemsFromCsvAsync();

    Task<List<FoodItemDto>> GetFoodItemsWithPaginationAsync(int pageIndex, int pageSize, string sort = null, string search = null);


}

using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction.Food;
using Route.Talabat.Application.Abstraction.Food.Models;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Core.Domain.Entities.Food;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Food
{
    public class FoodUserController : ApiControllerBase
    {
        private readonly IFoodUserService _foodUserService;

        public FoodUserController(IFoodUserService foodUserService)
        {
            _foodUserService = foodUserService;
        }

        // Endpoint to add rating to food item
        [HttpPost("add-rating")]
        public async Task<IActionResult> AddRatingAsync([FromBody] FoodRatingDto rating)
        {
            if (rating == null)
                return BadRequest("Invalid rating data.");

            await _foodUserService.AddRatingAsync(rating);
            return Ok("Rating added successfully.");
        }

        // Endpoint to add food item to favorites
        [HttpPost("add-to-favorites")]
        public async Task<IActionResult> AddToFavoritesAsync([FromBody] FavoriteDto favorite)
        {
            if (favorite == null)
                return BadRequest("Invalid favorite data.");

            await _foodUserService.AddToFavoritesAsync(favorite);
            return Ok("Food item added to favorites.");
        }

        // Endpoint to get all favorites for a user
        [HttpGet("get-favorites/{userId}")]
        public async Task<IActionResult> GetFavoritesAsync(string userId)
        {
            var favoriteDtos = await _foodUserService.GetFavoritesAsync(userId);

            if (favoriteDtos == null || !favoriteDtos.Any())
                return NotFound("No favorites found for this user.");

            return Ok(new
            {
                Message = "Favorites retrieved successfully.",
                Favorites = favoriteDtos
            });
        }
    }
}

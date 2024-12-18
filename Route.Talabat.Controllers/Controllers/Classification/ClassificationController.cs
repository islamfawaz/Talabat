using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Domain.Contract.Persistence.Food;
using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Controllers.Controllers.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Talabat.Controllers.Controllers.Food
{
    public class ClassificationController : ApiControllerBase
    {
        private readonly IFoodClassificationService _classificationService;

        public ClassificationController(IFoodClassificationService classificationService)
        {
            _classificationService = classificationService;
        }

        // Endpoint to classify food data
        [HttpPost("classify")]
        public IActionResult ClassifyData()
        {
            try
            {
                string fileName = "Dataset_for_print.csv"; // File name in wwwroot
                _classificationService.TrainAndClassify(fileName); // Pass the file name here only
                return Ok("Classification completed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error during classification.", Error = ex.Message });
            }
        }

        // Endpoint to get food recommendations based on user ID and count
        [HttpGet("recommend/{userId}")]
        public IActionResult GetRecommendationsForUser(int userId, [FromQuery] int count = 1)
        {
            try
            {
                var recommendations = _classificationService.RecommendFoods(userId, count);

                if (recommendations.Any())
                {
                    return Ok(new
                    {
                        Message = "Recommendations generated successfully.",
                        Recommendations = recommendations.Select(r => new
                        {
                            r.NameFood,
                            r.Ingredients,
                            r.Nutrients,
                            r.Rating
                        })
                    });
                }
                else
                {
                    return Ok(new { Message = "No recommendations available for this user." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error generating recommendations.", Error = ex.Message });
            }
        }

        // Endpoint to import classified food data
        [HttpPost("import-classified-foods")]
        public async Task<IActionResult> ImportClassifiedFoods()
        {
            try
            {
                string fileName = "Dataset_for_print.csv";
                await _classificationService.ImportClassifiedFoodsAsync(fileName);
                return Ok("Classified foods imported successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error during import.", Error = ex.Message });
            }
        }

        // Endpoint to get recommendations with async method
        [HttpGet("recommendations/{userId}")]
         public   ActionResult<List<FoodItem>> Recommendations(int userId, [FromQuery] int numberOfRecommendations)
        {
            try
            {
                var recommendations =  _classificationService.RecommendFoods(userId, numberOfRecommendations);
                return Ok(recommendations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }
    }
}

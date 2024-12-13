using Route.Talabat.Core.Domain.Contract.Persistence.Food;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Controllers.Controllers.Base;
using Route.Talabat.Core.Domain.Entities.Food;

namespace Route.Talabat.Controllers.Controllers.Food
{
    public class ClassificationController : ApiControllerBase
    {
        private readonly IFoodClassificationService _classificationService;

        public ClassificationController(IFoodClassificationService classificationService)
        {
            _classificationService = classificationService;
        }

        [HttpPost("classify")]
        public IActionResult ClassifyData()
        {
            // تحديد اسم الملف
            string fileName = "Dataset_for_print.csv"; // اسم الملف داخل wwwroot
            _classificationService.TrainAndClassify(fileName); // تمرير اسم الملف هنا فقط
            return Ok("Classification Completed");
        }


        [HttpGet("recommend/{userId}")]
        public IActionResult GetRecommendations(int userId, [FromQuery] int count = 1)
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



        [HttpPost("import-classified-foods")]
        public async Task<IActionResult> ImportClassifiedFoods()
        {
            string fileName = "Dataset_for_print.csv";
            await _classificationService.ImportClassifiedFoodsAsync(fileName);
            return Ok("Classified foods imported successfully.");
        }


      



    }
}

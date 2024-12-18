using Microsoft.ML.Data;

namespace Route.Talabat.Infrastructure.Persistance.Models
{
    public class FoodPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedLabel { get; set; }

        public float Probability { get; set; }
        public float Score { get; set; }
    }
}

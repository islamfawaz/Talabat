namespace Route.Talabat.Application.Abstraction.Food.Models
{
    public class FoodItemDto
    {
        public int ItemId { get; set; }
        public string NameFood { get; set; }
        public float Rating { get; set; }
        public string Ingredients { get; set; }
        public string Nutrients { get; set; }
        public string LinkImage { get; set; }
        public string LinkFood { get; set; }
    }
}

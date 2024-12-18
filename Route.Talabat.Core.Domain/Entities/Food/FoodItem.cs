using CsvHelper.Configuration.Attributes;

namespace Route.Talabat.Core.Domain.Entities.Food
{
    public class FoodItem : BaseAuditableEntity<int>
    {
        [Name("itemID")]
        public int ItemId { get; set; }

        [Name("userID")]
        public int UserId { get; set; }

        [Name("name_food")]
        public string NameFood { get; set; }

        [Name("rating")]
        public float Rating { get; set; }

        [Name("link_image_food")]
        public string LinkImage { get; set; }

        [Name("link_food")]
        public string LinkFood { get; set; }

        [Name("ingredients")]
        public string Ingredients { get; set; }

        [Name("nutrients")]
        public string Nutrients { get; set; }

        [Name("review_user")]
        public string ReviewUser { get; set; }

        public bool Label { get; set; } // This is writable for training purposes
    }
}

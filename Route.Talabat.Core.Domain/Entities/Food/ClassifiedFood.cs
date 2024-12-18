
namespace Route.Talabat.Core.Domain.Entities.Food
{
    public class ClassifiedFood : BaseAuditableEntity<int>
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string NameFood { get; set; }
        public float Rating { get; set; }
        public string Ingredients { get; set; }
        public string Nutrients { get; set; }
        public string LinkImage { get; set; }
        public string LinkFood { get; set; }
        public bool IsRecommended { get; set; }
    }
}

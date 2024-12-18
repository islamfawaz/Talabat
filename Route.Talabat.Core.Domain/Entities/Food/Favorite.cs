using Route.Talabat.Core.Domain.Entities.Food;

public class Favorite : BaseAuditableEntity<int>
{
    public int UserId { get; set; }  // تعديل UserId ليكون من نوع int
    public int FoodId { get; set; }
    public virtual FoodItem Food { get; set; }
}

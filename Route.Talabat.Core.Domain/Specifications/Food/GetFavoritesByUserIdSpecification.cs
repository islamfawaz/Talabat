using Route.Talabat.Core.Domain.Specifications;

public class GetFavoritesByUserIdSpecification : BaseSpecifications<Favorite, int>
{
    public GetFavoritesByUserIdSpecification(int userId)
        : base(favorite => favorite.UserId == userId)
    {
    }

    // يمكنك إضافة أي خصائص إضافية في التصفية هنا
    private protected override void AddInclude()
    {
        base.AddInclude();
    }
}

using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Core.Domain.Specifications;

namespace Route.Talabat.Core.Domain.Specifications.Food
{
    public class GetFavoritesByUserIdSpecification : BaseSpecifications<Favorite, int>
    {
        // Constructor لتصفية الـ Favorites حسب الـ userId
        public GetFavoritesByUserIdSpecification(string userId)
            : base(favorite => favorite.UserId == userId)
        {
        }

        // يمكنك إضافة أي خصائص إضافية في التصفية هنا
        private protected override void AddInclude()
        {
            base.AddInclude();
            
        }
    }
}

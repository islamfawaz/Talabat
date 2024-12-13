using AutoMapper;
using Route.Talabat.Application.Abstraction.Food;
using Route.Talabat.Application.Abstraction.Food.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Food;
using Route.Talabat.Core.Domain.Specifications.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Food
{
    public class FoodUserService : IFoodUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FoodUserService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddRatingAsync(FoodRatingDto rating)
        {
            var ratingToCreate = _mapper.Map<FoodRating>(rating);

            await _unitOfWork.GetRepository<FoodRating, int>().AddAsync(ratingToCreate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddToFavoritesAsync(FavoriteDto favorite)
        {
            var favoriteToCreate= _mapper.Map<Favorite>(favorite);

            await _unitOfWork.GetRepository<Favorite, int>().AddAsync(favoriteToCreate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<FavoriteDto>> GetFavoritesAsync(string userId)
        {
            var specification = new GetFavoritesByUserIdSpecification(userId);

            var favorites = await _unitOfWork.GetRepository<Favorite, int>()
                                              .GetAllAsyncWithSpec(specification);

             var favoriteDtos = _mapper.Map<List<FavoriteDto>>(favorites);

            return favoriteDtos;
        }


    }
}

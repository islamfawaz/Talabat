using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;

namespace Route.Talabat.Dashboard.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUserService _loggedUserService;

        public BrandController(IUnitOfWork unitOfWork ,ILoggedUserService loggedUserService)
        {
            _unitOfWork = unitOfWork;
            _loggedUserService = loggedUserService;
        }

        // GET: Brand/Index
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return View(brands);
        }

        // POST: Brand/Create
        [HttpPost]
        public async Task<JsonResult> Create(string Name) 
        {
            var productBrand = new ProductBrand
            {
                Name = Name,
                CreatedBy = _loggedUserService.UserId, 
                LastModifiedBy = _loggedUserService.UserId 
            };

            if (string.IsNullOrEmpty(productBrand.Name) || productBrand.Name.Length > 100)
            {
                return Json(new { success = false, message = "Invalid data: Brand name is required and can't be longer than 100 characters." });
            }

            try
            {
                // Add the new brand to the repository
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(productBrand);
                await _unitOfWork.CompleteAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Brand/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var productBrand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);
            if (productBrand == null)
            {
                return Json(new { success = false, message = "Brand not found." });
            }

            try
            {
                _unitOfWork.GetRepository<ProductBrand, int>().Delete(productBrand);
                await _unitOfWork.CompleteAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

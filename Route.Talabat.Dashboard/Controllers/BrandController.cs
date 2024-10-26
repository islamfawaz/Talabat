using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;

namespace Route.Talabat.Dashboard.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Brand/Index
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return View(brands);
        }

        // POST: Brand/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductBrand productBrand)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Please Enter a valid Name");
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }

            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(productBrand);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                ModelState.AddModelError("", $"An error occurred while creating the brand: {ex.Message}");
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }
        }

        // POST: Brand/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the brand by ID
            var productBrand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.GetRepository<ProductBrand, int>().Delete(productBrand);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                 ModelState.AddModelError("", $"An error occurred while deleting the brand: {ex.Message}");
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }
        }
    }
}

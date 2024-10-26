using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Specifications.Products;
using Route.Talabat.Dashboard.Models;
using Route.Talabat.Dashboard.Models.Route.Talabat.Dashboard.Models;

namespace Route.Talabat.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUserService _loggedUserService;

        public ProductController(IUnitOfWork unitOfWork ,IMapper mapper ,ILoggedUserService loggedUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedUserService = loggedUserService;
        }

        public async Task<IActionResult> Index(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex, specParams.Search);

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsyncWithSpec(spec);

            var totalItem = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(new ProductWithBrandCategorySpecifications(
                specParams.Sort, specParams.BrandId, specParams.CategoryId, int.MaxValue, 1, specParams.Search
            ));

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            var viewModel = new PaginatedProductViewModel
            {
                Products = mappedProducts,
                PageIndex = specParams.PageIndex,
                PageSize = specParams.PageSize,
                TotalCount = totalItem
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync(), "Id", "Name");

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model);

                // set the NormalizedName
                product.NormalizedName = model.Name.ToUpperInvariant(); 

                // Set created and modified by properties
                product.CreatedBy = _loggedUserService.UserId;
                product.LastModifiedBy = _loggedUserService.UserId;

                // Save image 
                product.PictureUrl = model.Image != null ? await SaveImage(model.Image) : null;

                // Save the product
                await _unitOfWork.GetRepository<Product, int>().AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }


            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync(), "Id", "Name");

            return View(model);
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/" + fileName; 
        }



    }
}

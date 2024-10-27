using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Route.Talabat.Application.Abstraction.Abstraction;
using Route.Talabat.Application.Abstraction.Products.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Specifications.Products;
using Route.Talabat.Dashboard.Helper;
using Route.Talabat.Dashboard.Models;
using Route.Talabat.Dashboard.Models.Route.Talabat.Dashboard.Models;

namespace Route.Talabat.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUserService _loggedUserService;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, ILoggedUserService loggedUserService)
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

        #region Helper
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
        #endregion

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync(), "Id", "Name");

            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product!);
            mappedProduct.PictureUrl = product?.PictureUrl;

            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    // Delete old image if PictureUrl exists
                    if (!string.IsNullOrEmpty(model.PictureUrl))
                    {
                        var fileName = Path.GetFileName(model.PictureUrl);
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            PictureSettings.DeleteFile("products", fileName);
                        }
                    }

                    // Upload new image and set PictureUrl
                    model.PictureUrl = PictureSettings.UploadFile(model.Image!, "products");
                }
                else if (string.IsNullOrEmpty(model.PictureUrl))
                {
                    ModelState.AddModelError("PictureUrl", "Image is required.");
                    // Refresh dropdown lists for model errors
                    ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
                    ViewBag.Categories = new SelectList(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync(), "Id", "Name");
                    return View(model);
                }

                var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);
                mappedProduct.CreatedBy = _loggedUserService.UserId;
                mappedProduct.LastModifiedBy = _loggedUserService.UserId;
                mappedProduct.NormalizedName = model.Name.ToUpperInvariant();

                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);

                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            // Refill dropdown lists 
            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync(), "Id", "Name");

            return View(model);
        }




        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct=_mapper.Map<Product, ProductViewModel>(product!);

            return View(mappedProduct);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound();
                }

                var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                 if (!string.IsNullOrEmpty(product.PictureUrl))
                {
                    var fileName = Path.GetFileName(product.PictureUrl);
                    PictureSettings.DeleteFile("products", fileName);
                }

                 _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                
                 ModelState.AddModelError("", "An error occurred while deleting the product.");
                return View("Error");  
            }
        }

    }
}

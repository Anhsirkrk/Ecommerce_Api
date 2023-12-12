using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace Ecommerce_Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    public class AdminController : ControllerBase 

    {
        private readonly IAdminRepository _iar;
        private readonly EcommerceDailyPickContext _context;
        private readonly ILogger _logger;

        private readonly DatabaseLogger _databaseLogger;


        public AdminController(IAdminRepository iar, EcommerceDailyPickContext context, ILogger logger, DatabaseLogger databaselogger)
        {
            _context = context;
            _iar = iar;
            _logger = _logger;
            _databaseLogger = databaselogger;
            context.Database.SetCommandTimeout(120);

        }
        [HttpGet]
        [Route("GenerateRandomKey")]
        public string GenerateRandomKey(int lengthInBytes)
        {
            byte[] keyBytes = new byte[lengthInBytes];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }

        //Category
        [Authorize]
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm] IFormFile image, [FromForm] CategoryViewModel ACVM)

        {
            try
            {
                if (_context != null)
                {
                    var CreatingCategory = await _iar.CreateCategory(image, ACVM);
                    if(CreatingCategory != null)
                    {
                        return Ok(CreatingCategory);
                    }
                    else
                    {
                        BadRequest(CreatingCategory);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                _databaseLogger.SetUserId(ACVM.CategoryName); // Set the userId in the DatabaseLogger
                _logger.LogError(ex, "An error occurred while creating category");
                return BadRequest("an error occured while logging in ");
            }
            return null;
        }

        [Authorize]
        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<CategoryDetailsWithImage_> GetCategoryById(int category_id)
        {
            try
            {
                return await _iar.GetCategoryById(category_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetDetailsAndImagesOfCategories")]
        public async Task<IActionResult> GetDetailsAndImagesOfCategories()
        {
            try
            {
                if (_context != null)
                {
                    var categorieddetails = await _iar.GetDetailsAndImagesOfCategories();
                    if (categorieddetails != null)
                    {
                        return Ok(categorieddetails);
                    }
                    else
                    {
                        return BadRequest("details not found");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }
        [Authorize(policy: "AdminOnly")]
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<CategoryViewModel> UpdateCategory([FromForm] IFormFile image, [FromForm] CategoryViewModel UCVM)
        {
            try
            {
                if (UCVM != null)
                {
                    return await _iar.UpdateCategory(image, UCVM);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<string> DeleteCategory(int category_id)
        {
            try
            {
                return await _iar.DeleteCategory(category_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPut]
        //[Route("UploadCategoryImage")]
        //public async Task<Category> UploadCategoryImage(int category_id, IFormFile imagefile)
        //{
        //    try
        //    {
        //        if (category_id != null)
        //        {
        //            return await _iar.UploadCategoryImage(category_id, imagefile);
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //brand


        //brand

     
        [HttpPost]
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand([FromForm] IFormFile Image, [FromForm] BrandViewModel bvm)
        {
            try
            {
                if (_context != null)
                {
                    var creatingbrand = await _iar.CreateBrand(Image, bvm);
                    return Ok(creatingbrand);
                }
                else
                {
                    bvm.resultMessage = "Brand Not created";
                    bvm.IsBrandCreated = false;
                    return NotFound(bvm);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("GetDetailsAndImagesOfBrands")]
        public async Task<IActionResult> GetDetailsAndImagesOfBrands()
        {
            try
            {
                if (_context != null)
                {
                    var branddetails = await _iar.GetDetailsAndImagesOfBrands();
                    if (branddetails != null)
                    {
                        return Ok(branddetails);
                    }
                    else
                    {
                        return BadRequest("details not found");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }
        [Authorize]
        [HttpDelete]
        [Route("Deletebrand")]
        public async Task<string> Deletebrand(int brand_id)
        {
            try
            {
                return await _iar.DeleteBrand(brand_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateBrand")]
        public async Task<BrandViewModel> UpdateBrand([FromForm] IFormFile image, [FromForm] BrandViewModel bvm)
        {
            try
            {
                if (bvm != null)
                {
                    return await _iar.UpdateBrand(image, bvm);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetBrandById")]
        public async Task<BrandViewModel> GetBrandById(int brand_id)
        {
            try
            {
                return await _iar.GetBrandById(brand_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //product
        [Authorize]
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ProductViewModel> CreateProduct([FromForm] IFormFile Image, [FromForm] ProductViewModel APVM)
        {
            try
            {
                if (APVM != null)
                {
                    return await _iar.CreateProduct(Image, APVM);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<string> DeleteProduct(int product_id)
        {
            try
            {
                return await _iar.DeleteProduct(product_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] IFormFile image, [FromForm] ProductViewModel UPVM)
        {
            try
            {
                if (UPVM != null)
                {
                    var updateproduct = await _iar.UpdateProduct(image, UPVM);
                    return Ok(updateproduct);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<List<ProductViewModel>> GetAllProductswithImage()
        {
            try
            {
                if (_context != null)
                {
                    var productIds = await _context.Products.Select(x => x.ProductId).ToListAsync();
                    var productlist = new List<ProductViewModel>();

                    foreach (var id in productIds)
                    {
                        var productData = await GetProductDataAsync(id);
                        if (productData != null)
                        {
                            productlist.Add(productData);
                        }
                    }

                    return productlist;
                }

                return null;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }


        //public async Task<List<ProductViewModel>> GetAllProductswithImage()
        //{
        //    try
        //    {
        //        return await _iar.GetAllProductswithImage();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [Authorize]
        [HttpPost]
        [Route("GetProductById")]
        public async Task<List<Product>> GetProductById(List<int> product_ids)
        {
            try
            {
                return await _iar.GetProductById(product_ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetSubscriptionTypes")]
        public async Task<IActionResult> GetSubscriptiontypes()
        {
            try
            {
                var susbcriptionttypes = await _iar.GetSubscriptiontypes();
                return Ok(susbcriptionttypes);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }







        private async Task<ProductViewModel> GetProductDataAsync(int productId)
        {
            try
            {
                if (_context != null)
                {
                    var product = await _context.Products
                        .Include(p => p.ProductItemDetails)
                        .Include(p => p.Category)
                        .Include(p => p.Brand)
                        .FirstOrDefaultAsync(x => x.ProductId == productId);

                    if (product != null)
                    {
                        // Convert image to base64
                        string base64Image = ConvertImageToBase64(product.ImageUrl);

                        var productViewModel = new ProductViewModel
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            CategoryId = product.CategoryId,
                            CategoryName = product.Category.CategoryName,
                            BrandId = product.BrandId,
                            BrandName = product.Brand.BrandName,
                            Base64Image = base64Image,
                            Unit = product.ProductItemDetails.FirstOrDefault()?.Unit,
                            SizeOfEachUnits = product.ProductItemDetails.Select(item => (decimal)item.SizeOfEachUnit).ToList(),
                            WeightOfEachUnits = product.ProductItemDetails.Select(item => (decimal)item.WeightOfEachUnit).ToList(),
                            StockOfEachUnits = product.ProductItemDetails.Select(item => (decimal)item.StockOfEachUnit).ToList(),
                            PriceOfEachUnits = product.ProductItemDetails.Select(item => (decimal)item.Price).ToList(),
                            MFG_OfEachUnits = product.ProductItemDetails.Select(item => (DateTime)item.ManufactureDate).ToList(),
                            EXP_OfEachUnits = product.ProductItemDetails.Select(item => (DateTime)item.ExpiryDate).ToList(),
                            IsAvailable_OfEachUnit = product.ProductItemDetails.Select(item => (bool)item.IsAvailable).ToList(),
                            Avaialble_Quantity_ofEachUnit = product.ProductItemDetails.Select(item => (decimal)item.AvailableQuantity).ToList(),
                            Description_OfEachUnits = product.ProductItemDetails.Select(item => item.Description).ToList(),
                            DiscountId_OfEachUnit = product.ProductItemDetails.Select(item => (int)item.DiscountId).ToList()
                        };

                        return productViewModel;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }

        private string ConvertImageToBase64(string imageUrl)
        {
            try
            {
                using (var image = System.Drawing.Image.FromFile(imageUrl))
                {
                    ImageFormat format = image.RawFormat;
                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, format);
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return null;
            }
        }
    }

}


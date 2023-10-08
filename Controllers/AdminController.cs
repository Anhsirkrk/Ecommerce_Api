using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.IO;
using System.Drawing.Imaging;


namespace Ecommerce_Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _iar;
        private readonly EcommercedemoContext _context;

        public AdminController(IAdminRepository iar, EcommercedemoContext context )
        {
            _context = context;
            _iar = iar;
        }

        //Category
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<CategoryViewModel> CreateCategory([FromForm] IFormFile image,[FromForm] CategoryViewModel ACVM)
        
        {
            try
            {
                if(_context!=null)
                {
                    var CreatingCategory = await  _iar.CreateCategory(image, ACVM);
                    return CreatingCategory;
                }
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

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

        [HttpPost]
        [Route("GetDetailsAndImagesOfCategories")]
        public async Task<IActionResult> GetDetailsAndImagesOfCategories()
        {
            try
            {
                if (_context != null)
                {
                    var categorieddetails = await _iar.GetDetailsAndImagesOfCategories();
                    if(categorieddetails != null)
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
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<CategoryViewModel> UpdateCategory([FromForm] IFormFile image,[FromForm] CategoryViewModel UCVM)
        {
            try
            {
                if (UCVM != null)
                {
                    return await _iar.UpdateCategory(image,UCVM);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
        [HttpPost]
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand([FromForm] IFormFile Image,[FromForm] BrandViewModel bvm)
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
        [HttpDelete]
        [Route("Deletebrand")]
        public async Task<string> Deletebrand(int brand_id)
        {
            try
            {
                return await _iar.DeleteBrand(brand_id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut]
        [Route("UpdateBrand")]
        public async Task<BrandViewModel> UpdateBrand([FromForm] IFormFile image,[FromForm] BrandViewModel bvm)
        {
            try
            {
                if (bvm != null)
                {
                    return await _iar.UpdateBrand(image, bvm);
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
  
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
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ProductViewModel> CreateProduct([FromForm] IFormFile Image,[FromForm] ProductViewModel APVM)
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
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] IFormFile image,[FromForm] ProductViewModel UPVM)
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
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            try
            {
                return await _iar.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        [HttpGet]
        [Route("GetSubscriptionTypes")]
        public async Task<IActionResult> GetSubscriptiontypes()
        {
            try
            {
                var susbcriptionttypes= await _iar.GetSubscriptiontypes();
                return Ok(susbcriptionttypes);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

}


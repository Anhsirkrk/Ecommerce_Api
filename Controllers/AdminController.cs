using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _iar;

        public AdminController(IAdminRepository iar)
        {
            _iar = iar;
        }

    //brand
        [HttpPost]
        [Route("CreateBrand")]
        public async Task<Brand> CreateBrand(string brandName)
        {
            try
            {
                var brand = new Brand()
                {
                    BrandName =brandName,
                };
                return await _iar.CreateBrand(brand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<Brand> UpdateBrand(TotalViewModel TVM)
        {
            try
            {
                if (TVM != null)
                {
                    return await _iar.UpdateBrand(TVM);
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetAllBrands")]
        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                return await _iar.GetAllBrands();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetBrandById")]
        public async Task<Brand> GetBrandById(int brand_id)
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
        public async Task<ProductViewModel> CreateProduct([FromForm] ProductViewModel APVM)
        {
            try
            {
                if (APVM != null)
                {
                    return await _iar.CreateProduct(APVM);
                }
                return null;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UploadProductImage")]
        public async Task<Product> UploadProductImage( int product_id,  IFormFile imagefile)
        {
            try
            {
                return await _iar.UploadProductImage(product_id, imagefile);
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
        public async Task<Product> UpdateProduct([FromForm] ProductViewModel UPVM, [FromForm] IFormFile imageFile)
        {
            try
            {
                if (UPVM != null)
                {
                    return await _iar.UpdateProduct(UPVM,imageFile);
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
        public async Task<List<Product>> GetAllProducts()
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
        [HttpGet]
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

    }
}

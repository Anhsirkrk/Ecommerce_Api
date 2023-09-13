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
        public async Task<Brand> CreateBrand(TotalViewModel TVM)
        {
            try
            {
                var brand = new Brand()
                {
                    BrandName = TVM.BrandName,
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
        public async Task<Product> CreateProduct(TotalViewModel TVM)
        {
            try
            {
                if (TVM!=null)
                {
                    return await _iar.CreateProduct(TVM);
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
        public async Task<Product> UpdateProduct(TotalViewModel TVM)
        {
            try
            {
                if (TVM!= null)
                {
                    return await _iar.UpdateProduct(TVM);
                }
                return null;
            }   
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetAllBrands")]
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
        [Route("GetBrandById")]
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

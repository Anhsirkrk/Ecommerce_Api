using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Repository
{
    public interface IAdminRepository
    {

    //Brand
        Task<Brand> CreateBrand(Brand brand);
        Task<string> DeleteBrand(int brand_id);
        Task<Brand> UpdateBrand(TotalViewModel TVM);
        Task<List<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(int brand_id);

    //product
        Task<ProductViewModel> CreateProduct([FromForm] ProductViewModel APVM, [FromForm] IFormFile imageFile);
        Task<string> DeleteProduct(int product_id);
        Task<Product> UpdateProduct(ProductViewModel UPVM, IFormFile imageFile);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductById(List<int> product_id); 
    }
}

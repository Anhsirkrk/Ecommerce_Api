using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface IAdminInterface
    {

    //Brand
        Task<Brand> CreateBrand(Brand brand);
        Task<string> DeleteBrand(int brand_id);
        Task<Brand> UpdateBrand(TotalViewModel TVM);
        Task<List<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(int brand_id);

    //product
        Task<Product> CreateProduct(TotalViewModel TVM);
        Task<string> DeleteProduct(int product_id);
        Task<Product> UpdateProduct(TotalViewModel TVM);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductById(List<int> product_id); 
    }
}

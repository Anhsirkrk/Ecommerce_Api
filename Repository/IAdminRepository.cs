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
        Task<Brand> UpdateBrand(int brand_id, string brandName);
        Task<List<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(int brand_id);

    //Product
        Task<ProductViewModel> CreateProduct([FromForm] ProductViewModel APVM);
        Task<Product> UploadProductImage(int product_id, IFormFile imagefile);
        Task<string> DeleteProduct(int product_id);
        Task<Product> UpdateProduct(ProductViewModel UPVM);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductById(List<int> product_id); 


    //Category
        Task<CategoryViewModel> CreateCategory(CategoryViewModel ACVM);
        Task<CategoryViewModel> UpdateCategory(CategoryViewModel UCVM);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int category_id);
        Task<Category> UploadCategoryImage(int category_id, IFormFile imagefile);
        Task<string> DeleteCategory(int category_id);


    }
}

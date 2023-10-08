using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Repository
{
    public interface IAdminRepository
    {

        //subscriptions
        Task<List<SubscriptionType>> GetSubscriptiontypes();
        //Brand
        Task<BrandViewModel> CreateBrand([FromForm] IFormFile Image, [FromForm] BrandViewModel bvm);
        Task<List<BrandViewModel>> GetDetailsAndImagesOfBrands();
        Task<string> DeleteBrand(int brand_id);
        Task<BrandViewModel> UpdateBrand(IFormFile image, BrandViewModel BvM);
        Task<BrandViewModel> GetBrandById(int brand_id);

        //Product
        Task<ProductViewModel> CreateProduct([FromForm] IFormFile Image, [FromForm] ProductViewModel APVM);
       Task<string> DeleteProduct(int product_id);
        Task<ProductViewModel> UpdateProduct([FromForm] IFormFile image, [FromForm] ProductViewModel UPVM);
        Task<List<ProductViewModel>> GetAllProducts();
        Task<List<Product>> GetProductById(List<int> product_id); 


    //Category
        Task<CategoryViewModel> CreateCategory(IFormFile image,CategoryViewModel ACVM);
        Task<List<CategoryDetailsWithImage_>> GetDetailsAndImagesOfCategories();

        Task<string> GetCategoryImage(int category_id);
        Task<CategoryViewModel> UpdateCategory([FromForm] IFormFile image,[FromForm] CategoryViewModel UCVM);
        //Task<List<Category>> GetAllCategories();
        Task<CategoryDetailsWithImage_> GetCategoryById(int category_id);
        //Task<Category> UploadCategoryImage(int category_id, IFormFile imagefile);
        Task<string> DeleteCategory(int category_id);


    }
}

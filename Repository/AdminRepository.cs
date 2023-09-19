using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommerce_Api.Repository
{
    public class AdminRepository:IAdminRepository
    {
        private readonly EcommercedemoContext _context;
        public AdminRepository(EcommercedemoContext context)
        {
            _context = context;   
        }

        //brand
        public async Task<Brand> CreateBrand(Brand brand)
        {
            try
            {
                if (_context != null)
                {
                    await _context.Brands.AddAsync(brand);
                    await _context.SaveChangesAsync();
                       
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteBrand(int brand_id)
        {
            try
            {
                if (_context != null)
                {
                    var brand = await _context.Brands.FindAsync(brand_id);
                    if (brand != null)
                    {
                        _context.Brands.Remove(brand);
                        _context.SaveChanges();
                        return "Brand deleted success";
                    }
                    return "Brand not found";
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Brand> UpdateBrand(int brand_id, string brandName)
        {
            try
            {
                if(_context != null)
                {
                    var item = _context.Brands.FirstOrDefault(x => x.BrandId == brand_id);

                    if (item != null)
                    {
                        item.BrandName= brandName;

                        _context.Brands.Update(item);
                        await _context.SaveChangesAsync();
                        return item;
                    }
                        
                    
                    return null;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                if( _context != null)
                {
                    List<Brand> brands=new List<Brand>();

                    brands = _context.Brands.ToList();
                    return brands;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Brand> GetBrandById(int brand_id)
        {
            try
            {
                if(_context != null)
                {
                    var brand = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == brand_id);
                    return brand;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        //Product
        public async Task<ProductViewModel> CreateProduct([FromForm] ProductViewModel APVM)
        {
            try
            {
                if (_context != null)
                {
                    //string imageUrl = await SaveImageAsync(imageFile);
                    var product = new Product()
                    {
                        CategoryId = APVM.CategoryId,
                        BrandId = APVM.BrandId,
                        ProductName = APVM.ProductName,
                        StockQuantity = APVM.StockQuantity,
                        Price = APVM.Price,
                        Weight = APVM.Weight,
                        Unit = APVM.Unit,
                        //ImageUrl = imageUrl,
                        IsAvailable = APVM.IsAvailable,
                        ExpiryDate = APVM.ExpiryDate,
                        ManufactureDate = APVM.ManufactureDate,
                        DiscountId = APVM.DiscountId,
                        Description = APVM.Description,
                    };
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    return APVM;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> UploadProductImage(int product_id, IFormFile imagefile)
        {
            try
            {
                if (_context != null)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == product_id);
                    product.ImageUrl = await SaveProductImageAsync(imagefile);
                    _context.SaveChanges();
                    return product;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //image upload static code
        private async Task<string> SaveProductImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null; // No image provided
            }

            try
            {
                // Specify the directory where you want to save the image
                string ProductUploadDirectory = @"E:\Visual Studio\Ecommerce_Api\Assests\Images\Product_Images"; // Change this to your desired path


                // Ensure the directory exists, or create it if it doesn't
                if (!Directory.Exists(ProductUploadDirectory))
                {
                    Directory.CreateDirectory(ProductUploadDirectory);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(ProductUploadDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return "E:/Visual Studio/Ecommerce_Api/Assests/Images/Product_Images/" + fileName; // Store the relative URL in the database
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //image upload static code
        private async Task<string> ConvertImageToString(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null; // No image provided
            }

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes); // Convert image to base64 string
            }
        }

        public async Task<string> DeleteProduct(int product_id)
        {
            try
            {
                if (_context != null)
                {
                    var product = await _context.Products.FindAsync(product_id);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        return "Product deleted successfully";
                    }
                    return "Product not found";
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> UpdateProduct(ProductViewModel UPVM)
        {
            try
            {
                if (_context != null)
                {
                    var item = _context.Products.FirstOrDefault(x => x.ProductId == UPVM.ProductId);
                    if(item!= null)
                    {
                        //string imageUrl = await SaveProductImageAsync(imageFile);

                        item.CategoryId = UPVM.CategoryId;
                        item.BrandId = UPVM.BrandId;
                        item.ProductName = UPVM.ProductName;
                        item.StockQuantity = UPVM.StockQuantity;
                        item.Price = UPVM.Price;
                        item.Weight = UPVM.Weight;
                        item.Unit = UPVM.Unit;
                        //item.ImageUrl = imageUrl;
                        item.IsAvailable = UPVM.IsAvailable;
                        item.ExpiryDate = UPVM.ExpiryDate;
                        item.ManufactureDate = UPVM.ManufactureDate;
                        item.DiscountId = UPVM.DiscountId;
                        item.Description = UPVM.Description;

                        _context.Products.Update(item);
                       await _context.SaveChangesAsync();
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                if (_context != null)
                {
                    List<Product> products = new List<Product>();

                    products =await _context.Products.ToListAsync();
                    return products;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Product>> GetProductById(List<int> product_id)
        {
            try
            {
                if (_context != null && product_id!=null && product_id.Any())
                {
                    var product = await _context.Products.Where(x => product_id.Contains(x.ProductId)).ToListAsync();
                    return product;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Category
        public async Task<CategoryViewModel> CreateCategory(CategoryViewModel ACVM)
        {
            try
            {
                if(_context != null)
                {
                    var category = new Category()
                    {
                        CategoryName = ACVM.CategoryName,
                        Description = ACVM.Description,
                    };
                     _context.Categories.Add(category);
                     _context.SaveChanges();
                    return ACVM;

                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                if(_context != null)
                {
                    return await _context.Categories.ToListAsync();
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryViewModel> UpdateCategory(CategoryViewModel UCVM)
        {
            try
            {
                if (_context != null)
                {
                    var item = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId ==UCVM.CategoryId);
                    item.CategoryName = UCVM.CategoryName;
                    item.Description = UCVM.Description;

                    _context.Categories.Update(item);
                    _context.SaveChanges();

                    return UCVM;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> GetCategoryById(int category_id)
        {
            try
            {
                if (category_id != null)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == category_id);
                    return category;
                }
                return null; 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteCategory(int category_id)
        {
            try
            {
                if (_context != null)
                {
                    var category = await _context.Categories.FindAsync(category_id);
                    if (category != null)
                    {
                        _context.Categories.Remove(category);
                        _context.SaveChanges();

                        return "Product deleted successfully";
                    }
                    return "Product not found";
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> UploadCategoryImage(int category_id, IFormFile imagefile)
        {
            try
            {
                var item = _context.Categories.FirstOrDefault(x=>x.CategoryId == category_id);
                item.ImageUrl=await SaveCategoryuImageAsync(imagefile);
                _context.Categories.Update(item);
                _context.SaveChanges();

                return item;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //image upload static code
        private async Task<string> SaveCategoryuImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null; // No image provided
            }

            try
            {
                // Specify the directory where you want to save the image
                string CategoryuploadDirectory = @"E:\Visual Studio\Ecommerce_Api\Assests\Images\Category_images"; // Change this to your desired path


                // Ensure the directory exists, or create it if it doesn't
                if (!Directory.Exists(CategoryuploadDirectory))
                {
                    Directory.CreateDirectory(CategoryuploadDirectory);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(CategoryuploadDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return "E:/Visual Studio/Ecommerce_Api/Assests/Images/Category_images/" + fileName; // Store the relative URL in the database
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}

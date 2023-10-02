using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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


        //Product
        public async Task<ProductViewModel> CreateProduct([FromForm] IFormFile Image,[FromForm] ProductViewModel APVM)
        {
            try
            {
                if (_context != null && APVM!=null)
                {
                    APVM.IsProductAdded = false;
                    var product = new Product()
                    {
                        CategoryId = APVM.CategoryId,
                        BrandId = APVM.BrandId,
                        ProductName = APVM.ProductName,
                        ImageUrl = "EMPTY",
                    };
                    var createproduct = await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    var PID = product.ProductId;
                    string SAVEDimageUrl = await SaveProductImageAsync(Image,product.ProductId);
                    product.ImageUrl = SAVEDimageUrl;
                    await _context.SaveChangesAsync();
                    for (int i = 0, j = 0, k = 0,l = 0,m=0,n=0,o=0,p=0,q=0,r=0; i < APVM.SizeOfEachUnits.Count && j < APVM.WeightOfEachUnits.Count 
                        && k <APVM.StockOfEachUnits.Count && l<APVM.PriceOfEachUnits.Count && m < APVM.IsAvailable_OfEachUnit.Count
                        && n<APVM.MFG_OfEachUnits.Count && o < APVM.EXP_OfEachUnits.Count && p<APVM.DiscountId_OfEachUnit.Count 
                        && q<APVM.Avaialble_Quantity_ofEachUnit.Count && r<APVM.Description_OfEachUnits.Count; 
                        i++, j++, k++,l++,m++,n++,o++,p++,q++,r++)
                    {
                        var ItemdetailsOfeachproduct = new ProductItemDetail
                        {
                            ProductId = product.ProductId,
                            Unit = APVM.Unit,
                            SizeOfEachUnit = APVM.SizeOfEachUnits[i],
                            WeightOfEachUnit = APVM.WeightOfEachUnits[j],
                            StockOfEachUnit = APVM.StockOfEachUnits[k],
                            Price= APVM.PriceOfEachUnits[l],
                            IsAvailable = APVM.IsAvailable_OfEachUnit[m],
                            ManufactureDate= APVM.MFG_OfEachUnits[n],
                            ExpiryDate  = APVM.EXP_OfEachUnits[o],
                            DiscountId  = APVM.DiscountId_OfEachUnit[p],
                            AvailableQuantity = APVM.Avaialble_Quantity_ofEachUnit[q],
                            Description = APVM.Description_OfEachUnits[r],     
                        };
                        var addproductitemdetails = await _context.ProductItemDetails.AddAsync(ItemdetailsOfeachproduct);
                        await _context.SaveChangesAsync();
                    }
                    APVM.ResultMessage = "Product added Successfully";
                    APVM.IsProductAdded = true;
                    return APVM;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //image upload static code
        private async Task<string> SaveProductImageAsync(IFormFile image, int Product_Id)
        {
            try
            {
                var productitem = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == Product_Id);
                if (productitem != null)
                {
                    if (image == null || image.Length == 0)
                    {
                        return null; // No image provided
                    }


                    // Specify the directory where you want to save the image
                    string ProductuploadDirectory = @"C:\Users\HP\Source\Repos\Ecommerce_Api\Assests\Images\Product_images"; // Change this to your desired path

                    // Ensure the directory exists, or create it if it doesn't
                    if (!Directory.Exists(ProductuploadDirectory))
                    {
                        Directory.CreateDirectory(ProductuploadDirectory);
                    }

                    string fileName = image.FileName;
                    string filePath = Path.Combine(ProductuploadDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    return "C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Product_images/" + fileName; // Store the relative URL in the database
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
       

        public async Task<string> DeleteProduct(int product_id)
        {
            try
            {
                if (_context != null)
                {
                    var productitemdetails = await _context.ProductItemDetails.FirstOrDefaultAsync(X=>X.ProductId == product_id);
                    if (productitemdetails != null)
                    {
                            _context.ProductItemDetails.Remove(productitemdetails);
                            _context.SaveChanges();
                    }
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

        public async Task<ProductViewModel> UpdateProduct([FromForm] IFormFile image,[FromForm] ProductViewModel UPVM)
        {
            try
            {
                if (_context != null)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == UPVM.ProductId);
                    var ImageUrl = await SaveProductImageAsync(image, product.ProductId);
                    if (product != null)
                    {
                        product.ProductName = UPVM.ProductName;
                        product.CategoryId = UPVM.CategoryId;
                        product.BrandId = UPVM.BrandId;
                        product.ImageUrl = ImageUrl;
                        await _context.SaveChangesAsync();
                    }
                    var productitemdetail = await _context.ProductItemDetails.FirstOrDefaultAsync(x => x.ProductId == UPVM.ProductId);
                    if (productitemdetail != null)
                    {
                        for (int i = 0, j = 0, k = 0, l = 0, m = 0, n = 0, o = 0, p = 0,q=0,r=0; i < UPVM.SizeOfEachUnits.Count && j < UPVM.WeightOfEachUnits.Count
                            && k < UPVM.StockOfEachUnits.Count && l < UPVM.PriceOfEachUnits.Count &&
                            m < UPVM.IsAvailable_OfEachUnit.Count && n < UPVM.MFG_OfEachUnits.Count && o < UPVM.EXP_OfEachUnits.Count &&
                            p < UPVM.DiscountId_OfEachUnit.Count && q < UPVM.Avaialble_Quantity_ofEachUnit.Count && r < UPVM.Description_OfEachUnits.Count;
                        i++, j++, k++, l++, m++, n++, o++, p++, q++, r++)
                        {
                            productitemdetail.ProductId = product.ProductId;
                            productitemdetail.Unit = UPVM.Unit;
                            productitemdetail.SizeOfEachUnit = UPVM.SizeOfEachUnits[i];
                            productitemdetail.WeightOfEachUnit = UPVM.WeightOfEachUnits[j];
                            productitemdetail.StockOfEachUnit = UPVM.StockOfEachUnits[k];
                            productitemdetail.Price = UPVM.PriceOfEachUnits[l];
                            productitemdetail.IsAvailable = UPVM.IsAvailable_OfEachUnit[m];
                            productitemdetail.ManufactureDate = UPVM.MFG_OfEachUnits[n];
                            productitemdetail.ExpiryDate = UPVM.EXP_OfEachUnits[o];
                            productitemdetail.DiscountId = UPVM.DiscountId_OfEachUnit[p];
                            productitemdetail.AvailableQuantity = UPVM.Avaialble_Quantity_ofEachUnit[q];
                            productitemdetail.Description = UPVM.Description_OfEachUnits[r];
                            _context.SaveChangesAsync();
                        }
                        UPVM.ResultMessage = "product updated successfully";
                        UPVM.IsProductUpdated = true;
                        return UPVM;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<List<ProductViewModel>> GetAllProducts()
        
        
        {
            try
            {
                if (_context != null)
                {
                    List<ProductViewModel> productlist = new List<ProductViewModel>();

                    var productids = await _context.Products.Select(x => x.ProductId).ToListAsync();

                    foreach (var id in productids)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

                        var imageurl = product.ImageUrl;
                        using (var image = System.Drawing.Image.FromFile(imageurl))
                        {
                            ImageFormat format = image.RawFormat;
                            var memorystream = new MemoryStream();
                            image.Save(memorystream, format);

                            // Convert the image to base64 string
                            string base64Image = Convert.ToBase64String(memorystream.ToArray());

                            var productdata =await (from p in _context.Products
                                               join pi in _context.ProductItemDetails on p.ProductId equals pi.ProductId
                                               join pc in _context.Categories on p.CategoryId equals pc.CategoryId
                                               join pb in _context.Brands on p.BrandId equals pb.BrandId
                                               where p.ProductId == id
                                               select new ProductViewModel
                                               {
                                                   ProductId = p.ProductId,
                                                   ProductName = p.ProductName,
                                                   CategoryId = p.CategoryId,
                                                   CategoryName = pc.CategoryName,
                                                   BrandId = p.BrandId,
                                                   BrandName = pb.BrandName,
                                                   Base64Image = base64Image,
                                                   Unit = pi.Unit,
                                                   SizeOfUnit = (decimal)pi.SizeOfEachUnit,
                                                   WeightOfUnit = (decimal)pi.WeightOfEachUnit,
                                                   StockOfUnit = (decimal)pi.StockOfEachUnit,
                                                   Price = (decimal)pi.Price,
                                                   IsAvailable = (bool)pi.IsAvailable,
                                                   MFG = (DateTime)pi.ManufactureDate,
                                                   EXP = (DateTime)pi.ExpiryDate,
                                                   DiscountId = (int)pi.DiscountId,
                                                   Avaialble_Quantity = (decimal)pi.AvailableQuantity,
                                                  Description= pi.Description
                                               }).ToListAsync();

                            var groupedData = productdata.GroupBy(item => item.ProductId);

                            foreach (var group in groupedData)
                            {
                                ProductViewModel productViewModel = new ProductViewModel
                                {
                                    ProductId = group.Key,
                                    ProductName = group.First().ProductName,
                                    CategoryId = group.First().CategoryId,
                                    CategoryName = group.First().CategoryName,
                                    BrandId = group.First().BrandId,
                                    BrandName = group.First().BrandName,
                                    Base64Image = group.First().Base64Image,
                                    Unit = group.First().Unit,
                                    SizeOfEachUnits = group.Select(item => item.SizeOfUnit).ToList(),
                                    WeightOfEachUnits = group.Select(item => item.WeightOfUnit).ToList(),
                                    StockOfEachUnits = group.Select(item => item.StockOfUnit).ToList(),
                                    PriceOfEachUnits = group.Select(item => item.Price).ToList(),
                                    MFG_OfEachUnits = group.Select(item => item.MFG).ToList(),
                                    EXP_OfEachUnits = group.Select(item => item.EXP).ToList(),
                                    IsAvailable_OfEachUnit = group.Select(item => item.IsAvailable).ToList(),
                                    Avaialble_Quantity_ofEachUnit = group.Select(item => item.Avaialble_Quantity).ToList(),
                                    Description_OfEachUnits = group.Select(item => item.Description).ToList(),
                                    DiscountId_OfEachUnit = group.Select(item => item.DiscountId).ToList()
                                };

                                productlist.Add(productViewModel);
                            }
                        }
                    }
                    return productlist;
               
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<List<Product>> GetProductById(List<int> product_id)
        {
            try
            {
                if (_context != null && product_id != null && product_id.Any())
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


    

        //brand
        public async Task<BrandViewModel> CreateBrand([FromForm] IFormFile Image, [FromForm] BrandViewModel bvm)
        {

            try
            {
                if (_context != null)
                {

                    var brand = new Brand()
                    {
                        BrandName = bvm.BrandName,
                        BrandDescription = bvm.BrandDescription,
                        CategoryId=bvm.Category_id,
                    };
                    await _context.Brands.AddAsync(brand);
                    await _context.SaveChangesAsync();

                    bvm.BrandId = brand.BrandId;
                    var savedimageurl = await SaveBrandImageAsync(Image, bvm.BrandId);
                    brand.Imageurl = savedimageurl;
                    await _context.SaveChangesAsync();

                    bvm.resultMessage = "Brand created successfully";
                    bvm.IsBrandCreated = true;
                    return bvm;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<BrandViewModel>> GetDetailsAndImagesOfBrands()
        {
            try
            {
                if (_context != null)
                {
                    List<BrandViewModel> BrandDetailsWithImage_s = new List<BrandViewModel>();
                    List<int> Brands = _context.Brands.Select(x => x.BrandId).ToList();

                    foreach (int id in Brands)
                    {
                        var brand = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == id);
                        if (brand != null)
                        {
                            var imageurl = brand.Imageurl;
                            using (var image = System.Drawing.Image.FromFile(imageurl))
                            {
                                ImageFormat format = image.RawFormat;
                                var memorystream = new MemoryStream();
                                image.Save(memorystream, format);

                                // Convert the image to base64 string
                                string base64Image = Convert.ToBase64String(memorystream.ToArray());

                                // Create a CategoryDetailsWithImage_ object
                                BrandViewModel branddetails = new BrandViewModel
                                {
                                    BrandId = brand.BrandId,
                                    BrandName = brand.BrandName,
                                    Base64Image = base64Image
                                };

                                BrandDetailsWithImage_s.Add(branddetails);
                            }
                        }
                    }
                    return BrandDetailsWithImage_s;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<BrandViewModel> GetBrandById(int brand_id)
        {
            try
            {
                if (brand_id != null)
                {
                    if (_context != null)
                    {

                        var user = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == brand_id);
                        var Brandimage = await GetBrandImage(brand_id);
                        BrandViewModel BrandDetailsWithImage_ = new BrandViewModel
                        {
                            BrandId = user.BrandId,
                            BrandName = user.BrandName,
                            Base64Image = Brandimage
                        };

                        return BrandDetailsWithImage_;

                        //if (user != null)
                        //{
                        //    var imageurl = user.ImageUrl;
                        //    using (var image = System.Drawing.Image.FromFile(imageurl))
                        //    {
                        //        ImageFormat format = image.RawFormat;
                        //        var memorystream = new MemoryStream();
                        //        image.Save(memorystream, format);

                        //        // Convert the image to base64 string
                        //        string base64Image = Convert.ToBase64String(memorystream.ToArray());

                        //        // Create a CategoryDetailsWithImage_ object
                        //        CategoryDetailsWithImage_ CategoryDetailsWithImage_ = new CategoryDetailsWithImage_
                        //        {
                        //            UserId = user.CategoryId,
                        //            UserName = user.CategoryName,
                        //            Base64Image = base64Image
                        //        };
                        //        CategoryDetailsWithImage_s.Add(CategoryDetailsWithImage_);
                        //    }
                    }

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

        public async Task<BrandViewModel> UpdateBrand(IFormFile image, BrandViewModel BvM)
        {
            try
            {
                if (_context != null)
                {
                    var item = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == BvM.BrandId);
                    var savingbrandimage = await SaveBrandImageAsync(image, BvM.BrandId);
                    item.CategoryId = BvM.Category_id;
                    item.BrandName = BvM.BrandName;
                    item.BrandDescription = BvM.BrandDescription;
                    item.Imageurl = savingbrandimage;
                    _context.Brands.Update(item);
                    _context.SaveChanges();

                    BvM.resultMessage = "Brand Updated Successfully";
                    return BvM;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> SaveBrandImageAsync(IFormFile image, int brand_Id)
        {
            try
            {
                var categoryitem = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == brand_Id);
                if (categoryitem != null)
                {
                    if (image == null || image.Length == 0)
                    {
                        return null; // No image provided
                    }


                    // Specify the directory where you want to save the image
                    string CategoryuploadDirectory = @"C:\Users\HP\Source\Repos\Ecommerce_Api\Assests\Images\Brand_images"; // Change this to your desired path

                    // Ensure the directory exists, or create it if it doesn't
                    if (!Directory.Exists(CategoryuploadDirectory))
                    {
                        Directory.CreateDirectory(CategoryuploadDirectory);
                    }

                    string fileName = image.FileName;
                    string filePath = Path.Combine(CategoryuploadDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    return "C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/" + fileName; // Store the relative URL in the database
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<string> GetBrandImage(int brand_id)
        {
            try
            {
                if (brand_id != null)
                {
                    var brand = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == brand_id);
                    if (brand != null)
                    {
                        var imageurl = brand.Imageurl;
                        using (var image = System.Drawing.Image.FromFile(imageurl))
                        {
                            ImageFormat format = image.RawFormat;
                            var memorystream = new MemoryStream();
                            image.Save(memorystream, format);

                            // Convert the image to base64 string
                            string base64Image = Convert.ToBase64String(memorystream.ToArray());

                            // Create a CategoryDetailsWithImage_ object
                            BrandViewModel BrandDetailsWithImage = new BrandViewModel
                            {
                                BrandId = brand.BrandId,
                                BrandName = brand.BrandName,
                                Base64Image = base64Image
                            };
                            return base64Image;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }



        //Category
        public async Task<CategoryViewModel> CreateCategory(IFormFile image, CategoryViewModel ACVM)
        {
            try
            {
                if (_context != null)
                {
                    var category = new Category()
                    {
                        CategoryName = ACVM.CategoryName,
                        Description = ACVM.Description,
                    };
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                    ACVM.CategoryId = category.CategoryId;
                    var savedimageurl = await SaveCategoryuImageAsync(image, ACVM.CategoryId);
                    category.ImageUrl = savedimageurl;
                    await _context.SaveChangesAsync();
                    return ACVM;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<Category> UploadCategoryImage(CategoryViewModel CAVM)
        //{
        //    try
        //    {
        //        var item = _context.Categories.FirstOrDefault(x => x.CategoryId == category_id);
        //        item.ImageUrl = await SaveCategoryuImageAsync(imagefile);
        //        _context.Categories.Update(item);
        //        _context.SaveChanges();

        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private async Task<string> SaveCategoryuImageAsync(IFormFile image, int CategoryId)
        {
            try
            {
                var categoryitem = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == CategoryId);
                if (categoryitem != null)
                {
                    if (image == null || image.Length == 0)
                    {
                        return null; // No image provided
                    }


                    // Specify the directory where you want to save the image
                    string CategoryuploadDirectory = @"C:\Users\HP\Source\Repos\Ecommerce_Api\Assests\Images\Category_images"; // Change this to your desired path

                    // Ensure the directory exists, or create it if it doesn't
                    if (!Directory.Exists(CategoryuploadDirectory))
                    {
                        Directory.CreateDirectory(CategoryuploadDirectory);
                    }

                    string fileName = image.FileName;
                    string filePath = Path.Combine(CategoryuploadDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    return "C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Category_images/" + fileName; // Store the relative URL in the database
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<List<CategoryDetailsWithImage_>> GetDetailsAndImagesOfCategories()
        {
            try
            {
                if (_context != null)
                {

                    List<CategoryDetailsWithImage_> CategoryDetailsWithImage_s = new List<CategoryDetailsWithImage_>();
                    List<int> Categories = _context.Categories.Select(x => x.CategoryId).ToList();

                    foreach (int id in Categories)
                    {
                        var user = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
                        if (user != null)
                        {
                            var imageurl = user.ImageUrl;
                            using (var image = System.Drawing.Image.FromFile(imageurl))
                            {
                                ImageFormat format = image.RawFormat;
                                var memorystream = new MemoryStream();
                                image.Save(memorystream, format);

                                // Convert the image to base64 string
                                string base64Image = Convert.ToBase64String(memorystream.ToArray());

                                // Create a CategoryDetailsWithImage_ object
                                CategoryDetailsWithImage_ CategoryDetailsWithImage_ = new CategoryDetailsWithImage_
                                {
                                    CategoryId = user.CategoryId,
                                    CategoryName = user.CategoryName,
                                    Base64Image = base64Image
                                };

                                CategoryDetailsWithImage_s.Add(CategoryDetailsWithImage_);
                            }
                        }

                    }
                    return CategoryDetailsWithImage_s;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<CategoryDetailsWithImage_> GetCategoryById(int category_id)
        {
            try
            {
                if (category_id != null)
                {
                    if (_context != null)
                    {

                        var user = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == category_id);
                        var categoryimage = await GetCategoryImage(category_id);
                        CategoryDetailsWithImage_ CategoryDetailsWithImage_ = new CategoryDetailsWithImage_
                        {
                            CategoryId = user.CategoryId,
                            CategoryName = user.CategoryName,
                            Base64Image = categoryimage
                        };

                        return CategoryDetailsWithImage_;

                        //if (user != null)
                        //{
                        //    var imageurl = user.ImageUrl;
                        //    using (var image = System.Drawing.Image.FromFile(imageurl))
                        //    {
                        //        ImageFormat format = image.RawFormat;
                        //        var memorystream = new MemoryStream();
                        //        image.Save(memorystream, format);

                        //        // Convert the image to base64 string
                        //        string base64Image = Convert.ToBase64String(memorystream.ToArray());

                        //        // Create a CategoryDetailsWithImage_ object
                        //        CategoryDetailsWithImage_ CategoryDetailsWithImage_ = new CategoryDetailsWithImage_
                        //        {
                        //            UserId = user.CategoryId,
                        //            UserName = user.CategoryName,
                        //            Base64Image = base64Image
                        //        };
                        //        CategoryDetailsWithImage_s.Add(CategoryDetailsWithImage_);
                        //    }
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetCategoryImage(int category_id)
        {
            try
            {
                if (category_id != null)
                {
                    var user = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == category_id);
                    if (user != null)
                    {
                        var imageurl = user.ImageUrl;
                        using (var image = System.Drawing.Image.FromFile(imageurl))
                        {
                            ImageFormat format = image.RawFormat;
                            var memorystream = new MemoryStream();
                            image.Save(memorystream, format);

                            // Convert the image to base64 string
                            string base64Image = Convert.ToBase64String(memorystream.ToArray());

                            // Create a CategoryDetailsWithImage_ object
                            CategoryDetailsWithImage_ CategoryDetailsWithImage_ = new CategoryDetailsWithImage_
                            {
                                CategoryId = user.CategoryId,
                                CategoryName = user.CategoryName,
                                Base64Image = base64Image
                            };
                            return base64Image;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        public async Task<CategoryViewModel> UpdateCategory(IFormFile image, CategoryViewModel UCVM)
        {
            try
            {
                if (_context != null)
                {
                    var item = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == UCVM.CategoryId);
                    var savingcategoryimage = await SaveCategoryuImageAsync(image, UCVM.CategoryId);
                    item.CategoryName = UCVM.CategoryName;
                    item.Description = UCVM.Description;
                    item.ImageUrl = savingcategoryimage;
                    _context.Categories.Update(item);
                    _context.SaveChanges();

                    return UCVM;
                }
                return null;
            }
            catch (Exception ex)
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









        //hidden

        //image upload static code
        //private async Task<string> ConvertImageToString(IFormFile imageFile)
        //{
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        return null; // No image provided
        //    }

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        await imageFile.CopyToAsync(memoryStream);
        //        byte[] bytes = memoryStream.ToArray();
        //        return Convert.ToBase64String(bytes); // Convert image to base64 string
        //    }
        //}

        //public async Task<Product> UploadProductImage(int product_id, IFormFile imagefile)
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == product_id);
        //            product.ImageUrl = await SaveProductImageAsync(imagefile);
        //            _context.SaveChanges();
        //            return product;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




    }
}

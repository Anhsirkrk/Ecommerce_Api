
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;

namespace Ecommerce_Api.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly EcommerceDailyPickContext context;

        public WishlistRepository(EcommerceDailyPickContext _context)
        {
            context = _context;
        }
        public async Task<WishlistViewModel> AddWishlistOfProductbyUserID(WishlistViewModel wishlistViewModel)
        {
            try
            {
                if (context != null && wishlistViewModel != null)
                {
                    // Check if a wishlist item with the given UserId and ProductId already exists
                    var existingWishlist = await context.Wishlists
                        .FirstOrDefaultAsync(w => w.UserId == wishlistViewModel.UserId && w.ProductId == wishlistViewModel.ProductId);

                    if (existingWishlist != null)
                    {
                        // Wishlist item exists; update it
                        existingWishlist.IsInWishlist = wishlistViewModel.IsInWishlist;
                    }
                    else
                    {
                        // Wishlist item doesn't exist; insert a new one
                        var newWishlist = new Wishlist
                        {
                            UserId = wishlistViewModel.UserId,
                            ProductId = wishlistViewModel.ProductId,
                            IsInWishlist = wishlistViewModel.IsInWishlist,
                        };

                        await context.Wishlists.AddAsync(newWishlist);
                    }

                    await context.SaveChangesAsync();

                    // Return the updated or newly inserted item
                    var updatedWishlist = await context.Wishlists
                        .FirstOrDefaultAsync(w => w.UserId == wishlistViewModel.UserId && w.ProductId == wishlistViewModel.ProductId);

                    if (updatedWishlist != null)
                    {
                        var wishlistview = new WishlistViewModel
                        {
                            WishlistId = updatedWishlist.WishlistId,
                            UserId = updatedWishlist.UserId,
                            ProductId = updatedWishlist.ProductId,
                            IsInWishlist = updatedWishlist.IsInWishlist ,
                        };

                        return wishlistview;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<UserWishlistViewModel>> GetUserWishlistProducts(int userid)
        
        {
            try
            {
                if (context != null)
                {
                    //List<UserWishlistViewModel> userWishlistViewModels = new List<UserWishlistViewModel>();
                    // Get a list of Product IDs in the user's wishlist
                    var productIdsInWishlist = await context.Wishlists
                        .Where(w => w.UserId == userid && w.IsInWishlist == true)
                        .Select(w => w.ProductId)
                        .ToListAsync();

                    List<UserWishlistViewModel> wishlistProducts = new List<UserWishlistViewModel>();

                    foreach (var productId in productIdsInWishlist)
                    {
                        var wishlistproductitem = await context.Wishlists.FirstOrDefaultAsync(x => x.ProductId == productId);
                        var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

                        var imageurl = product.ImageUrl;
                        using (var image = System.Drawing.Image.FromFile(imageurl))
                        {
                            ImageFormat format = image.RawFormat;
                            var memorystream = new MemoryStream();
                            image.Save(memorystream, format);

                            // Convert the image to base64 string
                            string base64Image = Convert.ToBase64String(memorystream.ToArray());

                            var productDatainwishlist = await (from p in context.Products
                                                               join pi in context.ProductItemDetails on p.ProductId equals pi.ProductId
                                                               join pc in context.Categories on p.CategoryId equals pc.CategoryId
                                                               join pb in context.Brands on p.BrandId equals pb.BrandId
                                                               join W in context.Wishlists on p.ProductId equals W.ProductId
                                                               where p.ProductId == productId
                                                               select new UserWishlistViewModel
                                                               {
                                                                   WishlistId = W.WishlistId,
                                                                   UserId = W.UserId,
                                                                   IsInWishlist = W.IsInWishlist,
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
                                                                   Description = pi.Description
                                                               })
                            .ToListAsync();

                            var groupedData = productDatainwishlist.GroupBy(item => item.ProductId);

                            foreach (var group in groupedData)
                            {
                                UserWishlistViewModel userwishlist = new UserWishlistViewModel
                                {

                                    ProductId = group.Key,
                                    WishlistId = group.First().WishlistId,
                                    UserId = group.First().UserId,
                                    IsInWishlist = group.First().IsInWishlist,
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

                                wishlistProducts.Add(userwishlist);
                            }
                        }
                    }

                    return wishlistProducts;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}





using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;

namespace Ecommerce_Api.Repository
{
    public class CartRepository:ICartRepository
    {
        private readonly EcommercedemoContext context;

            public CartRepository(EcommercedemoContext _context)
        {
            context = _context;
        }


            public async Task<CartViewModel> AddItemToCart(CartViewModel cvm)
            {
                try
                {
                    if (context != null && cvm != null)
                    {
                        var itemexist = await context.ShoppingCartItems.FirstOrDefaultAsync(x => x.CartId == cvm.CartId && x.ProductId == cvm.ProductId);
                        if (itemexist != null)
                        {
                            itemexist.Quantity = cvm.Quantity;
                            var updatingitem = context.ShoppingCartItems.Update(itemexist);
                            await context.SaveChangesAsync();
                            cvm.Resultmessage = "Item Quantity Updated";
                            cvm.IsItemAdded = true;
                            cvm.IsQuantityUpdated = true;
                            return cvm;
                        }
                        else
                        {
                            var cartitem = new ShoppingCartItem
                            {
                                CartId = cvm.CartId,
                                ProductId = cvm.ProductId,
                                Quantity = cvm.Quantity,
                            };
                            var insertingitem = await context.ShoppingCartItems.AddAsync(cartitem);
                            await context.SaveChangesAsync();
                            cvm.Resultmessage = "Item Added successFully";
                            cvm.IsItemAdded = true;
                            return cvm;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return null;
           
            }
        

           public async Task<CartViewModel> ChangingQuantityOfItem(CartViewModel cvm)
           {
                try
                {
                    if(context != null && cvm != null)
                    {
                        var item = await context.ShoppingCartItems.FirstOrDefaultAsync(x => x.CartId == cvm.CartId && x.ProductId == cvm.ProductId);
                        item.Quantity = cvm.Quantity;
                        var ChangingQuantity =  context.ShoppingCartItems.Update(item);
                        await context.SaveChangesAsync();
                        cvm.Resultmessage = "Quantity Updated";
                        cvm.IsQuantityUpdated = true;
                        return cvm;
                    }
                    else
                    {
                        cvm.Resultmessage = "Quantity Not Updated";
                        cvm.IsQuantityUpdated= false;
                        return cvm;
                    }
                }
                catch (Exception ex) 
                { 
                    throw ex;
                }

            return null;
           }
           public async Task<CartViewModel> DeletecartItem(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var item = new ShoppingCartItem
                    {
                        CartId = cvm.CartId,
                        ProductId = cvm.ProductId,
                        ItemId = cvm.ItemId,
                    };
                    var deleteitem = context.ShoppingCartItems.Remove(item);
                    await context.SaveChangesAsync();
                    cvm.Resultmessage = "Item Deleted Successfully";
                    cvm.IsItemDeleted = true;
                    return cvm;
                }
                else
                {
                    cvm.IsItemDeleted = false;
                    cvm.Resultmessage = "Item Not Deleted";
                    return cvm;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<CartViewModel> CreateCartForUserMultipleProductsAtOnce(CartViewModel cartviewmodel)
        {
            try
            {
                if (context != null)
                {
                    if(cartviewmodel != null)
                    {
                        var newCart = new ShoppingCart
                        {
                            UserId = cartviewmodel.UserId
                        };
                        var createcart = await context.ShoppingCarts.AddAsync(newCart);
                        await context.SaveChangesAsync();

                        for (int i = 0 ,  j = 0; i < cartviewmodel.ProductsList.Count && j< cartviewmodel.Quantitiesofeachproduct.Count; i++,j++)
                            {
                            var newacartitems = new ShoppingCartItem
                            {
                                CartId = newCart.CartId,
                                ProductId = cartviewmodel.ProductsList[i],
                                Quantity = cartviewmodel.Quantitiesofeachproduct[j]
                            };
                            var insertitems = await context.ShoppingCartItems.AddAsync(newacartitems);
                            await context.SaveChangesAsync();
                            cartviewmodel.Resultmessage = " Added to Cart";

                        }
                        //foreach (int productid in cartviewmodel.ProductsList)
                        //{
                        //    foreach(int quantityofProItem in cartviewmodel.Quantitiesofeachproduct)
                        //    {
                        //             var newacartitems = new ShoppingCartItem
                        //             {
                        //                 CartId = newCart.CartId,
                        //                 ProductId = productid,
                        //                 Quantity = quantityofProItem
                        //             };
                        //        var insertitems = await context.ShoppingCartItems.AddAsync(newacartitems);
                        //        await context.SaveChangesAsync();
                        //        cartviewmodel.Resultmessage = " Added to Cart";
                                
                        //    }
                        //}
                        return cartviewmodel;
                        // Assuming both lists have the same length
                        //for (int i = 0; i < cartviewmodel.ProductsList.Count; i++)
                        //{
                        //    int productid = cartviewmodel.ProductsList[i];
                        //    int quantityofProItem = cartviewmodel.Quantitiesofeachproduct[i];

                        //    var newacartitems = new ShoppingCartItem
                        //    {
                        //        CartId = newCart.CartId,
                        //        ProductId = productid,
                        //        Quantity = quantityofProItem
                        //    };

                        //    var insertitems = await context.ShoppingCartItems.AddAsync(newacartitems);
                        //    await context.SaveChangesAsync();
                        //}
                    }
                    
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        //get user cartitems based on userid
       public async Task<List<CartUserViewModel>> GetCartItemsBasedOnUserId(int userid)
        {
            var cartUserViewModels = (from sc in context.ShoppingCarts
                                      join sci in context.ShoppingCartItems on  sc.CartId equals sci.CartId
                                      join p in context.Products on sci.ProductId equals p.ProductId
                                      join pid in context.ProductItemDetails on p.ProductId equals pid.ProductId
                                      join d in context.Discounts on pid.DiscountId equals d.DiscountId
                                      join c in context.Categories on p.CategoryId equals c.CategoryId
                                      join b in context.Brands on p.BrandId equals b.BrandId
                                      where sc.UserId == userid
                                      group new { sc, sci, p, pid, d, c, b } by p.ProductId into g
                                      select new CartUserViewModel
                                      {
                                          // Map properties based on your requirements
                                          UserId = g.First().sc.UserId ?? 0,
                                          CartId = g.First().sc.CartId,
                                          ItemId = g.First().sci.CartId ?? 0,
                                          ProductId = g.First().sci.ProductId ?? 0,
                                          image = g.First().p.ImageUrl,
                                          CategoryId = g.First().c.CategoryId,
                                          CategoryName = g.First().c.CategoryName,
                                          BrandId = g.First().b.BrandId,
                                          BrandName = g.First().b.BrandName,
                                          ProductName = g.First().p.ProductName,
                                          Unit = g.First().pid.Unit,
                                          SizeOfEachUnits = g.Select(item => item.pid.SizeOfEachUnit ?? 0m).ToList(),
                                          WeightOfEachUnits = g.Select(item => item.pid.WeightOfEachUnit ?? 0m).ToList(),
                                          StockOfEachUnits = g.Select(item => item.pid.StockOfEachUnit ?? 0m).ToList(),
                                          PriceOfEachUnits = g.Select(item => item.pid.Price ?? 0m).ToList(),
                                          MFG_OfEachUnits = g.Select(item => item.pid.ManufactureDate ?? DateTime.MinValue).ToList(),
                                          EXP_OfEachUnits = g.Select(item => item.pid.ExpiryDate ?? DateTime.MinValue).ToList(),
                                          IsAvailable_OfEachUnit = g.Select(item => item.pid.IsAvailable ?? false).ToList(),
                                          Avaialble_Quantity_ofEachUnit = g.Select(item => item.pid.AvailableQuantity ?? 0m).ToList(),
                                          Description_OfEachUnits = g.Select(item => item.pid.Description).ToList(),
                                          DiscountId_OfEachUnit = g.Select(item => item.pid.DiscountId ?? 0).ToList(),
                                          //SizeOfEachUnits = new List<decimal> { g.First().pid.SizeOfEachUnit ?? 0m },
                                          //WeightOfEachUnits = new List<decimal> { g.First().pid.WeightOfEachUnit ?? 0m },
                                          //StockOfEachUnits = new List<decimal> { g.First().pid.StockOfEachUnit ?? 0m },
                                          //PriceOfEachUnits = new List<decimal> { g.First().pid.Price ?? 0m },
                                          //MFG_OfEachUnits = new List<DateTime> { g.First().pid.ManufactureDate ?? DateTime.MinValue },
                                          //EXP_OfEachUnits = new List<DateTime> { g.First().pid.ExpiryDate ?? DateTime.MinValue },
                                          //IsAvailable_OfEachUnit = new List<bool> { g.First().pid.IsAvailable ?? false },
                                          //Avaialble_Quantity_ofEachUnit = new List<decimal> { g.First().pid.AvailableQuantity ?? 0m },
                                          //Description_OfEachUnits = new List<string> { g.First().pid.Description },
                                          //DiscountId_OfEachUnit = new List<int> { g.First().pid.DiscountId ?? 0 },
                                          //ResultMessage = g.First().sc.userid,
                                          IsAvailable = g.First().pid.IsAvailable ?? false,
                                          SizeOfUnit = g.First().pid.SizeOfEachUnit ?? 0m,
                                          WeightOfUnit = (decimal)(g.First().pid.WeightOfEachUnit ?? 0m),
                                          StockOfUnit = (decimal)(g.First().pid.StockOfEachUnit ?? 0m),
                                          MFG = g.First().pid.ManufactureDate ?? DateTime.MinValue,
                                          EXP = g.First().pid.ExpiryDate ?? DateTime.MinValue,
                                          Price = g.First().pid.Price ?? 0m,
                                          DiscountId = g.First().pid.DiscountId ?? 0,
                                          Avaialble_Quantity = g.First().pid.AvailableQuantity ?? 0m,
                                          Description = g.First().pid.Description,
                                          // Map other properties
                                      }).ToList();
            var cartUserViewModelslist = cartUserViewModels.ToList();

            // Convert and assign the image as base64
            foreach (var viewModel in cartUserViewModelslist)
            {
                string imageurl = viewModel.image; // Replace 'ImageUrl' with the actual property name

                using (var image = System.Drawing.Image.FromFile(imageurl))
                {
                    ImageFormat format = image.RawFormat;
                    var memorystream = new MemoryStream();
                    image.Save(memorystream, format);

                    // Convert the image to base64 string
                    string base64Image = Convert.ToBase64String(memorystream.ToArray());

                    viewModel.image = base64Image; // Replace 'ImageUrl' with the actual property name
                }
            }

            return cartUserViewModelslist;
            
        }
    }
}

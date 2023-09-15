using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class CartRepository:ICartInterface
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
                        foreach(int productid in cartviewmodel.ProductsList)
                        {
                            foreach(int quantityofProItem in cartviewmodel.Quantitiesofeachproduct)
                            {
                                     var newacartitems = new ShoppingCartItem
                                     {
                                         CartId = newCart.CartId,
                                         ProductId = productid,
                                         Quantity = quantityofProItem
                                     };
                                var insertitems = await context.ShoppingCartItems.AddAsync(newacartitems);
                                await context.SaveChangesAsync();
                                cartviewmodel.Resultmessage = " Added to Cart";
                                return cartviewmodel;
                            }
                        }
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
    }
}

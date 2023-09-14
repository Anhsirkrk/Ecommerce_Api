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

        public async Task<CartViewModel> CreateCartForUser(CartViewModel cartviewmodel)
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

using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommercedemoContext context;
        public UserRepository(EcommercedemoContext _context)
        {
            context = _context;
        }
        public async Task<UserViewModel> CreateUser(UserViewModel userviewmodel)
        {

            try
            {

                if (context != null)
                {
                    if (userviewmodel != null)
                    {
                        var user = new User
                        {
                            Username = userviewmodel.Email,
                            UserTypeId = userviewmodel.UserTypeId,
                            Password = userviewmodel.Password,
                            Firstname = userviewmodel.Firstname,
                            Lastname = userviewmodel.Lastname,
                            Mobile = userviewmodel.Mobile,
                            Email = userviewmodel.Email,
                            IsActive = userviewmodel.IsActive,
                        };
                        var res = await context.Users.AddAsync(user);
                        await context.SaveChangesAsync();
                        userviewmodel.isusercreated = true;
                        CartViewModel cvm = new CartViewModel();
                        cvm.UserId = user.UserId;
                        var creatinngcart = await CreateCart(cvm);
                        if (creatinngcart.IsCartCreated == true)
                        {
                            userviewmodel.ResultMessage = " Created Succesfully ";
                            return userviewmodel;
                        }

                    }
                }
                return null;
            }
            catch (Exception ex)
            { throw ex; }
        }
        public async Task<CartViewModel> CreateCart(CartViewModel cvm)
        {

            try
            {
                if (context != null && cvm.UserId != null)
                {

                    var newcart = new ShoppingCart
                    {
                        UserId = cvm.UserId,

                    };
                    var createcart = await context.ShoppingCarts.AddAsync(newcart);
                    await context.SaveChangesAsync();
                    cvm.CreatedCartID = newcart.CartId;
                    cvm.IsCartCreated = true;
                    cvm.Resultmessage = "Cart Created For user";
                    return cvm;
                }
                else
                {
                    cvm.Resultmessage = "Cart Not Created";
                    cvm.IsCartCreated = false;
                    return cvm;
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

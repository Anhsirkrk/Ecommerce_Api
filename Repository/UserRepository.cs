﻿using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

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

       //Get Products by brand
        public async Task<List<TotalViewModel>> GetProductsByBrand(int brand_Id)
        {
            try
            {
                var product = await (from b in context.Brands
                                     join p in context.Products on b.BrandId equals p.BrandId
                                     where p.BrandId == brand_Id select new TotalViewModel
                                     {
                                         ProductName = p.ProductName,
                                         ImageUrl=p.ImageUrl,
                                     }).ToListAsync();
                return product;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Get UserSubscibedProducts Besed on userid
       public async Task<List<UserSubscriptionProductsViewModel>> GetUserSubsriptionProductsBasedonUserId(int userId)
        {
           


            var userSubscriptionViewModels =
      from us in context.UserSubscriptions
       join o in context.Orders on us.OrderId equals o.OrderId
       join oi in context.OrderItems on o.OrderId equals oi.OrderId
       join p in context.Products on oi.ProductId equals p.ProductId
       join q in context.ProductItemDetails on p.ProductId equals q.ProductId
       join a in context.Addresses on o.UserId equals a.UserId
       where us.UserId == userId
       group new { us, o, oi, p, q, a } by p.ProductId into g
       select new UserSubscriptionProductsViewModel
       {
           ProductId = g.Key,
           ProductName = g.First().p.ProductName,
           image = g.First().p.ImageUrl,
           SizeOfEachUnit = g.First().q.SizeOfEachUnit ?? 0m,
           ProductPrice = g.First().oi.ProductPrice,
           Quantity = g.First().oi.Quantity ?? 0,
           OrderDate = g.First().o.OrderDate ?? DateTime.MinValue,
           StartDate = g.First().o.StartDate,
           EndDate = g.First().o.EndDate,
           TotalAmount = g.First().o.TotalAmount,
           AddressId = g.First().a.AddressId,
           Country = g.First().a.Country,
           State = g.First().a.State,
           City = g.First().a.City,
           Area = g.First().a.Area,
           Pincode = g.First().a.Pincode,
           HouseNo = g.First().a.HouseNo,
           Longitude = g.First().a.Longitude ?? 0.0m,
           Latitude = g.First().a.Latitude ?? 0.0m,
           // Map other properties here...
       };
            return userSubscriptionViewModels.ToList();

        }
    }
}

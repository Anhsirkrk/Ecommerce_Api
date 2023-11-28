using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;

namespace Ecommerce_Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceDailyPickContext context;
        public UserRepository(EcommerceDailyPickContext _context)
        {
            context = _context;
        }

        public UserRepository()
        {
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
                        userviewmodel.UserId = user.UserId;
                        var creatinngcart = await CreateCart(cvm);
                        if (creatinngcart.IsCartCreated == true)
                        {
                            userviewmodel.ResultMessage = " Created Succesfully ";
                            if (userviewmodel.Email!=null)
                            {
                                SendEmail(userviewmodel.Email);
                            }
                            return userviewmodel;
                        }

                    }
                }
                return null;
            }
            catch (DbUpdateException ex)
            {  // Log the details of the exception, including inner exceptions
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                    
                }
                throw ex;
            }
        }


        private void SendEmail(string email)
        {
            string senderEmail = "ramakrishna.hds.15@gmail.com";
            string senderPassword = "xvdf bpib kdpx fyru";


            string toEmail = email;
            string subject = "Welcome to Our DailyPick Website!";
            string body = "Thank you for registering on our website. We are excited to have you on board!";


            MailMessage mail = new MailMessage(senderEmail, toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            try
            {
                smtp.Send(mail);
            }
            catch (SmtpException ex)
            {
                // Handle SMTP exceptions (e.g., invalid recipient, server not reachable, authentication issues, etc.)
                // Log the error and handle it appropriately.
                Console.WriteLine("SMTP Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the error and handle it appropriately.
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                mail.Dispose();
                smtp.Dispose();
            }
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


        //Get user details by UserId 
        public async Task<User> GetUserDetailsByUserId(int userid)
        {
            try
            {
                var item = context.Users.Find(userid);

                return item;
            }
            catch
            {
                throw;
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
      join b in context.Brands on p.BrandId equals b.BrandId
      join q in context.ProductItemDetails on p.ProductId equals q.ProductId
      join a in context.Addresses on o.AddressId equals a.AddressId
      join pay in context.Payments on o.OrderId equals pay.OrderId
      join subs in context.SubscriptionTypes on o.SubscriptionTypeId equals subs.SubscriptionId
      where us.UserId == userId
      group new { us, o, oi, p, q, a ,b,subs, pay } by us.UserSubscriptionId into g
      orderby g.Max(x=>x.o.OrderId) descending
      select new UserSubscriptionProductsViewModel
      {
          ItemId = g.Key,   
          OrderId = g.First().o.OrderId,
          ProductId = g.First().p.ProductId,
          ProductName = g.First().p.ProductName,
          BrandName = g.First().b.BrandName,
          productindividualprice = g.First().oi.ProductPrice,
          SubscriptionType = g.First().subs.SubscriptionType1,
          image = g.First().p.ImageUrl,
          Quantity = g.First().oi.Quantity ?? 0,
          OrderDate = g.First().o.OrderDate ?? DateTime.MinValue,
          StartDate = (DateTime)g.First().o.StartDate,
          EndDate = (DateTime)g.First().o.EndDate,
          TotalAmount = g.First().o.TotalAmount,
          AddressId = g.First().a.AddressId,
          paymentrefno = g.First().pay.TransactionId,
          Country = g.First().a.Country,
          State = g.First().a.State,
          City = g.First().a.City,
          Area = g.First().a.Area,
          Pincode = g.First().a.Pincode,
          HouseNo = g.First().a.HouseNo,
          Longitude = g.First().a.Longitude ?? 0.0m,
          Latitude = g.First().a.Latitude ?? 0.0m,
          IsSubscriptionActive = (bool)g.First().us.IsActive
          // Map other properties here...
      };
            var userSubscriptionList = userSubscriptionViewModels.ToList();

            // Convert and assign the image as base64
            foreach (var viewModel in userSubscriptionList)
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

            return userSubscriptionList;

        }

        //update userprofile
        public async Task<UserViewModel> UpdateUserDetails(UserViewModel user)
        {
            var existingUser = context.Users.Find(user.UserId);

            if (existingUser == null)
            {
                return null;
            }


            //existingUser.UserTypeId = user.UserTypeId;
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Firstname = user.Firstname;
            existingUser.Lastname = user.Lastname;
            existingUser.Mobile = user.Mobile;
            existingUser.Email = user.Email;
            //existingUser.IsActive = user.IsActive;

            // Save changes to the repository
            context.Users.Update(existingUser);
            context.SaveChanges();
            var updatedUserViewModel = new UserViewModel
            {
                UserId = existingUser.UserId,
                Username = existingUser.Username,
                Password = existingUser.Password,
                Firstname = existingUser.Firstname,
                Lastname = existingUser.Lastname,
                Mobile = existingUser.Mobile,
                Email = existingUser.Email,

                // Map other properties as needed
            };
            return updatedUserViewModel; // Return the updated user
        }

        //adding adress details of user
        public async Task<UserAdressViewModel> AddingAdressDetails(UserAdressViewModel address)
        {
            try
            {
                if (address != null)
                {
                    var useraddress = new Address
                    {
                        UserId = address.UserId,
                        Country = address.Country,
                        State = address.State,
                        City = address.City,
                        Area = address.Area,
                        Pincode = address.Pincode,
                        HouseNo = address.HouseNo,
                        Longitude = address.Longitude,
                        Latitude = address.Latitude,
                        Username = address.Username,
                        MobileNumber = address.MobileNumber,
                    };
                    context.Addresses.Add(useraddress);
                    await context.SaveChangesAsync();
                    address.AddressId = useraddress.AddressId;
                    return address;
                }
                return address;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<UserAdressViewModel>> GetTheUserAdressDetails(int userid)
        {
            var addresses = await context.Addresses
            .Where(address => address.UserId == userid)
            .Select(address => new UserAdressViewModel
            {
                AddressId = address.AddressId,
                UserId = address.UserId ?? 0,
                Country = address.Country,
                State = address.State,
                City = address.City,
                Area = address.Area,
                Pincode = address.Pincode,
                HouseNo = address.HouseNo,
                Longitude = address.Longitude ?? 0,
                Latitude = address.Latitude ?? 0,
                Username = address.Username,
                MobileNumber = address.MobileNumber,
            })
            .ToListAsync();
            return addresses;
        }


        public async Task<UserAdressViewModel> UpdateAdressDetails(UserAdressViewModel userAdressViewModel)
        {
            var existingadress = context.Addresses.Find(userAdressViewModel.AddressId);

            if (existingadress == null)
            {
                return null;
            }


            //existingUser.UserTypeId = user.UserTypeId;
            existingadress.AddressId = userAdressViewModel.AddressId;
            existingadress.UserId = userAdressViewModel.UserId;
            existingadress.Country = userAdressViewModel.Country;
            existingadress.State = userAdressViewModel.State;
            existingadress.City = userAdressViewModel.City;
            existingadress.Area = userAdressViewModel.Area;
            existingadress.Pincode = userAdressViewModel.Pincode;
            existingadress.HouseNo = userAdressViewModel.HouseNo;
            existingadress.Longitude = userAdressViewModel.Longitude;
            existingadress.Latitude = userAdressViewModel.Latitude;
            existingadress.Username = userAdressViewModel.Username;
            existingadress.MobileNumber = userAdressViewModel.MobileNumber;
            //existingUser.IsActive = user.IsActive;

            // Save changes to the repository
            context.Addresses.Update(existingadress);
            context.SaveChanges();
            var updatedUserAdressViewModel = new UserAdressViewModel
            {
                AddressId = existingadress.AddressId,
                UserId = existingadress.UserId ?? 0,
                Country = existingadress.Country,
                State = existingadress.State,
                City = existingadress.City,
                Area = existingadress.Area,
                Pincode = existingadress.Pincode,
                HouseNo = existingadress.HouseNo,
                Longitude = existingadress.Longitude ?? 0,
                Latitude = existingadress.Latitude ?? 0,
                Username = existingadress.Username,
                MobileNumber = existingadress.MobileNumber,

                // Map other properties as needed
            };
            return updatedUserAdressViewModel; // Return the updated user
        }



        //hidden
        //Get Products by brand
        //public async Task<List<TotalViewModel>> GetProductsByBrand(int brand_Id)
        //{
        //    try
        //    {
        //        var product = await (from b in context.Brands
        //                             join p in context.Products on b.BrandId equals p.BrandId
        //                             where p.BrandId == brand_Id select new TotalViewModel
        //                             {
        //                                 ProductName = p.ProductName,
        //                                 ImageUrl=p.ImageUrl,
        //                             }).ToListAsync();
        //        return product;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
    }


}

using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Repository;


namespace Ecommerce_Api.Repository
{
    public interface IUserRepository
    {
        public Task<UserViewModel> CreateUser(UserViewModel userViewModel);


        // GET: get the details of user-subscribed products 
        Task<List<UserSubscriptionProductsViewModel>> GetUserSubsriptionProductsBasedonUserId(int userId);
        Task<UserViewModel> UpdateUserDetails(UserViewModel userViewModel);
        Task<User> GetUserDetailsByUserId(int userid);
        //adding adress details of user
        Task<UserAdressViewModel> AddingAdressDetails(UserAdressViewModel useraddress);

        //get  adress details of user
        Task<List<UserAdressViewModel>> GetTheUserAdressDetails(int userid);


        //hidden
        ////Get by Brand
        //Task<List<TotalViewModel>> GetProductsByBrand(int brand_Id);
    }

}

using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface IloginRepository
    {

        Task<LoginViewModel> GetUserByMobileNumber(LoginViewModel loginViewModel);
        //public Users GetUserByMobileNumber(decimal mobileNumber);
        public User CreateUser(User newuser);
    }
}

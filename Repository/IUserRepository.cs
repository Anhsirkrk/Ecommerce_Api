using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Repository;


namespace Ecommerce_Api.Repository
{
    public interface IUserRepository
    {
        public Task<LoginViewModel> CreateUser(LoginViewModel loginViewModel);
    }

}

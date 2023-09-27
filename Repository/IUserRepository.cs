using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Repository;


namespace Ecommerce_Api.Repository
{
    public interface IUserRepository
    {
        public Task<UserViewModel> CreateUser(UserViewModel userViewModel);

        //Get by Brand
        Task<List<TotalViewModel>> GetProductsByBrand(int brand_Id);
    }

}

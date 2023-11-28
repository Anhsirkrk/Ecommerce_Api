

using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.Identity.Client;

namespace Ecommerce_Api
{
    public class Test : IAdminRepository ,IUserRepository,ISupplierRepository
    {

        private readonly UserRepository _userRepository;
        private readonly AdminRepository _adminRepository;

        public Test (UserRepository userRepository, AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }


        public Task<CategoryDetailsWithImage_> getcatbyid(int id)
        {
          
            var res = _adminRepository.GetCategoryById(id);
            return res;
        }


        Task<UserAdressViewModel> IUserRepository.AddingAdressDetails(UserAdressViewModel useraddress)
        {
            int id = 1;
           var res =  _adminRepository.GetCategoryById(id);
            throw new NotImplementedException();
        }

        Task<SupplierViewModel> ISupplierRepository.AddSupplier(SupplierViewModel supplierViewModel)
        {
            throw new NotImplementedException();
        }

        Task<BrandViewModel> IAdminRepository.CreateBrand(IFormFile Image, BrandViewModel bvm)
        {
            throw new NotImplementedException();
        }

        Task<CategoryViewModel> IAdminRepository.CreateCategory(IFormFile image, CategoryViewModel ACVM)
        {
            throw new NotImplementedException();
        }

        Task<ProductViewModel> IAdminRepository.CreateProduct(IFormFile Image, ProductViewModel APVM)
        {
            throw new NotImplementedException();
        }

        Task<UserViewModel> IUserRepository.CreateUser(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }

        Task<string> IAdminRepository.DeleteBrand(int brand_id)
        {
            throw new NotImplementedException();
        }

        Task<string> IAdminRepository.DeleteCategory(int category_id)
        {
            throw new NotImplementedException();
        }

        Task<string> IAdminRepository.DeleteProduct(int product_id)
        {
            throw new NotImplementedException();
        }

        Task<List<ProductViewModel>> IAdminRepository.GetAllProductswithImage()
        {
            throw new NotImplementedException();
        }

        Task<BrandViewModel> IAdminRepository.GetBrandById(int brand_id)
        {
            throw new NotImplementedException();
        }

        Task<CategoryDetailsWithImage_> IAdminRepository.GetCategoryById(int category_id)
        {
            throw new NotImplementedException();
        }

        Task<string> IAdminRepository.GetCategoryImage(int category_id)
        {
            throw new NotImplementedException();
        }

        Task<List<BrandViewModel>> IAdminRepository.GetDetailsAndImagesOfBrands()
        {
            throw new NotImplementedException();
        }

        Task<List<CategoryDetailsWithImage_>> IAdminRepository.GetDetailsAndImagesOfCategories()
        {
            throw new NotImplementedException();
        }

        Task<List<Product>> IAdminRepository.GetProductById(List<int> product_id)
        {
            throw new NotImplementedException();
        }

        Task<List<SubscriptionType>> IAdminRepository.GetSubscriptiontypes()
        {
            throw new NotImplementedException();
        }

        Task<Supplier> ISupplierRepository.GetSupplierById(int SupplierId)
        {
            throw new NotImplementedException();
        }

        Task<List<SupplierOrderDetailsViewModel>> ISupplierRepository.GetSupplierOrderDetailsBySupplierId(int supplierId, string filterStatus1, string filterStatus2, string filterStatus3, string filterStatus4, string filterStatus5, string filterStatus6, string filterStatus7, string filterStatus8)
        {
            throw new NotImplementedException();
        }

        List<SupplierWithPinCodesViewModel> ISupplierRepository.GetSuppliersWithPinCodes()
        {
            throw new NotImplementedException();
        }

        Task<List<UserAdressViewModel>> IUserRepository.GetTheUserAdressDetails(int userid)
        {
            throw new NotImplementedException();
        }

        Task<List<SupplierOrderDetailsViewModel>> ISupplierRepository.GetTodaySupplierOrderDetailsBySupplierId(int supplierId, string filterStatus1, string filterStatus2, string filterStatus3, string filterStatus4, string filterStatus5, string filterStatus6, string filterStatus7, string filterStatus8)
        {
            throw new NotImplementedException();
        }

        Task<User> IUserRepository.GetUserDetailsByUserId(int userid)
        {
            throw new NotImplementedException();
        }

        Task<List<UserSubscriptionProductsViewModel>> IUserRepository.GetUserSubsriptionProductsBasedonUserId(int userId)
        {
            throw new NotImplementedException();
        }

        Task<SupplierViewModel> ISupplierRepository.SupplierLogin(SupplierLoginViewModel supplierLoginViewModel)
        {
            throw new NotImplementedException();
        }

        Task<SupplierOrderViewModel> ISupplierRepository.SupplierOrderCreation(SupplierOrderViewModel sovm)
        {
            throw new NotImplementedException();
        }

        Task<UserAdressViewModel> IUserRepository.UpdateAdressDetails(UserAdressViewModel userAdressViewModel)
        {
            throw new NotImplementedException();
        }

        Task<BrandViewModel> IAdminRepository.UpdateBrand(IFormFile image, BrandViewModel BvM)
        {
            throw new NotImplementedException();
        }

        Task<CategoryViewModel> IAdminRepository.UpdateCategory(IFormFile image, CategoryViewModel UCVM)
        {
            throw new NotImplementedException();
        }

        Task<ProductViewModel> IAdminRepository.UpdateProduct(IFormFile image, ProductViewModel UPVM)
        {
            throw new NotImplementedException();
        }

        Task<UpdateOrderStatusSupplierViewModel> ISupplierRepository.UpdatetheOrderStatusBySupplier(UpdateOrderStatusSupplierViewModel supplierOrderTable)
        {
            throw new NotImplementedException();
        }

        Task<UserViewModel> IUserRepository.UpdateUserDetails(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }
    }
}

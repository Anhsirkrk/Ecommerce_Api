using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface ISupplierRepository
    {
        Task<SupplierViewModel> AddSupplier(SupplierViewModel supplierViewModel);
        Task<Supplier> GetSupplierById(int SupplierId);
        List<SupplierWithPinCodesViewModel> GetSuppliersWithPinCodes();
        Task<SupplierOrderViewModel> SupplierOrderCreation(SupplierOrderViewModel sovm);
        Task<List<SupplierOrderDetailsViewModel>> GetSupplierOrderDetailsBySupplierId(int supplierId);
        Task<UpdateOrderStatusSupplierViewModel> UpdatetheOrderStatusBySupplier(UpdateOrderStatusSupplierViewModel supplierOrderTable);

        Task<SupplierViewModel> SupplierLogin(SupplierLoginViewModel supplierLoginViewModel);

    }
}

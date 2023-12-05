using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface ISupplierRepository
    {
        Task<SupplierViewModel> AddSupplier(SupplierViewModel supplierViewModel);
        Task<Supplier> GetSupplierById(int SupplierId);
        List<SupplierWithPinCodesViewModel>
            GetSuppliersWithPinCodes();
        Task<SupplierOrderViewModel> SupplierOrderCreation(SupplierOrderViewModel sovm);
        Task<List<SupplierOrderDetailsViewModel>> GetSupplierOrderDetailsBySupplierId(int supplierId, string filterStatus1, string filterStatus2, string filterStatus3, string filterStatus4, string filterStatus5, string filterStatus6, string filterStatus7, string filterStatus8);
        Task<UpdateOrderStatusSupplierViewModel> UpdatetheOrderStatusBySupplier(UpdateOrderStatusSupplierViewModel supplierOrderTable);

        Task<SupplierViewModel> SupplierLogin(SupplierLoginViewModel supplierLoginViewModel);

        Task<List<SupplierOrderDetailsViewModel>> GetTodaySupplierOrderDetailsBySupplierId(int supplierId, string filterStatus1, string filterStatus2, string filterStatus3, string filterStatus4, string filterStatus5, string filterStatus6, string filterStatus7, string filterStatus8);
    }
}

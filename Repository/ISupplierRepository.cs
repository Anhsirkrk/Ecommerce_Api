using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface ISupplierRepository
    {
        Task<SupplierViewModel> AddSupplier(SupplierViewModel supplierViewModel);
        Task<Supplier> GetSupplierById(int SupplierId);
        List<Supplier> GetAllSuppliers();
    }
}

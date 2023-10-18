using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class SupplierRepository:ISupplierRepository
    {
        private readonly EcommercedemoContext context;
        public SupplierRepository(EcommercedemoContext _context)
        {
            context = _context;
        }

        public async Task<SupplierViewModel> AddSupplier(SupplierViewModel supplierViewModel)
        {
            var parameters = new[]
            {
            new SqlParameter("@Name", supplierViewModel.Name),
            new SqlParameter("@Email", supplierViewModel.Email),
            new SqlParameter("@Mobile", supplierViewModel.Mobile),
            new SqlParameter("@JoinDate", supplierViewModel.JoinDate),
            new SqlParameter("@RegistrationAmountPaid", supplierViewModel.RegistrationAmountPaid),
            new SqlParameter("@ExpiryDate", supplierViewModel.ExpiryDate),
            new SqlParameter("@StatusOfRegistration", supplierViewModel.StatusOfRegistration),
            new SqlParameter("@PanCard", supplierViewModel.PanCard),
                new SqlParameter("@Licenceno", supplierViewModel.Licenceno)
            };

            context.Database.ExecuteSqlRaw("EXEC InsertSupplier @Name, @Email, @Mobile, @JoinDate, @RegistrationAmountPaid, @ExpiryDate, @StatusOfRegistration, @PanCard, @Licenceno", parameters);

            return supplierViewModel;
        }

       public async Task<Supplier> GetSupplierById(int SupplierId)
        {
            var supplierIdParam = new SqlParameter("@SupplierId", SupplierId);
            var supplier = context.Suppliers
        .FromSqlRaw("EXEC GetSupplierById @SupplierId", supplierIdParam)
         .AsEnumerable()
        .SingleOrDefault();

           
            return supplier;
            
        }


        public  List<Supplier> GetAllSuppliers()
        {
            return  context.Suppliers.FromSqlRaw("EXEC GetAllSuppliers").AsEnumerable().ToList();
        }
    }
}

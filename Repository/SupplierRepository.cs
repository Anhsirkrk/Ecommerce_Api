using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce_Api.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly EcommerceDailyPickContext context;
        public SupplierRepository(EcommerceDailyPickContext _context)
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


        public List<SupplierWithPinCodesViewModel> GetSuppliersWithPinCodes()
        {
            var suppliersWithPinCodes = new List<SupplierWithPinCodesViewModel>();

            var suppliers = context.Suppliers.ToList();

            foreach (var supplier in suppliers)
            {
                var pinCodes = context.SupplierPinCodes
                    .Where(sp => sp.SupplierId == supplier.SupplierId)
                    .Select(sp => sp.PinCodeOfSupply)
                    .ToList();

                var supplierWithPinCodes = new SupplierWithPinCodesViewModel
                {
                    SupplierId = supplier.SupplierId,
                    Name = supplier.Name,
                    Email = supplier.Email,
                    Mobile = supplier.Mobile,
                    JoinDate = (DateTime)supplier.JoinDate,
                    RegistrationAmountPaid = (decimal)supplier.RegistrationAmountPaid,
                    ExpiryDate = (DateTime)supplier.ExpiryDate,
                    StatusOfRegistration = supplier.StatusOfRegistration,
                    PanCard = supplier.PanCard,
                    Licenceno = supplier.Licenceno,
                    PinCodesOfSupply = pinCodes
                };

                suppliersWithPinCodes.Add(supplierWithPinCodes);
            }

            return suppliersWithPinCodes;
        }



        public async Task<SupplierOrderViewModel> SupplierOrderCreation(SupplierOrderViewModel sovm)
        {
            var insertedSupplierOrderIdParameter = new SqlParameter("@InsertedSupplier_order_ID", SqlDbType.Int);
            insertedSupplierOrderIdParameter.Direction = ParameterDirection.Output;

            var parameters = new[]
          {
            new SqlParameter("@Supplier_Id", sovm.SupplierId),
            new SqlParameter("@Order_Id", sovm.OrderId),
            new SqlParameter("@Amount_per_Order", sovm.SupplierOrderAmount),
            new SqlParameter("@Order_status", sovm.orderstatus),
            new SqlParameter("@Order_Payment_status", sovm.OrderPaymentStatus),
            new SqlParameter("@Order_type", sovm.SubscriptionTypeId),
            new SqlParameter("@Order_startdate", sovm.StartDate),
            new SqlParameter("@Order_enddate", sovm.EndDate),
                 insertedSupplierOrderIdParameter // Add the output parameter to the list of parameters
            };

            context.Database.ExecuteSqlRaw("EXEC SP_InsertSupplier_order_Table @Supplier_Id, @Order_Id, @Amount_per_Order, @Order_status, @Order_Payment_status, @Order_type, @Order_startdate, @Order_enddate,@InsertedSupplier_order_ID OUTPUT", parameters.ToArray());


            // Retrieve the inserted OrderId from the output parameter
            int insertedOrderId = (int)insertedSupplierOrderIdParameter.Value;

            // Assign the retrieved OrderId to the orderViewModel
            sovm.SupplierOrderID = insertedOrderId;

            return sovm;
        }

    }
}
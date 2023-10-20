using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{

    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDailyPickContext context;

        public OrderRepository(EcommerceDailyPickContext _context)
        {

            context = _context;
        }

        public async Task<OrderViewModel> AddOrderOfUser(OrderViewModel orderViewModel)
        {
            var parameters = new List<SqlParameter>
        {
            new SqlParameter("@UserId", orderViewModel.UserId),
            new SqlParameter("@SubscriptionTypeId", orderViewModel.SubscriptionTypeId),
            new SqlParameter("@TotalAmount", orderViewModel.TotalAmount),
            new SqlParameter("@OrderDate", orderViewModel.OrderDate),
            new SqlParameter("@StartDate", orderViewModel.StartDate),
            new SqlParameter("@EndDate", orderViewModel.EndDate),
            new SqlParameter("@OrderPaymentStatus", orderViewModel.OrderPaymentStatus),
            new SqlParameter("@TimeSlot", orderViewModel.TimeSlot),
            new SqlParameter("@AddressId", orderViewModel.AddressId),
            new SqlParameter("@SupplierId", orderViewModel.SupplierId),
            new SqlParameter("@ProductId", orderViewModel.ProductId),
            new SqlParameter("@ProductPrice", orderViewModel.ProductPrice),
            new SqlParameter("@Quantity", orderViewModel.Quantity),
            new SqlParameter("@SizeOfProduct", orderViewModel.SizeOfProduct),
        };

            // Call the stored procedure
            context.Database.ExecuteSqlRaw("EXEC SpInsertOrderAndOrderItem @UserId, @SubscriptionTypeId, @TotalAmount, @OrderDate, @StartDate, @EndDate, @OrderPaymentStatus, @TimeSlot, @AddressId, @SupplierId, @ProductId, @ProductPrice, @Quantity, @SizeOfProduct", parameters.ToArray());

            return orderViewModel;
        }
    }
}
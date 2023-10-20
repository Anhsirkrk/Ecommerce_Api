using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data;

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
            var insertedOrderIdParameter = new SqlParameter("@InsertedOrderId", SqlDbType.Int);
            insertedOrderIdParameter.Direction = ParameterDirection.Output;

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
        insertedOrderIdParameter // Add the output parameter to the list of parameters
    };

            // Call the stored procedure
            context.Database.ExecuteSqlRaw("EXEC SpInsertOrderAndOrderItem @UserId, @SubscriptionTypeId, @TotalAmount, @OrderDate, @StartDate, @EndDate, @OrderPaymentStatus, @TimeSlot, @AddressId, @SupplierId, @ProductId, @ProductPrice, @Quantity, @SizeOfProduct, @InsertedOrderId OUTPUT", parameters.ToArray());

            // Retrieve the inserted OrderId from the output parameter
            int insertedOrderId = (int)insertedOrderIdParameter.Value;

            // Assign the retrieved OrderId to the orderViewModel
            orderViewModel.OrderId = insertedOrderId;

            return orderViewModel;
        }

        //public async Task<OrderViewModel> GetOrderIdbaseonOrderDetails(OrderViewModel ovm)
        //{
        //    try
        //    {
        //        var gettingorderid = await context.Orders.OrderByDescending(x => x.OrderId).FirstOrDefaultAsync(x => x.UserId == ovm.UserId && x.SubscriptionTypeId == ovm.SubscriptionTypeId && x.TotalAmount == ovm.TotalAmount && x.OrderDate == ovm.OrderDate && x.StartDate == ovm.StartDate && x.EndDate == ovm.EndDate && x.OrderPaymentStatus == ovm.OrderPaymentStatus && x.TimeSlot == ovm.TimeSlot && x.AddressId == ovm.AddressId && x.SupplierId == ovm.SupplierId);
        //    }
        //}
    }


}
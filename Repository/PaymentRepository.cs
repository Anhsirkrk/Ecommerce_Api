using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly EcommerceDailyPickContext context;
        public PaymentRepository(EcommerceDailyPickContext _context)
        {
            context = _context;
        }

        public async Task<PaymentViewModel> UserPayment(PaymentViewModel paymentView)
        {
            try
            {
                if (paymentView != null)
                {
                    // Create a new Payment entity and map properties from PaymentViewModel
                    var payment = new Payment
                    {
                        // Map the properties as needed
                        OrderId = paymentView.OrderId,
                        // PaymentDate = paymentView.PaymentDate,
                        PaymentMethod = paymentView.PaymentMethod,
                        Amount = paymentView.Amount,
                        PaymentStatus = "Pending",
                        // PaymentStatus = paymentView.PaymentStatus,

                        // Generate a random TransactionId
                        TransactionId = GenerateRandomTransactionId()
                    };

                    // Set PaymentDate to the current date/time if not provided
                    payment.PaymentDate ??= DateTime.Now;

                    // Set a default payment status (e.g., "Pending") if not provided
                    // payment.PaymentStatus ??= "Sucess";

                    context.Payments.Add(payment);
                    await context.SaveChangesAsync();
                    var paymentViewModel = new PaymentViewModel
                    {
                        // Map the properties as needed
                        OrderId = payment.OrderId,
                        // PaymentDate = payment.PaymentDate ?? DateTime.Now,
                        PaymentMethod = payment.PaymentMethod,
                        Amount = payment.Amount,
                        // PaymentStatus = payment.PaymentStatus,
                        //TransactionId = payment.TransactionId
                    };

                    return paymentViewModel;

                }
                return null;
            }

            catch
            {
                throw;
            }
        }

        private string GenerateRandomTransactionId()
        {
            // Define characters for the TransactionId
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Define the length of the TransactionId
            const int length = 10; // Adjust the length as needed

            // Use a random number generator to create a random TransactionId
            Random random = new Random();
            string transactionId = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return transactionId;
        }
    }
}

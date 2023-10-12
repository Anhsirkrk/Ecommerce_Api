using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace Ecommerce_Api.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RazorpayClient razorpayClient;
        private readonly EcommerceDailyPickContext context;
        public PaymentRepository(EcommerceDailyPickContext _context, RazorpayClient _razorpayClient)
        {
            razorpayClient = _razorpayClient;
            context = _context;
        }



        public async Task<PaymentViewModel> UserPayment([FromBody] PaymentViewModel paymentView)
        {
            try
            {
                if (paymentView != null)
                {
                    // Create a Razorpay payment order
                    Random randomObj = new Random();
                    string transactionId = randomObj.Next(10000000, 100000000).ToString();
                    var amountInPaise = (int)(paymentView.Amount * 100);
                    var orderOptions = new Dictionary<string, object>
                {
                    { "amount", amountInPaise },
                    { "currency", "INR" },
                    { "receipt", transactionId },
                    // You can add additional options here as needed
                };

                    var order = razorpayClient.Order.Create(orderOptions);

                    // Capture the payment using the Razorpay order ID
                    //var paymentId = order["id"].ToString();
                    var order_id = order["id"].ToString();
                    var paymentOptions = new Dictionary<string, object>
                {
                      { "order_id", order_id },
                    { "amount", amountInPaise },
                     { "currency", "INR" },
                };

                    var capturedPayment = razorpayClient.Payment.Capture(paymentOptions);

                    var pid = razorpayClient.Payment["id"].ToString();
                    // Capture the payment using the Razorpay order and payment IDs.
                    var payment = razorpayClient.Payment.Fetch(pid);

                    if (payment["status"] == "captured")
                    {
                        // Payment was successful, store data in the 'Payment' table.
                        var paymentData = new Model.Payment
                        {
                            OrderId = paymentView.OrderId,
                            PaymentDate = DateTime.UtcNow,
                            PaymentMethod = "Razorpay",
                            Amount = paymentView.Amount, // Convert paise to rupees if necessary.
                            TransactionId = transactionId,
                            PaymentStatus = "Sucess",
                        };

                        // Set PaymentDate to the current date/time if not provided
                        paymentData.PaymentDate ??= DateTime.Now;

                        context.Payments.Add(paymentData);
                        await context.SaveChangesAsync();

                        var paymentViewModel = new PaymentViewModel
                        {
                            // Map the properties as needed
                            OrderId = paymentData.OrderId,
                            // PaymentDate = payment.PaymentDate ?? DateTime.Now,
                            PaymentMethod = paymentData.PaymentMethod,
                            Amount = paymentData.Amount,
                            // PaymentStatus = payment.PaymentStatus,
                            //TransactionId = payment.TransactionId
                        };

                        return paymentViewModel;
                    }

                    return null;
                }

                return null;
            }
            catch (Exception ex)
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

        //public async Task<PaymentViewModel> UserPayment(PaymentViewModel paymentView)
        //{
        //    try
        //    {
        //        if (paymentView != null)
        //        {
        //            // Create a new Payment entity and map properties from PaymentViewModel
        //            var payment = new Payment
        //            {
        //                // Map the properties as needed
        //                OrderId = paymentView.OrderId,
        //                // PaymentDate = paymentView.PaymentDate,
        //                PaymentMethod = paymentView.PaymentMethod,
        //                Amount = paymentView.Amount,
        //                PaymentStatus = "Pending",
        //                // PaymentStatus = paymentView.PaymentStatus,

        //                // Generate a random TransactionId
        //                TransactionId = GenerateRandomTransactionId()
        //            };

        //            // Set PaymentDate to the current date/time if not provided
        //            payment.PaymentDate ??= DateTime.Now;

        //            // Set a default payment status (e.g., "Pending") if not provided
        //            // payment.PaymentStatus ??= "Sucess";

        //            context.Payments.Add(payment);
        //            await context.SaveChangesAsync();
        //            var paymentViewModel = new PaymentViewModel
        //            {
        //                // Map the properties as needed
        //                OrderId = payment.OrderId,
        //                // PaymentDate = payment.PaymentDate ?? DateTime.Now,
        //                PaymentMethod = payment.PaymentMethod,
        //                Amount = payment.Amount,
        //                // PaymentStatus = payment.PaymentStatus,
        //                //TransactionId = payment.TransactionId
        //            };

        //            return paymentViewModel;

        //        }
        //        return null;
        //    }

        //    catch
        //    {
        //        throw;
        //    }
        //}


    }
}

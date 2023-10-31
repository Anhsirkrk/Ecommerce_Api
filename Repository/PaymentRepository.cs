using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

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

        public async Task<PaymentViewModel> UserPayment3([FromBody] PaymentViewModel paymentView)
        
        {
            var insertedPaymentIdParameter = new SqlParameter("@InsertedPaymentId", SqlDbType.Int);
            insertedPaymentIdParameter.Direction = ParameterDirection.Output;
            var insertedUserSubscriptionIdParameter = new SqlParameter("@InsertedUserSubscriptionId", SqlDbType.Int);
            insertedUserSubscriptionIdParameter.Direction = ParameterDirection.Output;

            var tid = GenerateRandomTransactionId();
            var parameters = new List<SqlParameter>
        {
            new SqlParameter("@OrderId", paymentView.OrderId),
            new SqlParameter("@PaymentDate", DateTime.UtcNow),
            new SqlParameter("@PaymentMethod", paymentView.PaymentMethod),
            new SqlParameter("@Amount", paymentView.Amount),
            new SqlParameter("@TransactionId",tid),
            new SqlParameter("@PaymentStatus", paymentView.PaymentStatus),
             insertedPaymentIdParameter,
             insertedUserSubscriptionIdParameter // Add the output parameter to the list of parameters
        };
            context.Database.ExecuteSqlRaw("EXEC SP_InsertPaymentAndUpdateOrderAndUserSubscription @OrderId, @PaymentDate, @PaymentMethod, @Amount, @TransactionId, @PaymentStatus, @InsertedPaymentId OUTPUT, @InsertedUserSubscriptionId OUTPUT", parameters.ToArray());
            // Retrieve the inserted PayemntId from the output parameter
            int insertedpaymentid = (int)insertedPaymentIdParameter.Value;

            // Retrieve the inserted UserSusbcriptionId from the output parameter
            int insertedUserSubscriptionid = (int)insertedUserSubscriptionIdParameter.Value;

            // Assign the retrieved OrderId to the orderViewModel
            paymentView.PaymentId = insertedpaymentid;
            paymentView.UserSubscriptionId = insertedUserSubscriptionid;
            paymentView.PaymentStatus = "Success";
            paymentView.TransactionId = tid;
            

            return paymentView;
        }
        public async Task<string> PaymentStatusEmail(int userid,string status,string amount)
        {
            try
            {
                var item= await context.Users.FirstOrDefaultAsync(x=>x.UserId== userid);
                if(item!=null)
                {
                    SendEmail(item.Email, status,amount);
                    return "Status";
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void SendEmail(string email, string status, string Amount)
        {
            string senderEmail = "ramakrishna.hds.15@gmail.com";
            string senderPassword = "xvdf bpib kdpx fyru";


            string toEmail = email;
            string subject = "Welcome to Our DailyPick Website!";
            string body = $"Your Payment for the order is {status}. Amount: {Amount}";


            MailMessage mail = new MailMessage(senderEmail, toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            try
            {
                smtp.Send(mail);
            }
            catch (SmtpException ex)
            {
                // Handle SMTP exceptions (e.g., invalid recipient, server not reachable, authentication issues, etc.)
                // Log the error and handle it appropriately.
                Console.WriteLine("SMTP Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the error and handle it appropriately.
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                mail.Dispose();
                smtp.Dispose();
            }
        }



        public async Task<PaymentViewModel> UserPayment2([FromBody] PaymentViewModel paymentView)
        {
            try
            {
                if (paymentView != null)
                {
                    // Create a Razorpay payment order
                    Random randomObj = new Random();
                    string transactionId = randomObj.Next(10000000, 100000000).ToString();
                    var amountInPaise = (int)(paymentView.Amount * 100);
                //    var orderOptions = new Dictionary<string, object>
                //{
                //    { "amount", amountInPaise },
                //    { "currency", "INR" },
                //    { "receipt", transactionId },
                //    // You can add additional options here as needed
                //};

                    //var order = razorpayClient.Order.Create(orderOptions);

                    // Capture the payment using the Razorpay order ID
                    //var paymentId = order["id"].ToString();
                //    var order_id = order["id"].ToString();
                //    var paymentOptions = new Dictionary<string, object>
                //{
                //      { "order_id", order_id },
                //    { "amount", amountInPaise },
                //     { "currency", "INR" },
                //};

                    //var capturedPayment = razorpayClient.Payment.Capture(paymentOptions);

                    //var pid = razorpayClient.Payment["id"].ToString();
                    // Capture the payment using the Razorpay order and payment IDs.
                    //var payment = razorpayClient.Payment.Fetch(pid);

                    //if (payment["status"] == "captured")
                    //{
                        // Payment was successful, store data in the 'Payment' table.
                        var paymentData = new Model.Payment
                        {
                            OrderId = paymentView.OrderId,
                            PaymentDate = DateTime.UtcNow,
                            PaymentMethod = "Razorpay",
                            Amount = paymentView.Amount, // Convert paise to rupees if necessary.
                            TransactionId = transactionId,
                            PaymentStatus = paymentView.PaymentStatus,
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
                    //}

                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
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
                            PaymentStatus = "Success",
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

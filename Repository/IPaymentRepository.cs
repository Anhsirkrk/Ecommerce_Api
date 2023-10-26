using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
namespace Ecommerce_Api.Repository
{
    public interface IPaymentRepository
    {
        //adding payment details of user 
        Task<PaymentViewModel> UserPayment(PaymentViewModel paymentView);
        Task<PaymentViewModel> UserPayment2(PaymentViewModel paymentView);

        Task<PaymentViewModel> UserPayment3(PaymentViewModel paymentView);
    }
}

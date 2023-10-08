
using Ecommerce_Api.Model;
namespace Ecommerce_Api.Repository

{
    public interface ISubscriptionRepository
    {
        //get the subscription_Types
        Task<List<SubscriptionType>> GetSubscriptionTypes();
    }
}

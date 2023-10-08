using Ecommerce_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly EcommerceDailyPickContext context;

        public SubscriptionRepository(EcommerceDailyPickContext _context)
        {
            context = _context;
        }
        //get the subscription_Types
        public async Task<List<SubscriptionType>> GetSubscriptionTypes()
        {
            try
            {

                var item = await context.SubscriptionTypes.ToListAsync();
                return item;


            }
            catch
            {
                throw;
            }
            return null;
        }

    }
}

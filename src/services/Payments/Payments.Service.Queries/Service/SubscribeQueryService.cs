using Payments.Service.Queries.DTOs;
using Payments.Service.Queries.IService;
using Service.Common.Collection;
using Service.Common.Mapping;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Service.Queries.Service
{
    public class SubscribeQueryService : ISubscribeQueryService
    {
        public async Task<dynamic> GetAllAsync(int page, int take)
        {
            var options = new SubscriptionListOptions
            {
                Limit = 100,
            };
            var service = new SubscriptionService();
            StripeList<Subscription> subscriptions = service.List(
                  options
                );
            return subscriptions;
        }

        public async Task<SubscribeDTO> GetAsync(string id)
        {
            var service = new SubscriptionService();
            var result =  (service.Get(id)).MapTo<SubscribeDTO>();
            return result;
        }
    }
}

using MediatR;
using Payment.Service.EventHandlers.Commands;
using Peyment.Persistence.Database;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Service.EventHandlers.EventHandler
{
    public class SubscribeUpdateEventHandler:INotificationHandler<SubScribeUpdateCommand>
    {
        private readonly ApplicationDbContext _context;
        public SubscribeUpdateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(SubScribeUpdateCommand notification, CancellationToken cancellationToken)
        {

            var options = new SubscriptionUpdateOptions
            { 
                
                Metadata = new Dictionary<string, string>
              {
                { "order_id", "6735" },
              },
            };
            var service = new SubscriptionService();
            service.Update("sub_IwM4sgU9duxkBg", options);
        }
    }
}

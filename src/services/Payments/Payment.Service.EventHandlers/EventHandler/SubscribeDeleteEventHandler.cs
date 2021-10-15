using MediatR;
using Payment.Service.EventHandlers.Commands;
using Peyment.Persistence.Database;
using Stripe;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Service.EventHandlers.EventHandler
{
    public class SubscribeDeleteEventHandler : INotificationHandler<SubScribeDeleteCommand>
    {
        private readonly ApplicationDbContext _context;
        public SubscribeDeleteEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(SubScribeDeleteCommand notification, CancellationToken cancellationToken)
        {
            var service = new SubscriptionService();
            service.Cancel(notification.id);
        }
    }
}

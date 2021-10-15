using Makaya.Resolver.Eum;
using Makaya.Resolver.Handlers;
using Makaya.Resolver.IExceptions;
using MediatR;
using Payment.Service.EventHandlers.Commands;
using Payments.Common;
using Payments.Domain;
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
    public class SubscribeCreateEventHandler :
        INotificationHandler<SubScribeCreateCommand>
    {
        private readonly ApplicationDbContext _context;
        public SubscribeCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(SubScribeCreateCommand notification, CancellationToken cancellationToken)
        {
            try
            {

                #region Tokens Card
                TokenCreateOptions options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = notification.cardnumber,
                        ExpMonth = notification.month,
                        ExpYear = notification.year,
                        Cvc = notification.cvc,
                        Currency = "usd"
                    },
                };
                TokenService service = new TokenService();
                Token strimptoken = service.Create(options);
                #endregion

                #region Customer
                CustomerCreateOptions customerption = new CustomerCreateOptions
                {
                    Name = notification.fullname,
                    Email = notification.email,
                    Description = notification.description,
                    Source = strimptoken.Id
                };
                CustomerService customerservice = new CustomerService();
                Customer custom = customerservice.Create(customerption);
                #endregion

                #region Payment Atach Method
                PaymentMethodAttachOptions paymentatch = new PaymentMethodAttachOptions
                {
                    Customer = custom.Id,
                };
                PaymentMethodService paymentmethodservice = new PaymentMethodService();
                PaymentMethod paymentMethod = paymentmethodservice.Attach(
                  strimptoken.Card.Id,
                  paymentatch
                );
                #endregion

                #region Subscription Create
                SubscriptionCreateOptions subscriptioncreateoption = new SubscriptionCreateOptions
                {
                    Customer = custom.Id,
                    Items = new List<SubscriptionItemOptions>
                    {
                        new SubscriptionItemOptions
                        {
                            Price = notification.iDPlanPrice,
                            Quantity = 1,

                        },
                    },
                };
                SubscriptionService subscriptionservice = new SubscriptionService();
                Subscription subscription = subscriptionservice.Create(subscriptioncreateoption);
                #endregion

                #region Insert DB 

                notification.CustomerID = subscription.CustomerId;
                notification.SubscribeStripeId = custom.Id;
                notification.idCardStripe = strimptoken.Card.Id;
                notification.SubscribeDate = Convert.ToDateTime(DateTime.Now);
                notification.state = (Int32)StateEnum.Activeted;
                notification.countscreen = ListGeneric.GetInstance().GetScreen(notification.TypePlan);

                await _context.Subscribes.AddAsync(new Subscribe
                {
                    AccountId = notification.AccountId,
                    PlanPriceSID = notification.iDPlanPrice,
                    CustomerID = notification.CustomerID,
                    SubscribeStripeId = notification.SubscribeStripeId,
                    idCardStripe = notification.idCardStripe,
                    SubscribeDate = Convert.ToDateTime(DateTime.Now),
                    state = (Int32)StateEnum.Activeted,
                    countscreen = notification.countscreen
                });
                await _context.SaveChangesAsync();
                #endregion
            }
            catch (Exception ex)
            {
                throw new ApiBusinessException(ex.GetHashCode().ToString(), ex.Message, System.Net.HttpStatusCode.NotFound, "Http");
            }
        }
    }
}

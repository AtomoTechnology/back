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
    public class PaymentSubscribeCreateEventHandler :
        INotificationHandler<PaymentSubScribeCommand>
    {
        private readonly ApplicationDbContext _context;
        public PaymentSubscribeCreateEventHandler(ApplicationDbContext context)
        {            
            _context = context;
        }
        public async Task Handle(PaymentSubScribeCommand notification, CancellationToken cancellationToken)
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
                this.Insert(subscription, notification, strimptoken.Card.Id);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private method
        private async void Insert(dynamic subscription, PaymentSubScribeCommand command, String CardId)
        {
            try
            {
                //dynamic str = subscription;
                //Subscribe entitystripe = Transform(str, command, CardId);
                //

                 _context.Add(new Subscribe {
                    AccountId = command.AccountId,
                    PlanPriceSID = command.iDPlanPrice,
                    CustomerID = subscription.CustomerId,
                    SubscribeId = subscription.Id,
                    idCardStripe = CardId,
                    SubscribeDate = Convert.ToDateTime(DateTime.Now),
                    state = (Int32)StateEnum.Activeted,
                    countscreen = ListGeneric.GetInstance().GetScreen(command.TypePlan)
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Transform
        private Subscribe Transform(dynamic custom, PaymentSubScribeCommand cm, String CardId)
        {
            Subscribe cust = new Subscribe()
            {
                AccountId = cm.AccountId,
                PlanPriceSID = cm.iDPlanPrice,
                CustomerID = custom.CustomerId,
                SubscribeId = custom.Id,
                idCardStripe = CardId,
                SubscribeDate = Convert.ToDateTime(DateTime.Now),
                state = (Int32)StateEnum.Activeted,
                countscreen = ListGeneric.GetInstance().GetScreen(cm.TypePlan)
            };


            return cust;
        }
        #endregion
    }
}

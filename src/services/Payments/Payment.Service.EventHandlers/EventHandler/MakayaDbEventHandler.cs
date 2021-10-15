using Makaya.Resolver.Eum;
using Makaya.Resolver.IExceptions;
using MediatR;
using Payment.Service.EventHandlers.Commands;
using Payments.Domain;
using Peyment.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Service.EventHandlers.EventHandler
{
    public class MakayaDbEventHandler : INotificationHandler<MakayaSubscribeDbCommand>
    {

        private readonly ApplicationDbContext _context;
        public MakayaDbEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(MakayaSubscribeDbCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Subscribes.AddAsync(new Subscribe
                {
                    AccountId = notification.AccountId,
                    PlanPriceSID = notification.PlanPriceSID,
                    CustomerID = notification.CustomerID,
                    SubscribeStripeId = notification.SubscribeStripeId,
                    idCardStripe = notification.idCardStripe,
                    SubscribeDate = Convert.ToDateTime(DateTime.Now),
                    state = (Int32)StateEnum.Activeted,
                    countscreen = notification.countscreen
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiBusinessException(ex.GetHashCode().ToString(), ex.Message, System.Net.HttpStatusCode.NotFound, "Http");
            }
           
        }

        #region Private method
        //public async void Insert(dynamic subscription, PaymentSubScribeCommand command, String CardId)
        //{
        //    try
        //    {
        //        await _context.AddAsync(new Subscribe
        //        {
        //            AccountId = command.AccountId,
        //            PlanPriceSID = command.iDPlanPrice,
        //            CustomerID = subscription.CustomerId,
        //            SubscribeId = subscription.Id,
        //            idCardStripe = CardId,
        //            SubscribeDate = Convert.ToDateTime(DateTime.Now),
        //            state = (Int32)StateEnum.Activeted,
        //            countscreen = ListGeneric.GetInstance().GetScreen(command.TypePlan)
        //        });
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion
    }
}

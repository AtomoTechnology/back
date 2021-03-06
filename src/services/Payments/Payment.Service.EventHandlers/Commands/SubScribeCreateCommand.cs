using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Service.EventHandlers.Commands
{
    public class SubScribeCreateCommand : INotification
    {
        public Int64 idPaymentIntent { get; set; }
        public String AccountId { get; set; }
        public String TypePlan { get; set; }
        public String iDPlanPrice { get; set; }
        public String fullname { get; set; }
        public String email { get; set; }
        public int countscreen { get; set; }
        public string idCardStripe { get; set; }
        public DateTime SubscribeDate { get; set; }
        public string SubscribeStripeId { get; set; }
        public string CustomerID { get; set; }     

        public String description { get; set; }


        #region new
        public String cardnumber { get; set; }
        public Int32 month { get; set; }
        public Int32 year { get; set; }
        public String cvc { get; set; }
        public Int32 value { get; set; }
        #endregion
        public Int32 state { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Service.EventHandlers.Commands
{
    public class MakayaSubscribeDbCommand : INotification
    {
        public String SubscribeId { get; set; }
        public String AccountId { get; set; }
        public String PlanPriceSID { get; set; }
        public String CustomerID { get; set; }
        public String SubscribeStripeId { get; set; }
        public String idCardStripe { get; set; }
        public DateTime SubscribeDate { get; set; }
        public Int32 countscreen { get; set; }
        public Int32 state { get; set; }
    }
}

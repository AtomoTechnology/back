using System;

namespace Payments.Domain
{
    public class Plan
    {
        public Int64 PlanId { get; set; }
        public String idProduct { get; set; }
        public Int64 AccountId { get; set; }
        public String idplanstripe { get; set; }
        public String TypePlan { get; set; }
        public Int64 Price { get; set; }
        public String Description { get; set; }
        public DateTime PlanDate { get; set; }
        public Int32 State { get; set; }


        public Product Pruduct { get; set; }
    }
}

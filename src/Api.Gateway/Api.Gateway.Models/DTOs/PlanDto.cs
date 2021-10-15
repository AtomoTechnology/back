using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Gateway.Models.DTOs
{
    public class PlanDto
    {
        public String idProduct { get; set; }
        public String idplanstripe { get; set; }
        public String TypePlan { get; set; }
        public Int64 Price { get; set; }
        public String Description { get; set; }
        public DateTime PlanDate { get; set; }
    }
}

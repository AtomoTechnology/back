using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain
{
    public class Product
    {
        public Int64 ProductId { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String urlimg { get; set; }
        public Boolean active { get; set; }
    }
}

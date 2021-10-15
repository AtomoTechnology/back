using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Service.EventHandlers.Commands
{
    public class SubScribeUpdateCommand : INotification
    {
        public String id { get; set; }
        public String customer { get; set; }
        public DateTime created { get; set; }
        public DateTime current_period_end { get; set; }
        public DateTime current_period_start { get; set; }
        public String nickname { get; set; }
        public String product { get; set; }
        public String collection_method { get; set; }
        public String url { get; set; }
        public DateTime start_date { get; set; }
        public DateTime status { get; set; }
    }
}

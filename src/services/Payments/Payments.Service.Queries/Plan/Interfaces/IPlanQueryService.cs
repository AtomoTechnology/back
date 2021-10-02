using Payments.Service.Queries.Plan.DTOs;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Service.Queries.Plan.Interfaces
{
    public interface IPlanQueryService
    {
        Task<List<PlanStripeDto>> GetAllAsync(int quantity);
    }
}

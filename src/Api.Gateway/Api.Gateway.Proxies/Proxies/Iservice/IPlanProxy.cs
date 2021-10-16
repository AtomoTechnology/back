using Api.Gateway.Models.Collections;
using Api.Gateway.Models.Command.Plan;
using Api.Gateway.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Proxies.Iservice
{
    public interface IPlanProxy
    {
        Task<List<PlanDto>> GetAllAsync(int quantity);
        Task<PlanDto> GetAsync(int id);
        Task CreateAsync(PlanCreateCommand command);
        Task UpdateAsync(PlanUpdateCreateCommand command);
        Task Delete(string id);
    }
}

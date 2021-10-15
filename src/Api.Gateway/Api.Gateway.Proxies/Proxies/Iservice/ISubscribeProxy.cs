using Api.Gateway.Models.Collections;
using Api.Gateway.Models.Command;
using Api.Gateway.Models.Command.Subscribe;
using Api.Gateway.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Proxies.Iservice
{
    public interface ISubscribeProxy
    {
        Task<DataCollection<PaymentSubScribeCommandDTO>> GetAllAsync(int page, int take);
        Task<PaymentSubScribeCommandDTO> GetAsync(int id);
        Task CreateAsync(SubScribeCreateCommand command);
        Task UpdateAsync(SubScribeUpdateCommand command);
        Task Delete(string id);
    }
}

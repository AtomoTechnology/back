using Payments.Service.Queries.DTOs;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Service.Queries.IService
{
    public interface ISubscribeQueryService
    {
        Task<dynamic> GetAllAsync(int page, int take);
        Task<SubscribeDTO> GetAsync(String id);
    }
}

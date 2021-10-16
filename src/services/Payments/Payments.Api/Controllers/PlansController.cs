using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payments.Service.Queries.DTOs;
using Payments.Service.Queries.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.Api.Controllers
{
    [Route("api/v1/Plans")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly ILogger<PlansController> _logger;
        private readonly IPlanQueryService _service;
        private readonly IMediator _mediator;

        public PlansController(ILogger<PlansController> logger, IPlanQueryService service,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _service = service;

        }
        [HttpGet]
        public async Task<List<PlanStripeDto>> GetAll(int quantity = 100)
        {
            var result = await _service.GetAllAsync(quantity);
            return result;
        }

    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Service.EventHandlers.Commands;
using Payments.Service.Queries.Plan.DTOs;
using Payments.Service.Queries.Plan.Interfaces;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.Api.Controllers
{
    [Route("api/v1/PaymentSubScribe")]
    [ApiController]
    public class PaymentSubScribeController : ControllerBase
    {
        private readonly ILogger<PaymentSubScribeController> _logger;
        private readonly IPlanQueryService _service;
        private readonly IMediator _mediator;

        public PaymentSubScribeController(ILogger<PaymentSubScribeController> logger, IPlanQueryService service,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _service = service;

        }
        [HttpGet]
        public async Task<List<PlanStripeDto>> GetAll(int quantity = 10)
        {
            return await _service.GetAllAsync(quantity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentSubScribeCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }
    }
}

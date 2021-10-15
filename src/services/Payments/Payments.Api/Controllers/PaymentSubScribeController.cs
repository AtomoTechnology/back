using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Service.EventHandlers.Commands;
using Payments.Service.Queries.DTOs;
using Payments.Service.Queries.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payments.Api.Controllers
{
    [Route("api/v1/PaymentSubScribe")]
    [ApiController]
    public class PaymentSubScribeController : ControllerBase
    {
        private readonly ILogger<PaymentSubScribeController> _logger;
        private readonly ISubscribeQueryService _service;
        private readonly IMediator _mediator;

        public PaymentSubScribeController(ILogger<PaymentSubScribeController> logger, ISubscribeQueryService service,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _service = service;

        }
        [HttpGet]
        public async Task<List<SubscribeDTO>> GetAll(int page = 1, int take = 10)
        {
            return await _service.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<SubscribeDTO> Get(String id)
        {
            return await _service.GetAsync(id);
        }


        [HttpPost]
        public async Task<IActionResult> Create(SubScribeCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubScribeUpdateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _mediator.Publish(new SubScribeDeleteCommand { id = id });
            return NoContent();
        }     
    }
}

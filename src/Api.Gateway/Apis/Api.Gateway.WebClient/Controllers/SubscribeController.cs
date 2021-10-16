using Api.Gateway.Models.Collections;
using Api.Gateway.Models.Command;
using Api.Gateway.Models.Command.Subscribe;
using Api.Gateway.Models.DTOs;
using Api.Gateway.Proxies.Proxies.Iservice;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/Subscribe")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeProxy _service;

        public SubscribeController(ISubscribeProxy service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<DataCollection<PaymentSubScribeCommandDTO>> GetAll(int page = 1, int take = 12)
        {
            return await _service.GetAllAsync(page, take);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SubScribeCreateCommand command)
        {
            await _service.CreateAsync(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(SubScribeUpdateCommand command)
        {
            await _service.UpdateAsync(command);
            return Ok();
        }
    }
}

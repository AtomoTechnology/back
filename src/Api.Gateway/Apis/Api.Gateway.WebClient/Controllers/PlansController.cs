using Api.Gateway.Models.Collections;
using Api.Gateway.Models.DTOs;
using Api.Gateway.Proxies.Proxies.Iservice;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/Plans")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanProxy _service;

        public PlansController(IPlanProxy service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<PlanDto>> GetAll(int quantity = 100)
        {
            var result = await _service.GetAllAsync(quantity);
            return result;
        }
    }
}

using Api.Gateway.Models.Collections;
using Api.Gateway.Models.Command.Plan;
using Api.Gateway.Models.DTOs;
using Api.Gateway.Proxies.Config;
using Api.Gateway.Proxies.Proxies.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Proxies.Service
{
    public class PlanProxy : IPlanProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public PlanProxy(
            HttpClient httpClient,
            IOptions<ApiUrls> apiUrls,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<PlanDto>> GetAllAsync(int quantity)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SubscribeUrl}api/v1/Plans?quantity={quantity}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<PlanDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public Task<PlanDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task CreateAsync(PlanCreateCommand command)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PlanUpdateCreateCommand command)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}

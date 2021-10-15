using Api.Gateway.Models.Collections;
using Api.Gateway.Models.Command;
using Api.Gateway.Models.Command.Subscribe;
using Api.Gateway.Models.DTOs;
using Api.Gateway.Proxies.Config;
using Api.Gateway.Proxies.Proxies.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Gateway.Proxies.Proxies.Service
{
    public class SubscribeProxy : ISubscribeProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public SubscribeProxy(
            HttpClient httpClient,
            IOptions<ApiUrls> apiUrls,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<PaymentSubScribeCommandDTO>> GetAllAsync(int page, int take)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SubscribeUrl}v1/PaymentSubScribe?page={page}&take={take}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<PaymentSubScribeCommandDTO>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<PaymentSubScribeCommandDTO> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SubscribeUrl}v1/PaymentSubScribe/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<PaymentSubScribeCommandDTO>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateAsync(SubScribeCreateCommand command)
        {
            var content = new StringContent(
               JsonSerializer.Serialize(command),
               Encoding.UTF8,
               "application/json"
           );

            var request = await _httpClient.PostAsync($"{_apiUrls.SubscribeUrl}v1/PaymentSubScribe", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(SubScribeUpdateCommand command)
        {
            var content = new StringContent(
              JsonSerializer.Serialize(command),
              Encoding.UTF8,
              "application/json"
          );

            var request = await _httpClient.PutAsync($"{_apiUrls.SubscribeUrl}v1/PaymentSubScribe", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task Delete(string id)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(new SubScribeDeleteCommand { Id = id }),
                 Encoding.UTF8,
                 "application/json"
             );
            var request = await _httpClient.PutAsync($"{_apiUrls.SubscribeUrl}v1/PaymentSubScribe", content);
            request.EnsureSuccessStatusCode();
        }
    }
}

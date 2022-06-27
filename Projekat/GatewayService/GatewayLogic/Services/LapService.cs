using DataLayer.DTOs;
using GatewayLogic.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GatewayLogic.Services
{
    public class LapService : ILapService
    {
        private readonly IHttpClientFactory _HttpClientFactory;

        public LapService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }
        public async Task<BaseResponse<bool>> AddLap(LapWriteDTO lap)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var content = JsonContent.Create<LapWriteDTO>(lap);

                using (var response = await httpClient.PostAsync("/laps", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return new BaseResponse<bool>
                        {
                            ResponseContent = true,
                            IsSuccess = true
                        };
                    }

                    return new BaseResponse<bool>
                    {
                        ErrorMessage = await response.Content?.ReadAsStringAsync(),
                        IsSuccess = false
                    };
                }
            }
            catch(Exception e)
            {
                return new BaseResponse<bool>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }
    }
}

using DataLayer.DTOs;
using DataLayer.Models;
using GatewayLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GatewayLogic.Services
{
    public class DriversService : IDriversService
    {
        private readonly IHttpClientFactory _HttpClientFactory;

        public DriversService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<BaseResponse<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>>> GetDrivers(string search)
        {
            try
            {
                var apiDtos = new List<DriverApiReadDTO>();
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/drivers?search=" + search)) 
                {
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModels = JsonConvert.DeserializeObject<ApiResponse<List<DriverModel>>>(responseContent);

                        foreach (var responseModel in responseModels.Response)
                        {
                            apiDtos.Add(responseModel.ConvertToReadDTO());
                        }
                    }
                    else
                    {
                        apiDtos = null;
                    }
                }

                var httpClient  = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var serviceDtos = new List<DriverReadDTO>();
                string errorMessage = null;

                using (var response = await httpClient.GetAsync("/drivers?search=" + search))
                {
                    var responseContent = await response.Content?.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        serviceDtos = JsonConvert.DeserializeObject<List<DriverReadDTO>>(responseContent);
                    }
                    else
                    {
                        serviceDtos = null;
                        errorMessage = responseContent;
                    }
                }

                if (apiDtos == null && serviceDtos == null)
                    return new BaseResponse<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>>
                    {
                        ErrorMessage = errorMessage,
                        IsSuccess = false
                    };

                return new BaseResponse<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>>
                {
                    ResponseContent = new GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>
                    {
                        ApiReponse = apiDtos,
                        ServiceResponse = serviceDtos
                    },
                    IsSuccess = true
                };
            }
            catch(Exception e)
            {
                return new BaseResponse<GatewayResponse<List<DriverApiReadDTO>, List<DriverReadDTO>>>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<BaseResponse<GatewayResponse<DriverApiReadDTO, DriverReadDTO>>> GetDriverByCode(string code)
        {
            try
            {
                DriverApiReadDTO driverApiDto = null;
                using (var response = await Common.SendApiRequest(_HttpClientFactory, "/drivers?search=" + code))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponse<List<DriverModel>>>(responseContent);
                        if (responseModel.Response.Count > 0)
                            driverApiDto = responseModel.Response[0].ConvertToReadDTO();
                    }
                }

                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");

                DriverReadDTO serviceDriverDTO = null;
                string errorMessage = null;
                using(var response = await httpClient.GetAsync("/drivers/" + code))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        serviceDriverDTO = JsonConvert.DeserializeObject<DriverReadDTO>(responseContent);
                    }
                    else
                    {
                        errorMessage = responseContent;
                    }
                }

                if (driverApiDto == null && serviceDriverDTO == null)
                    return new BaseResponse<GatewayResponse<DriverApiReadDTO, DriverReadDTO>>
                    {
                        ErrorMessage = errorMessage,
                        IsSuccess = false
                    };

                return new BaseResponse<GatewayResponse<DriverApiReadDTO, DriverReadDTO>>
                {
                    ResponseContent = new GatewayResponse<DriverApiReadDTO, DriverReadDTO>
                    {
                        ApiReponse = driverApiDto,
                        ServiceResponse = serviceDriverDTO
                    },
                    IsSuccess = true
                };
            }
            catch(Exception e)
            {
                return new BaseResponse<GatewayResponse<DriverApiReadDTO, DriverReadDTO>>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<BaseResponse<DriverWriteDTO>> AddDriver(DriverWriteDTO driver)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var content = JsonContent.Create<DriverWriteDTO>(driver);

                using(var response = await httpClient.PostAsync("/drivers", content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new BaseResponse<DriverWriteDTO>
                        {
                            ResponseContent = driver,
                            IsSuccess = true
                        };
                    }

                    return new BaseResponse<DriverWriteDTO>
                    {
                        ErrorMessage = await response.Content?.ReadAsStringAsync(),
                        IsSuccess = false
                    };
                }
            }
            catch(Exception e)
            {
                return new BaseResponse<DriverWriteDTO>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<BaseResponse<string>> DeleteDriver(int driverId)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");

                using(var response = await httpClient.DeleteAsync("/drivers/" + driverId))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return new BaseResponse<string>
                        {
                            ErrorMessage = await response.Content?.ReadAsStringAsync(),
                            IsSuccess = false
                        };
                    }

                    return new BaseResponse<string>
                    {
                        IsSuccess = true,
                        ResponseContent = await response.Content?.ReadAsStringAsync()
                    };

                }
            }
            catch(Exception e)
            {
                return new BaseResponse<string>
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<BaseResponse<DriverReadDTO>> EditDriver(int driverId, DriverEditDTO newDriverData)
        {
            try
            {
                var httpClient = _HttpClientFactory.CreateClient("ServiceHttpClient");
                var content = JsonContent.Create<DriverEditDTO>(newDriverData);

                using (var response = await httpClient.PutAsync("/drivers/" + driverId, content))
                {
                    var responseContent = await response.Content?.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new BaseResponse<DriverReadDTO>
                        {
                            ResponseContent = JsonConvert.DeserializeObject<DriverReadDTO>(responseContent),
                            IsSuccess = true
                        };
                    }

                    return new BaseResponse<DriverReadDTO>
                    {
                        ErrorMessage = responseContent,
                        IsSuccess = false
                    };
                }
            }
            catch(Exception e)
            {
                return new BaseResponse<DriverReadDTO>
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
            }
        }
    }
}

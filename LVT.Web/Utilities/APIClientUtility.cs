using LVT.Web.Models;
using LVT.Models.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LVT.Web.Utilities
{
    public class APIClientUtility : IAPIClientUtility
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        string _baseUrl = string.Empty;
        public APIClientUtility(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _baseUrl = _configuration.GetValue<string>("API_Base_Url");
        }

        public async Task<APIResponseModel> LoginRequest(APIRequestModel ReqModel, StringContent Content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                APIResponseModel response = new APIResponseModel();

                HttpResponseMessage Res = await client.PostAsync(ReqModel.MethodName, Content);
                if (Res.IsSuccessStatusCode)
                {
                    string EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    response.Object = EmpResponse;
                }
                else
                {
                    response.Object = null;
                }
                return response;
            }

        }
        public async Task<APIResponseModel> GetRequest(APIRequestModel ReqModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("UserId", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("AuthUserId")));
                client.DefaultRequestHeaders.Add("Key", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Key")));
                client.DefaultRequestHeaders.Add("Token", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Token")));

                APIResponseModel response = new APIResponseModel();
                HttpResponseMessage Res = await client.GetAsync(ReqModel.MethodName);
                if (Res.IsSuccessStatusCode)
                {
                    string EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    response.Object = EmpResponse;
                }
                else
                {
                    response.Object = null;
                }
                return response;

            }

        }
        public async Task<APIResponseModel> PostRequestB(APIRequestModel ReqModel, StringContent Content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //client.DefaultRequestHeaders.Add("vehicleAlarmsFilter", Convert.ToString(JsonConvert.SerializeObject(filter)));
                //client.DefaultRequestHeaders.Add("Key", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Key")));
                //client.DefaultRequestHeaders.Add("Token", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Token")));

                APIResponseModel response = new APIResponseModel();


                HttpResponseMessage Res = await client.PostAsync(ReqModel.MethodName, Content);
                if (Res.IsSuccessStatusCode)
                {
                    HttpContent content = Res.Content;
                    var EmpResponse = content.ReadAsStringAsync();
                    //response.Object = EmpResponse;
                }
                else
                {
                    response.Object = null;
                }
                return response;

            }

        }

        public async Task<APIResponseModel> PostRequest(APIRequestModel ReqModel, StringContent Content)
        {



            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(_baseUrl + ReqModel.MethodName.Remove(0, 1)),
                Content = Content
            };

            requestMessage.Content.Headers.ContentType =
         new System.Net.Http.Headers.MediaTypeHeaderValue(
             "application/json");

            requestMessage.Content.Headers.TryAddWithoutValidation(
           "x-custom-header", "value");

            APIResponseModel responseModel = new APIResponseModel();
            using (var client = new HttpClient())
            {
                
                var response = await client.SendAsync(requestMessage);
                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                responseModel.Object = responseBody;



            }

            return responseModel;

        }
        public async Task<APIResponseModel> PutRequest(APIRequestModel ReqModel, StringContent Content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("UserId", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("AuthUserId")));
                client.DefaultRequestHeaders.Add("Key", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Key")));
                client.DefaultRequestHeaders.Add("Token", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Token")));
                APIResponseModel response = new APIResponseModel();

                HttpResponseMessage Res = await client.PutAsync(ReqModel.MethodName, Content);
                if (Res.IsSuccessStatusCode)
                {
                    string EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    response.Object = EmpResponse;
                }
                else
                {
                    response.Object = null;
                }
                return response;

            }

        }
        public async Task<APIResponseModel> DeleteRequest(APIRequestModel ReqModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("UserId", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("AuthUserId")));
                client.DefaultRequestHeaders.Add("Key", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Key")));
                client.DefaultRequestHeaders.Add("Token", Convert.ToString(_httpContextAccessor.HttpContext.Session.Get("Token")));
                APIResponseModel response = new APIResponseModel();

                HttpResponseMessage Res = await client.DeleteAsync(ReqModel.MethodName);
                if (Res.IsSuccessStatusCode)
                {
                    string EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    response.Object = EmpResponse;
                }
                else
                {
                    response.Object = null;
                }
                return response;
            }

        }
        public async Task<List<T>> GetResponse<T>(APIRequestModel ReqModel, StringContent content)
        {
            APIResponseModel response = await PostRequest(ReqModel, content);
            if (response.Object != null)
            {
                var Responsess = JsonConvert.DeserializeObject<APIResponseModel>(response.Object);
                if (Responsess.Object != null)
                    return JsonConvert.DeserializeObject<List<T>>(Responsess.Object);
            }
            return default(List<T>);
        }
        public async Task<List<T>> GetResponse<T>(APIRequestModel ReqModel)
        {
            APIResponseModel response = await GetRequest(ReqModel);
            if (response.Object != null)
            {
                var Responsess = JsonConvert.DeserializeObject<APIResponseModel>(response.Object);
                if (Responsess.Object != null)
                    return JsonConvert.DeserializeObject<List<T>>(Responsess.Object);
            }
            return default(List<T>);
        }

        public string JsonResponse(string sEcho, int totalRecords, string status, string message, object data)
        {
            return JsonConvert.SerializeObject(new
            {
                sEcho = sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = data,
                Status = status,
                Message = message
            });
        }
    }
}

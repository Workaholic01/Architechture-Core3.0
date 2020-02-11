using LVT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LVT.Web.Utilities
{
    public interface IAPIClientUtility
    {
        Task<APIResponseModel> LoginRequest(APIRequestModel ReqModel, StringContent Content);
        Task<APIResponseModel> GetRequest(APIRequestModel ReqModel);
        Task<APIResponseModel> PostRequest(APIRequestModel ReqModel, StringContent Content);
        Task<APIResponseModel> PutRequest(APIRequestModel ReqModel, StringContent Content);
        Task<APIResponseModel> DeleteRequest(APIRequestModel ReqModel);
        Task<List<T>> GetResponse<T>(APIRequestModel ReqModel, StringContent content);

        Task<List<T>> GetResponse<T>(APIRequestModel ReqModel);

        string JsonResponse(string sEcho, int totalRecords, string status, string message, object data);
    }
}

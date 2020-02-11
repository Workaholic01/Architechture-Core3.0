using LVT.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LVT.Services.Common
{
    public class APIResponseMessages
    {
        public static APIResponse CreateSuccessResponse(Object response)
        {
            return new APIResponse()
            {
                Message = "Success",
                Code = 0,
                Object = response == null ? "null" : JsonConvert.SerializeObject(response),
                ObjectType = response == null ? "null" : response.GetType().Name
            };
        }
        public static APIResponse CreateSuccessResponse(string message, Object response)
        {
            return new APIResponse()
            {
                Message = message,
                Code = 0,
                Object = response == null ? "null" : JsonConvert.SerializeObject(response),
                ObjectType = response == null ? "null" : response.GetType().Name
            };
        }

        public static APIResponse CreateErrorResponse(string message, Object response)
        {
            return new APIResponse()
            {
                Message = message,
                Code = -1,
                Object = response == null ? "null" : JsonConvert.SerializeObject(response),
                ObjectType = response == null ? "null" : response.GetType().Name
            };
        }
    }
}

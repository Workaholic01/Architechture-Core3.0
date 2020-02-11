using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVT.Web.Models
{
    public class APIRequestModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string MethodName { get; set; }
        public DateTime RequestTime { get; set; }
    }
}

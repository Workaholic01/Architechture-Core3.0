using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LVT.Models.Entities;
using LVT.Models.Filters;
using LVT.Web.Models;
using LVT.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LVT.Web.Controllers
{
    public class AlarmController : Controller
    {
        private readonly IAPIClientUtility _apiClientUtility;
        private readonly IConfiguration _configuration;

        public AlarmController(IAPIClientUtility apiClientUtility , IConfiguration configuration)
        {
            _apiClientUtility = apiClientUtility;
            _configuration = configuration;
        }
        public async Task<ActionResult> Index()
        {
            VehicleAlarmsFilter vehicleAlarmsFilter = new VehicleAlarmsFilter() ;
            vehicleAlarmsFilter.PageNo = 1;
            vehicleAlarmsFilter.PageSize = 10;
            var model = JsonConvert.SerializeObject(vehicleAlarmsFilter);
            var content = new StringContent(model, System.Text.Encoding.UTF8, "application/json");
            var requestModel = new APIRequestModel();
            requestModel.MethodName = _configuration.GetValue<string>("get_alarms");
            var data = await _apiClientUtility.GetResponse<VehicleAlarm>(requestModel, content);
            return View(data);
        }

        public async Task<ActionResult> GetAlarms(VehicleAlarmsFilter vehicleAlarmsFilter)
        {
            try
            {
                var model = JsonConvert.SerializeObject(vehicleAlarmsFilter);
                var content = new StringContent(model, System.Text.Encoding.UTF8, "application/json");
                var requestModel = new APIRequestModel();

                requestModel.MethodName = _configuration.GetValue<string>("get_alarms");
                var data = await _apiClientUtility.GetResponse<VehicleAlarm>(requestModel, content);

                return Json( _apiClientUtility.JsonResponse("Echo", data.Count, "SUCCESS", "Data Found", data));
            }
            catch (Exception ex)
            {

                return Json("Error : " + ex.Message , new JsonSerializerSettings() { });
            }
            
        }


    }
}
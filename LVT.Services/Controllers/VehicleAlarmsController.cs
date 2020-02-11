using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using LVT.Core.Alarm;
using LVT.Models.Common;
using LVT.Models.DTO;
using LVT.Models.Entities;
using LVT.Models.Filters;
using LVT.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAlarmsController : ControllerBase
    {
        private IAlarmService _alarmService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public VehicleAlarmsController(IAlarmService alarmService , IMapper mapper , IConfiguration configuration)
        {
            _alarmService = alarmService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/alarms/push")]
        public IActionResult Push([FromForm]VehicleAlarmViewModel vehicleAlarmViewModel)
        {
            try
            {
                if (vehicleAlarmViewModel == null)
                {
                    return BadRequest("vehicleAlarm object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                // Getting Image
                var image = vehicleAlarmViewModel.Image;
    
                // Saving Image on Server
                if (image.Length > 0)
                {
                  string filePath = Path.Combine(_configuration.GetValue<string>("ImageFilePath"), image.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    VehicleAlarm vehicleAlarm = _mapper.Map<VehicleAlarm>(vehicleAlarmViewModel);
                    vehicleAlarm.ImagePath = filePath;
                    _alarmService.PushAlarm(vehicleAlarm);
                }
                return StatusCode(201, "Processed");
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("/alarms")]
        public string GetAll(VehicleAlarmsFilter vehicleAlarmsFilter)
        {
            try
            {
                List<VehicleAlarmViewModel> lstVehicleAlarmViewModel = null;
               List<VehicleAlarm> lstAlarms =   _alarmService.GetAll(vehicleAlarmsFilter);

                if (lstAlarms.Count > 0)
                    lstVehicleAlarmViewModel = _mapper.Map<List<VehicleAlarmViewModel>>(lstAlarms);
                return JsonConvert.SerializeObject( ApiResponseUtility.CreateSuccessResponse(lstVehicleAlarmViewModel));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ApiResponseUtility.CreateErrorResponse(ex));
                
            }
        }


        [HttpGet]
        [Route("/get_alarms")]
        public IActionResult GetAlarms(VehicleAlarmsFilter vehicleAlarmsFilter)
        {
            try
            {
                List<VehicleAlarmViewModel> lstVehicleAlarmViewModel = null;
                List<VehicleAlarm> lstAlarms = _alarmService.GetAll(vehicleAlarmsFilter);

                if (lstAlarms.Count > 0)
                    lstVehicleAlarmViewModel = _mapper.Map<List<VehicleAlarmViewModel>>(lstAlarms);
              

                if (lstVehicleAlarmViewModel.Count > 0)
                {
                    ApiResponseUtility.CreateSuccessResponse(lstVehicleAlarmViewModel);
                }

                return StatusCode(201, "Processed");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost]
        [Route("/alarms/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VehicleAlarms
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/VehicleAlarms/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

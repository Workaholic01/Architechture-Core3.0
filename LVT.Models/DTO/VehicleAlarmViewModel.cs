using Microsoft.AspNetCore.Http;
using System;


namespace LVT.Models.DTO
{
    public class VehicleAlarmViewModel
    {
        public Guid Id { get; set; }
        public string RegistrationNo { get; set; }
        public string Color { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public string OwnerName { get; set; }
        public string Model { get; set; }
        public string ReportedOn { get; set; }
        public string FirPoliceStation { get; set; }
        public string FirNO { get; set; }
        public string InvestigatorName { get; set; }
        public string Status { get; set; }
        public string AlarmCameraId { get; set; }
        public string AlarmLocation { get; set; }
        public string AlarmGenerationTime { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
}

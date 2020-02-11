
using AutoMapper;
using LVT.Models.DTO;
using LVT.Models.Entities;

namespace LVT.Services.Mapper
{
    public class VehicleAlarmsProfile : Profile
    {
        public VehicleAlarmsProfile()
        {
            CreateMap<VehicleAlarm, VehicleAlarmViewModel>();
            CreateMap<VehicleAlarmViewModel , VehicleAlarm>();
        }
    }
}

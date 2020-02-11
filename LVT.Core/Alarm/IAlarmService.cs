
using LVT.Models.Entities;
using LVT.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LVT.Core.Alarm
{
    public interface IAlarmService
    {
        int PushAlarm(VehicleAlarm vehicleAlarm);

        List<VehicleAlarm> GetAll(VehicleAlarmsFilter vehicleAlarmFilter);
    }
}

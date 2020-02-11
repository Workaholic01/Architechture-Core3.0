using LVT.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LVT.Models.Filters
{
    public class VehicleAlarmsFilter
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string AlarmType { get; set; }
        public DateRange DateRange { get; set; }

    }
}

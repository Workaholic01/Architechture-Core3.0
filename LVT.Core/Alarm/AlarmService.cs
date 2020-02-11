using LVT.Data.RepositoryWrapper;
using LVT.Models.Entities;
using LVT.Models.Filters;
using System;
using System.Collections.Generic;


namespace LVT.Core.Alarm
{
    public class AlarmService : IAlarmService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AlarmService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<VehicleAlarm> GetAll(VehicleAlarmsFilter vehicleAlarmFilter)
        {
            try
            {
                return _repositoryWrapper.VehicleAlarms.GetPaged(vehicleAlarmFilter.PageNo, vehicleAlarmFilter.PageSize);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int PushAlarm(VehicleAlarm vehicleAlarm)
        {
            int rowsEffected = 0;
            try
            {
                _repositoryWrapper.VehicleAlarms.Create(vehicleAlarm);
                rowsEffected = _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return rowsEffected;
        }
    }
}

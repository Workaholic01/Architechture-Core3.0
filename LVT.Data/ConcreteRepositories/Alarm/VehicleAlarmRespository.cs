using LVT.Data.Context;
using LVT.Data.RepositoryBase;
using LVT.Models.Entities;

namespace LVT.Data.ConcreteRepositories.Alarm
{
    public class VehicleAlarmRespository : RepositoryBase<VehicleAlarm>, IVehicleAlarmRepository
    {
        public VehicleAlarmRespository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}

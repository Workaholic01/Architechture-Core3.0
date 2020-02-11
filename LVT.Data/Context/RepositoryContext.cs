
using LVT.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LVT.Data.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<VehicleAlarm> Alarms { get; set; }
    }
}

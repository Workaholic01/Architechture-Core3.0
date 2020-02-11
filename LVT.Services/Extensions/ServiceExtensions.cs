using AutoMapper;
using LVT.Core.Alarm;
using LVT.Data.Context;
using LVT.Data.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LVT.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddEntityFrameworkSqlServer().AddDbContext<RepositoryContext>(x => x.UseSqlServer(config["ConnectionStrings:DefaultConnection"]));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAlarmService, AlarmService>();
            services.AddAutoMapper(typeof(Startup));
            
        }
    }
}

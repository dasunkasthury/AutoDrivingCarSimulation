using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Core.Services;
using AutoDrivingCarSimulator.Core.Services.Concretes;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDrivingCarSimulator.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register repositories
            // use singleton since no session data is required and it is fine to generate dependencies one time 
            services.AddSingleton<ISimulatorService, SimulatorService>();
            services.AddSingleton<ISimulatorRepository, SimulatorRepository>();
            services.AddAutoMapper(typeof(Program));
            services.AddSingleton<AutoDrivingCarApp>();

            return services;
        }
    }
}

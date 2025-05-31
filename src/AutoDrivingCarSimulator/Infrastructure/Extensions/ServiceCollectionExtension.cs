using AutoDrivingCarSimulator.Core.Services;
using AutoDrivingCarSimulator.Core.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDrivingCarSimulator.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register repositories
            services.AddSingleton<ISimulatorService, SimulatorService>();

            return services;
        }
    }
}

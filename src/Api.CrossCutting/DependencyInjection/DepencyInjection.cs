using Domain.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IWebHostEnvironment environment)
        {
            services.AddSingleton<SigningConfigurations>();
            services.ConfigureDependenciesRepository(environment);
            services.ConfigureDependenciesService();

            return services;
        }
    }
}

using CrossCutting.Mappings;
using Domain.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IWebHostEnvironment environment)
        {
            services.AddSingleton<SigningConfigurations>();
            services.ConfigureDependenciesRepository(environment);
            services.ConfigureDependenciesService();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            return services;
        }
    }
}

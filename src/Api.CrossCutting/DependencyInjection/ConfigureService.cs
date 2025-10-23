using Domain.Interfaces.Services.Cep;
using Domain.Interfaces.Services.Municipio;
using Domain.Interfaces.Services.Uf;
using Domain.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureDependenciesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();

            serviceCollection.AddTransient<IUfService, UfService>();
            serviceCollection.AddTransient<IMunicipioService, MunicipioService>();
            serviceCollection.AddTransient<ICepService, CepService>();

            return serviceCollection;
        }
    }
}

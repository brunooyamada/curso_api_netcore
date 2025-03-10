using Data.Context;
using Data.Repository;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            serviceColletion.AddDbContext<MyContext>(options => options.UseNpgsql("Server=localhost;Port=5432;Database=dbApi;Uid=postgres;Pwd=masterkey"));
        }
    }
}

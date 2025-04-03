using Data.Context;
using Data.Implementations;
using Data.Repository;
using Domain.Interfaces;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            var banco = Environment.GetEnvironmentVariable("DATABASE")?.ToLower() ?? "";
            var conexao = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? "Server=localhost;Port=5432;Database=dbApi2;Uid=postgres;Pwd=masterkey";


            if (banco == "SQLSERVER".ToLower())
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer(conexao)
                );
            }
            else
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseNpgsql(conexao)
                    //options => options.UseNpgsql("Server=localhost;Port=5432;Database=dbApi2;Uid=postgres;Pwd=masterkey")
                    //options => options.UseSqlServer("Password=masterkey;Persist Security Info=True;User ID=sa;Initial Catalog=dbApi;Data Source=bruno\\sqlexpress;TrustServerCertificate=True")
                );
            }
            
        }
    }
}

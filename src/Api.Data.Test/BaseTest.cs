using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;

public abstract class BaseTest
{
    protected BaseTest()
    {
        
    }
}

public class DbTeste : IDisposable
{
    private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<MyContext>(o =>
            o.UseNpgsql($"Server=localhost;Port=5432;Database={dataBaseName};Uid=postgres;Pwd=masterkey"), 
                ServiceLifetime.Transient
        );

        ServiceProvider = serviceCollection.BuildServiceProvider();
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context.Database.EnsureCreated();
        }
    }

    public void Dispose()
    {
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context.Database.EnsureDeleted();
        }
    }
}

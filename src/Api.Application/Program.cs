using AutoMapper;
using CrossCutting.DependencyInjection;
using CrossCutting.Mappings;
using Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Environment);
builder.Services.AddInfrastructureJWT(builder.Configuration);

// Movido para helpers/StartupConfigurationHelper
// if (_envionment.IsEnvironment("Testing"))
// {
//     Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=True;Server=localhost;Port=5432;Database=dbApi_Integration;Uid=postgres;Pwd=masterkey");
//     Environment.SetEnvironmentVariable("DATABASE", "Postgres");
//     Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
//     Environment.SetEnvironmentVariable("Audience", "ExampleAudience");
//     Environment.SetEnvironmentVariable("Issuer", "ExampleIssuer");
//     Environment.SetEnvironmentVariable("Seconds", "28800");
// }

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureSwagger(builder.Configuration, builder.Environment);

//ConfigureService.ConfigureDependenciesService(builder.Services);
//ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModelProfile());
    cfg.AddProfile(new EntityToDtoProfile());
    cfg.AddProfile(new ModelToEntityProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var conexao = Environment.GetEnvironmentVariable("MIGRATION")?.ToLower() ?? "";

if (conexao == "APLICAR".ToLower())
{
    using (var service = app.Services.GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        using (var context = service.ServiceProvider.GetService<MyContext>())
        {
            context.Database.Migrate();
        }
    }
}

app.Run();

using CrossCutting.DependencyInjection;
using Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Curso de API com AspNetCore 8 - Na prática",
        Description = "Arquitetura DDD",
        TermsOfService = new Uri("http://www.mfrinfo.com.br"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Marcos Fabricio Rosa",
            Email = "mfrinfo@mail.com",
            Url = new Uri("http://www.mfrinfo.com.br"),
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Termo de licença de Uso",
            Url = new Uri("http://www.mfrinfo.com.br"),
        }
    }
));
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

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

app.Run();

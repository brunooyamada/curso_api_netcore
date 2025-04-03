using AutoMapper;
using CrossCutting.DependencyInjection;
using CrossCutting.Mappings;
using Data.Context;
using Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Curso de API com AspNetCore 8 - Na prática",
        Description = "Arquitetura DDD",
        TermsOfService = new Uri("http://www.mfrinfo.com.br"),
        Contact = new OpenApiContact
        {
            Name = "Marcos Fabricio Rosa",
            Email = "mfrinfo@mail.com",
            Url = new Uri("http://www.mfrinfo.com.br"),
        },
        License = new OpenApiLicense
        {
            Name = "Termo de licença de Uso",
            Url = new Uri("http://www.mfrinfo.com.br"),
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o Token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme 
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});

ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModelProfile());
    cfg.AddProfile(new EntityToDtoProfile());
    cfg.AddProfile(new ModelToEntityProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations")
).Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero; // Tempo de tolerância para expiração de um token (utilizado caso haja problemas de sincronismo de horário entre diferentes computadores envolvidos no processo de comunicação)
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

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

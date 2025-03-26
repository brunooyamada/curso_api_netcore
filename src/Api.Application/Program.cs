using CrossCutting.DependencyInjection;
using Data.Context;
using Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

app.Run();

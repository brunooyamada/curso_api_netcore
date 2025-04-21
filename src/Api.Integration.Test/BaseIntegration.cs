using Api.Application.Controllers;
using Api.Application.Helpers;
using AutoMapper;
using CrossCutting.DependencyInjection;
using CrossCutting.Mappings;
using Data.Context;
using Domain.Dtos;
using Domain.Interfaces.Services.User;
using Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Service.Services;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext myContext { get; private set; }
        public HttpClient client { get; private set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }
        public BaseIntegration()
        {
            hostApi = "https://localhost:7201/api/";

            var connectionString = "Server=localhost;Port=5432;Database=dbApi;Uid=postgres;Pwd=masterkey";

            StartupConfigurationHelper.ConfigureEnvironment(new FakeWebHostEnvironment { EnvironmentName = "Testing" });

            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Carrega configurações se quiser (ex: appsettings.Testing.json)
                    config.AddEnvironmentVariables("Testing");
                })
                .ConfigureServices(services =>
                {
                    services.AddControllers()
                        .AddApplicationPart(typeof(LoginController).Assembly);

                    // Mapper manual
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new DtoToModelProfile());
                        cfg.AddProfile(new EntityToDtoProfile());
                        cfg.AddProfile(new ModelToEntityProfile());
                    });

                    ConfigureService.ConfigureDependenciesService(services);
                    ConfigureRepository.ConfigureDependenciesRepository(services);

                    // Authorization
                    // Configuração de autenticação JWT
                    var signingConfigurations = new SigningConfigurations();
                    services.AddSingleton(signingConfigurations);

                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            // Supondo que você tenha as variáveis de ambiente configuradas corretamente para a validação
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
                                ValidAudience = Environment.GetEnvironmentVariable("Audience"),
                                IssuerSigningKey = signingConfigurations.Key,
                                ClockSkew = TimeSpan.Zero
                            };
                        });
                    //

                    // Adiciona jwt
                    services.AddAuthorization(options => 
                    {
                        options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build());
                    });
                    //

                    // Registre SigningConfigurations no DI
                    services.AddSingleton<SigningConfigurations>();

                    IMapper mapper = config.CreateMapper();
                    services.AddSingleton(mapper);
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseAuthentication(); // <- ESSENCIAL
                    app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });


            var server = new TestServer(builder);
            myContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;
            myContext.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            client = server.CreateClient();
        }

        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto()
            {
                Email = "mfrinfo@mail.com"
            };

            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.accessToken);
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object dataClass, string url, HttpClient client)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataClass), System.Text.Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            myContext.Dispose();
            client.Dispose();
        }
    }

    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new DtoToModelProfile());
            });
            return config.CreateMapper();
        }
        public void Dispose()
        {
            // Clean up resources if needed
        }
    }

    public class FakeWebHostEnvironment : IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Testing";
        public string ApplicationName { get; set; } = "TestApp";
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get; set; }
        public string WebRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider WebRootFileProvider { get; set; }
    }
}

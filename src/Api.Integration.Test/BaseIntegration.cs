using Api.Application.Controllers;
using Api.Application.Helpers;
using AutoMapper;
using CrossCutting.DependencyInjection;
using Data.Context;
using Domain.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext myContext { get; private set; }
        public HttpClient client { get; private set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }
        public IConfiguration configuration;

        public BaseIntegration()
        {
            configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
            {
                {"ConnectionStrings:DefaultConnection", 
                    "Server=localhost;Port=5432;Database=dbApi_Integration;Uid=postgres;Pwd=masterkey" },
            }).Build();

            hostApi = "https://localhost:7201/api/";

            var environment = new FakeWebHostEnvironment { EnvironmentName = "Testing" };
            StartupConfigurationHelper.ConfigureEnvironment(environment, null);

            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Carrega configurações se quiser (ex: appsettings.Testing.json)
                    config.AddEnvironmentVariables("Testing");
                })
                .ConfigureServices(services =>
                {
                    services.AddInfrastructure(environment);
                    services.AddInfrastructureJWT(configuration);
                    services.AddInfrastructureSwagger(configuration, environment);
                    
                    services.AddControllers()
                        .AddApplicationPart(typeof(LoginController).Assembly);

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

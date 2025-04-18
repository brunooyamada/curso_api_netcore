using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Application.Helpers
{
    public class StartupConfigurationHelper
    {
        public static void ConfigureEnvironment(IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=True;Server=localhost;Port=5432;Database=dbApi_Integration;Uid=postgres;Pwd=masterkey");
                Environment.SetEnvironmentVariable("DATABASE", "Postgres");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "ExampleAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExampleIssuer");
                Environment.SetEnvironmentVariable("Seconds", "28800");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LinuxVmApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();


            ILogger<Program> logger = null;
            IConfiguration config = builder.Build();
            try
            {
                IHost host = CreateHostBuilder(args, config).Build();
                logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("info", config);
                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogError("somehting gone wrong", ex);
                throw;
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration(configure =>
                {
                    // configure.AddAzureAppConfiguration(op =>
                    // {
                    //     op.Connect(new Uri(config["appconfigEndpoint"]),
                    //     new ManagedIdentityCredential(config["appconfigClientId"]));
                    // });
                }).UseStartup<Startup>());
    }
}

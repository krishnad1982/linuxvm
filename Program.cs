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
            ILogger<Program> logger = null;
            try
            {
                IHost host = CreateHostBuilder(args).Build();
                logger = host.Services.GetRequiredService<ILogger<Program>>();
                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogError("somehting gone wrong", ex);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration(configure =>
                {
                    var settings = configure.Build();
                    var endpoint = new Uri("https://mytest2021config.azconfig.io");
                    var clientId = "b4f072bd-4e10-47f0-b827-3b67ed3742f1";
                    configure.AddAzureAppConfiguration(op =>
                    {
                        op.Connect(endpoint,
                        new ManagedIdentityCredential(clientId));
                    });
                }).UseStartup<Startup>());
    }
}

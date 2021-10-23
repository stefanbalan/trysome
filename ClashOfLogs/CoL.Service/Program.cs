using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = BuildConfiguration(); // access as var setting = config["Setting"]; var setting = config["Category:Setting"]; or config.GetSection<T>("section");

            CreateHostBuilder(config, args).Build().Run();
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            
            var env = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true)
                                                    .AddJsonFile($"appsettings.{env}.json", true, true)
                                                    .AddEnvironmentVariables();

            return builder.Build();
        }

        public static IHostBuilder CreateHostBuilder(IConfigurationRoot configuration, string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(lb =>
                {
                    lb.AddConsole();
                    lb.AddConfiguration(configuration);
                });
                services.AddHostedService<Worker>();
            });



    }
}

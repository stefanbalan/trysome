using DuplicateFileFind;
using Microsoft.EntityFrameworkCore;

namespace DFF;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        // Load the appsettings.json file
        var currentDirectory = Directory.GetCurrentDirectory();
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        // Configure DbContext with the connection string from appsettings.json
        builder.Services.AddDbContext<DffContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")
                              ?? Path.Combine(currentDirectory, "_dff.sqlite")));

        builder.Services.AddSingleton<IConfig, Config>(provider => new Config(
            provider.GetRequiredService<IConfiguration>(),
            args));


        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}
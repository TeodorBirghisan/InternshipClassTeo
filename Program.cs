using System;
using System.Linq;
using InternshipClass.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternshipClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            bool recreateDb = args.Contains("--recreateDb");

            InitializeDb(host, recreateDb);

            host.Run();
        }

        private static void InitializeDb(IHost host, bool recreateDb)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider; 
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var context = services.GetRequiredService<InternDbContext>();
                    var webHostEnviroment = services.GetRequiredService<IWebHostEnvironment>();
                    if (webHostEnviroment.IsDevelopment() && recreateDb)
                    {
                        logger.LogDebug("User required to recreate Database.");
                        context.Database.EnsureDeleted();
                        logger.LogWarning("The Database is removed");
                    }

                    SeedData.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

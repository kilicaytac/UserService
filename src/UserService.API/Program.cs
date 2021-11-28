using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserService.API.Infrastructure;
using UserService.Infrastructure.Persistence;

namespace UserService.API
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDbContext<ApplicationDbContext>((context, services) =>
            {
                new ApplicationContextSeed()
                    .SeedAsync(services)
                    .Wait();
            })
           .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder
              .UseContentRoot(Directory.GetCurrentDirectory())

              .UseUrls("http://*:5002")
              .UseStartup<Startup>()
              .UseConfiguration(GetConfiguration());
           });

        private static IConfiguration GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }

    public static class WebExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();

                try
                {
                    InvokeSeeder(seeder, context, services);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
           where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}

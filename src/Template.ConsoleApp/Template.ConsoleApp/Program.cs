using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Template.ConsoleApp
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            try
            {
                Configure();
                Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An exception was thrown while running the application.");
            }
            finally
            {
                Log.Information("Application closing.");
                Log.CloseAndFlush();
            }
        }

        private static void Configure()
        {
            var startup = new Startup();
            Log.Information("Building Service Collection.");
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        public static void Run()
        {
            
        }
    }
}

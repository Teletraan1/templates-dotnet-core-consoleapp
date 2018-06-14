using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.ConsoleApp
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile(@"appSettings.Json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{environment}.Json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            Log.Information("Logging Initialized.");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("Configuring Services.");
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddOptions();
            services.AddAutoMapper();

            Mapper.AssertConfigurationIsValid();

        }
    }
}

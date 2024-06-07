using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Serilog;

namespace candidatehub.Application
{
    public static class DependencyResolution
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            return services;
        }

        public static void ConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            services.AddLogging(logg=> logg.AddSerilog(Log.Logger));
        }
    }
}

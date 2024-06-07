using Microsoft.Extensions.DependencyInjection;
using System;

namespace candidatehub.Application
{
    public static class DependencyResolution
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            {
            return services;
            }
    }
}

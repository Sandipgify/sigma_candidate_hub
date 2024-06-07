using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace candidatehub.Application
{
    public static class DependencyResolution
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyResolution).Assembly;
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}

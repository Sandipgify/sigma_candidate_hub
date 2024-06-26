﻿using candidatehub.Domain.Infrastructure;
using candidatehub.Infrastructure;
using candidatehub.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CandidateHubContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),x => x.MigrationsAssembly("candidatehub.Web")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            return services;
        }

    }
}

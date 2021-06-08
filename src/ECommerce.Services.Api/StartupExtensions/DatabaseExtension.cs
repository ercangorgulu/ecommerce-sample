using ECommerce.Infra.CrossCutting.Identity.Data;
using ECommerce.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ECommerce.Services.Api.StartupExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(connection);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connection);
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddDbContext<EventStoreSqlContext>(options =>
            {
                options.UseSqlite(connection);
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }

        public static IApplicationBuilder UseCustomizedDatabase(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            serviceScope.ServiceProvider.InitDb<AuthDbContext>();
            serviceScope.ServiceProvider.InitDb<ApplicationDbContext>();
            serviceScope.ServiceProvider.InitDb<EventStoreSqlContext>();
            return app;
        }

        private static void InitDb<T>(this IServiceProvider serviceProvider)
            where T : DbContext
        {
            var context = serviceProvider.GetRequiredService<T>();
            context.Database.Migrate();
        }
    }
}

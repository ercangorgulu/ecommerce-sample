using ECommerce.Domain.Services.Hash;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Services.Api.StartupExtensions
{
    public static class HashingExtension
    {
        public static IServiceCollection AddCustomizedHash(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HashingOptions>(configuration.GetSection(HashingOptions.Hashing));
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}

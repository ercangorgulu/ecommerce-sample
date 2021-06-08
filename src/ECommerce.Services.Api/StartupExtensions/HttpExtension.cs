using ECommerce.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System;

namespace ECommerce.Services.Api.StartupExtensions
{
    public static class HttpExtension
    {
        public static IServiceCollection AddCustomizedHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient("Foo", x =>
                {
                    x.BaseAddress = new Uri(configuration.GetValue<string>("HttpClients:Foo"));
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)))
                .AddTypedClient(x => RestService.For<IFooClient>(x));

            return services;
        }
    }
}

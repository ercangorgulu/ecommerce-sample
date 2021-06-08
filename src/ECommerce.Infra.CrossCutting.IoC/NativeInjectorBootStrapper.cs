using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.CommandHandlers;
using ECommerce.Domain.CommandResults.Campaign;
using ECommerce.Domain.Commands.Campaign;
using ECommerce.Domain.Commands.Order;
using ECommerce.Domain.Commands.Product;
using ECommerce.Domain.Core.Bus;
using ECommerce.Domain.Core.Events;
using ECommerce.Domain.Core.Notifications;
using ECommerce.Domain.EventHandlers;
using ECommerce.Domain.Events.Campaign;
using ECommerce.Domain.Events.Order;
using ECommerce.Domain.Events.Product;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Services;
using ECommerce.Infra.CrossCutting.Bus;
using ECommerce.Infra.CrossCutting.Identity.Models;
using ECommerce.Infra.CrossCutting.Identity.Services;
using ECommerce.Infra.Data.EventSourcing;
using ECommerce.Infra.Data.Repository;
using ECommerce.Infra.Data.Repository.EventSourcing;
using ECommerce.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<ICampaignAppService, CampaignAppService>();
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IProductAppService, ProductAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CampaignCreatedEvent>, CampaignEventHandler>();
            services.AddScoped<INotificationHandler<OrderCreatedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<ProductCreatedEvent>, ProductEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<ApplyCampaignCommand, ApplyCampaignResult>, CampaignCommandHandler>();
            services.AddScoped<IRequestHandler<CreateNewCampaignCommand, bool>, CampaignCommandHandler>();
            services.AddScoped<IRequestHandler<CreateNewOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CreateNewProductCommand, bool>, ProductCommandHandler>();

            // Domain - 3rd parties
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMailService, MailService>();

            // Infra - Data
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
        }
    }
}

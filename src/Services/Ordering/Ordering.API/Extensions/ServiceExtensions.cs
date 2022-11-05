using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Services;
using Infrastructure.Configurations;
using Infrastructure.Extensions;
using Infrastructure.Service.Mail;
using Shared.Configurations;
using MassTransit;
using Ordering.API.Application.IntegrationEvents.EventHandler;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using EventBus.Messages.Services.Rabbitmq;
using EventBus.Messages.Interfaces;
using Ordering.API.Application.MassTransit;

namespace Ordering.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            var mailSettings = configuration.GetSection(nameof(EmailSMTPSettings)).Get<EmailSMTPSettings>();
            services.AddSingleton(mailSettings);
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddTransient<ISerializeService,SerializeService>();
            services.AddScoped<RabbitMqService>();
            services.AddScoped<IConsume,BasketCheckoutEventConsumer>();
            return services;
        }
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
            if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
                throw new ArgumentNullException("EventBusSetting is not configured");

            var mqConnection = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                    // cfg.ReceiveEndpoint("basket-checkout-queue", c =>
                    // {
                    //     c.ConfigureConsumer<BasketCheckoutEventHandler>(ctx);
                    // });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Configurations;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace EventBus.Messages.Services.Rabbitmq
{
    public class RabbitMqService
    {
        private IConfiguration _configuration;

        public RabbitMqService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IModel CreateChanel()
        {
            RabbitmqSettings settings = new RabbitmqSettings();
            var section = _configuration.GetSection("RabbitmqSettings");
            section.Bind(settings);
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password
            };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }
    }
}
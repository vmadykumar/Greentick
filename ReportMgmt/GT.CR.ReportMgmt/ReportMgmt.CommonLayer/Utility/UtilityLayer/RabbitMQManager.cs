using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportMgmt.CommonLayer.Utility.IUtilityLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.CommonLayer.Utility.UtilityLayer
{
    public class RabbitMQManager : IRabbitMQManager
    {
        private readonly IOptions<RabbitMQSettings> _settings;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        public RabbitMQManager(IOptions<RabbitMQSettings> rabbitMQSettings, IServiceProvider serviceProvider)
        {
            _settings = rabbitMQSettings;
            _serviceProvider = serviceProvider;
        }

        public void Publish(string message, IDictionary<string, object> arguments = null)
        {
            try
            {
                if (_connection == null) // checking wheteher connection is not null
                    _connection = CreateConnectionFactory().CreateConnection(); // establish a connection to RabbitMQ server
                var channel = _connection.CreateModel();
                channel.ExchangeDeclare(_settings.Value.ExchangeName, _settings.Value.ExchangeType);
                channel.QueueDeclare(queue: _settings.Value.QueueName,
                                 durable: _settings.Value.Durable,
                                 exclusive: _settings.Value.Exclusive,
                                 autoDelete: _settings.Value.AutoDelete,
                                 arguments: arguments);
                channel.QueueBind(_settings.Value.QueueName, _settings.Value.ExchangeName, _settings.Value.RoutingKey);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: _settings.Value.ExchangeName,
                                     routingKey: _settings.Value.RoutingKey,
                                     basicProperties: null,
                                     body: body);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Receive(Func<string, IServiceProvider, object> process, IDictionary<string, object> arguments = null, IServiceProvider serviceProvider = null)
        {
            try
            {
                if (_connection == null)
                    _connection = CreateConnectionFactory().CreateConnection();
                var channel = _connection.CreateModel();
                channel.ExchangeDeclare(_settings.Value.ExchangeName, _settings.Value.ExchangeType);
                channel.QueueDeclare(queue: _settings.Value.QueueName,
                                 durable: _settings.Value.Durable,
                                 exclusive: _settings.Value.Exclusive,
                                 autoDelete: _settings.Value.AutoDelete,
                                 arguments: arguments);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.QueueBind(_settings.Value.QueueName, _settings.Value.ExchangeName, _settings.Value.RoutingKey);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    process(message, _serviceProvider);
                };
                channel.BasicConsume(queue: _settings.Value.QueueName,
                                     autoAck: _settings.Value.AutoAck,
                                     consumer: consumer);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private ConnectionFactory CreateConnectionFactory()
        {
            var factory = new ConnectionFactory() { HostName = _settings.Value.HostName, Port = Convert.ToInt32(_settings.Value.Port), UserName = _settings.Value.UserName, Password = _settings.Value.Password, VirtualHost = _settings.Value.VirtualHost };
            return factory;
        }


    }
}

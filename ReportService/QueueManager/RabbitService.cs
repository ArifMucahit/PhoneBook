using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ReportService.QueueManager
{
    public class RabbitService:IRabbitService
    {
        private IConnection _connection = null;
        private IModel _channel = null;
        private IBasicProperties _queueProperty;

        public RabbitService(IConfiguration config)
        {
            var connection = new ConnectionFactory()
            {
                HostName = config.GetSection("RabbitMQ:Host").Value,
                UserName = config.GetSection("RabbitMQ:Username").Value,
                Password = config.GetSection("RabbitMQ:Password").Value,
                AutomaticRecoveryEnabled = true,
                Port = 5672,
                VirtualHost  = "/"
            };
            _connection = connection.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare("report", true, false, false, null);

            _queueProperty = _channel.CreateBasicProperties();
            _queueProperty.Persistent = true;
        }

        public void PushQueue(string uuid)
        {
            var stringModel = JsonConvert.SerializeObject(uuid);
            var byteModel = Encoding.UTF8.GetBytes(stringModel);
            _channel.BasicPublish(string.Empty, "report", false, _queueProperty, byteModel);
        }

    }
}

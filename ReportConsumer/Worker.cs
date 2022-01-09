using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportConsumer.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnection _connection = null;
        private IModel _channel = null;
        private IBasicProperties _queueProperty;
        private ReportContext _reportContext;
        private DbSet<ReportRequest> _table;

        public Worker(ILogger<Worker> logger,IConfiguration config,ReportContext reportContext)
        {
            _logger = logger;
            _reportContext = reportContext;
            _table = reportContext.Set<ReportRequest>();

            var connection = new ConnectionFactory()
            {
                HostName = config.GetSection("RabbitMQ:Host").Value,
                UserName = config.GetSection("RabbitMQ:Username").Value,
                Password = config.GetSection("RabbitMQ:Password").Value,
                AutomaticRecoveryEnabled = true,
                Port = 5672,
                VirtualHost = "/"
            };
            _connection = connection.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare("report", true, false, false, null);

            _queueProperty = _channel.CreateBasicProperties();
            _queueProperty.Persistent = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _channel.BasicQos(0, 1, false);
                var consumerSave = new EventingBasicConsumer(_channel);
                _channel.BasicConsume("report", false, "", consumerSave);

                consumerSave.Received += ConsumerSave_Received;
            }
        }

        private async void ConsumerSave_Received(object sender, BasicDeliverEventArgs e)
        {
            
        }


        private async void UpdateReportRequest(string uuid,string URL)
        {
            var request = await _table.Where(x => x.UUID == uuid).FirstOrDefaultAsync();
            request.ReportState = "Tamamlandı";
            request.ReportFileURL = URL;
            _table.Update(request);
            _reportContext.SaveChangesAsync();
        }
        private  void ReadReportData()
        {

        }
        
    }
}

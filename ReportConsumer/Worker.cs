using Aspose.Cells;
using Aspose.Cells.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportConsumer.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public Worker(ILogger<Worker> logger,IConfiguration config,ReportContext reportContext)
        {
            _logger = logger;
            _reportContext = reportContext;
            _table = reportContext.Set<ReportRequest>();
            _httpClient = new HttpClient();
            _configuration = config;

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
            var uuid = Encoding.UTF8.GetString(e.Body.ToArray()).Replace("\"","");

            var path = await ReadReportData(uuid);
            if (path == null)
                _channel.BasicNack(e.DeliveryTag,false,false);

            UpdateReportRequest(uuid, path);
            _channel.BasicAck(e.DeliveryTag, false);
        }


        private async void UpdateReportRequest(string uuid,string URL)
        {
            var request = await _table.Where(x => x.UUID == uuid).FirstOrDefaultAsync();
            request.ReportState = "Tamamlandý";
            request.ReportFileURL = URL;
            _table.Update(request);
            _reportContext.SaveChangesAsync();
        }
        private  async Task<string> ReadReportData(string uuid)
        {
            var data = await _httpClient.GetStringAsync(_configuration.GetSection("ThirdParty:ContactService").Value + "api/Contact/GetLocationReport");
            if (data == null)
                return null;
            var path = uuid + ".xlsx";
            var workBook = new Workbook();
            var workSheet = workBook.Worksheets[0];
            var jsonOpt = new JsonLayoutOptions();

            jsonOpt.ArrayAsTable = true;

            JsonUtility.ImportData(data, workSheet.Cells, 0, 0, jsonOpt);

            workBook.Save(path);
            return path;
        }
        
    }
}

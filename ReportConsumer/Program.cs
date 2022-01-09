using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportConsumer.Data;

namespace ReportConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    IConfiguration configuration = hostContext.Configuration;
                    services.AddHostedService<Worker>();
                    services.AddDbContext<ReportContext>(options => { options.UseNpgsql(configuration.GetConnectionString("Postgre")); },ServiceLifetime.Singleton);
                });
    }
}

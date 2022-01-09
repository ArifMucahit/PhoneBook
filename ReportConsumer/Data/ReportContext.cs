using Microsoft.EntityFrameworkCore;

namespace ReportConsumer.Data
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {

        }

        public DbSet<ReportRequest> ReportRequests { get; set; }
    }
}

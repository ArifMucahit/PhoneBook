using ReportService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Data
{
    public interface IRepo
    {
        Task<string> GetReport();
        Task<List<ReportRequest>> ListRequest();
        Task<ReportRequest> GetRequestDetail(string UUID);
    }
}

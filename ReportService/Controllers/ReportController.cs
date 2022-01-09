using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Data;
using ReportService.QueueManager;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        IRepo _repo;
        IRabbitService _queue;
        public ReportController(IRepo repo, IRabbitService rabbitService)
        {
            _repo = repo;
            _queue = rabbitService;
        }

        [HttpGet]
        public async Task<ActionResult> GetReport()
        {
            var reportUuid = await _repo.GetReport();
            if (reportUuid == null)
                return Problem();

            _queue.PushQueue(reportUuid);
            return Ok(reportUuid);

        }

        [HttpGet]
        public async Task<ActionResult> ListReport()
        {
            var list = await _repo.ListRequest();
            if (list.Count > 0)
                return Ok(list);

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetReportDetail(string uid)
        {
            var detail = await _repo.GetRequestDetail(uid);
            if (detail != null)
                return Ok(detail);

            return NotFound();
        }

    }
}

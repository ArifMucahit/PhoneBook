using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Data;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        IRepo _repo;

        public ReportController(IRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetReport()
        {
            return Ok( await _repo.GetReport());
        }

        [HttpGet]
        public async Task<ActionResult> ListReport()
        {
            var list =  await _repo.ListRequest();
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

using Microsoft.EntityFrameworkCore;
using ReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Data
{
    public class Repo:IRepo
    {

       ReportContext _context;

        public Repo(ReportContext reportContext)
        {
            _context = reportContext; 
        }

        public async Task<string> GetReport()
        {
            var reportRequest = new ReportRequest
            {
                ReportFileURL = null,
                ReportState = "Hazırlanıyor",
                RequestDate = DateTime.Now,
                UUID = Guid.NewGuid().ToString()
            };

             _context.Set<ReportRequest>().Add(reportRequest);

            int result = await _context.SaveChangesAsync();
            if (result > 0)
                return reportRequest.UUID;

            throw new Exception("Couldn't create report request!");
        }

        public async Task<List<ReportRequest>> ListRequest()
        {
            return  await _context.Set<ReportRequest>().ToListAsync();
        }

        public async Task<ReportRequest> GetRequestDetail(string UUID)
        {
            return await _context.Set<ReportRequest>().Where(x => x.UUID == UUID).FirstOrDefaultAsync();
            
        }
    }
}

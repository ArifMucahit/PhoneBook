using System;

namespace ReportConsumer.Data
{
    public class ReportRequest
    {
        public string UUID { get; set; }
        public DateTime RequestDate { get; set; }
        public string ReportState { get; set; }
        public string ReportFileURL { get; set; }
    }
}

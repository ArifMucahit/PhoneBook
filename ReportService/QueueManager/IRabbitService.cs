namespace ReportService.QueueManager
{
    public interface IRabbitService
    {
        void PushQueue(string uuid);
    }
}

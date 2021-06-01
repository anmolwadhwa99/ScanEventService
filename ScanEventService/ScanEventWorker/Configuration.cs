using System.Configuration;

namespace ScanEventWorker
{
    public class Configuration
    {
        public static string ServiceUrl = ConfigurationManager.AppSettings["ParcelEventServiceUrl"];
        public static string ScanEventSize = ConfigurationManager.AppSettings["ScanEventSize"];
    }
}

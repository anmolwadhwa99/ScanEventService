using System;

namespace ScanEventWorker.Logging.Interfaces
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogException(Exception innerException);
    }
}

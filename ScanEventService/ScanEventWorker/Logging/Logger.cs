using System;
using ScanEventWorker.Logging.Interfaces;

namespace ScanEventWorker.Logging
{
    public class Logger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
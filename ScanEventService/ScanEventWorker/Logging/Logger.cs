using System;
using System.IO;
using System.Reflection;
using ScanEventWorker.Logging.Interfaces;

namespace ScanEventWorker.Logging
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public void LogMessage(string message)
        {
            using (var w = File.AppendText(_logFilePath + "\\log.log"))
            {
                w.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}: ");
                w.WriteLine(message);
            }
        }

        public void LogException(Exception innerException)
        {
            using (var w = File.AppendText(_logFilePath + "\\log.log"))
            {
                w.Write($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}: ");
                w.WriteLine($"Exception Message : {innerException.Message} \n StackTrace: {innerException.StackTrace}");
            }
        }
    }
}

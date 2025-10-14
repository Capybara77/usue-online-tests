using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace usue_online_tests.Services
{
    public static class Logger
    {
        private static readonly string logFilePath;
        private static readonly object lockObject = new object();

        static Logger()
        {
            string logDir = "Logs";
            logFilePath = Path.Combine(logDir, "error_log.txt");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        public static void LogError(string message, Exception ex = null)
        {
            try
            {
                lock (lockObject)
                {
                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}");
                        if (ex != null)
                        {
                            writer.WriteLine($"Exception: {ex.GetType().Name}");
                            writer.WriteLine($"Message: {ex.Message}");
                            writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                            writer.WriteLine("---");
                        }
                    }
                }
            }
            catch
            {
                
            }
        }


        public static void LogInfo(string message)
        {
            lock (lockObject)
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO: {message}");
                }
            }
        }

    }
}

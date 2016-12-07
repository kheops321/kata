using System;
using System.IO;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Log
{
    public class ApiLogger : IApiLogger
    {

        public static IApiLogger Current = new ApiLogger();

        private string _fileName;
        private bool _enabled;

        public void Enable()
        {
            var logDirectory = System.Configuration.ConfigurationManager.AppSettings["LogDirectory"];

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            _fileName = Path.Combine(
                logDirectory,
                string.Concat(DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss"), ".txt"));

            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        public string Info(string message)
        {
            return Log("VERBOSE", message);
        }
        
        public string Critical(Exception exception)
        {
            return Log("CRITICAL", exception.ToString());
        }

        private string Log(string level, string message)
        {
            var token = Guid.NewGuid().ToString();
            var lineToAdd = $"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} - {level} - {token} : {message}";

            if (_enabled)
            {
                File.AppendAllLines(_fileName, new[] {lineToAdd});
            }

            return token;

        }
    }
}
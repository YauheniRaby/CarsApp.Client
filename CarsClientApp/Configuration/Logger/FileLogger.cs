using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace CarsClientApp.Configuration
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                await File.AppendAllTextAsync(filePath, formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Configuration;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;

namespace Blopnote
{
    public static class BlopnoteLogger
    {
        private static ILoggerFactory Factory;
        static BlopnoteLogger()
        {
            //new Serilog.Sinks.File.FileSink(Path.Combine(Environment.CurrentDirectory, $"logs/{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt"), );

            string logPath = $"logs/{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt";
            var fileLogger = new LoggerConfiguration()
                .WriteTo.File(logPath, outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u4}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Factory = LoggerFactory.Create(builder => builder
                .AddConsole()
                .AddSerilog(fileLogger, dispose: true)
            );
        }
        public static ILogger<T> CreateLogger<T>()
            where T: class
        {
            // I don't think any of below is possible:
            //    - class A will need in logger of class B.
            //    - class A will create Logger<A> more than one time.
            // Consequently this way must work perfectly. (another way - use dictionary with loggers)
            return Factory.CreateLogger<T>();
        }
    }
}

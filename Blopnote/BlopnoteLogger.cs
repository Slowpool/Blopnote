using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Blopnote
{
    public static class BlopnoteLogger
    {
#warning add file logger in addition
        private static ILoggerFactory Factory;
        static BlopnoteLogger()
        {
            Factory = LoggerFactory.Create(builder => builder.AddConsole());
            // ok, i tried. It's too hard to figure out how does it work.
                //.AddConsoleFormatter<ConsoleFormatter, ConsoleFormatterOptions>(options =>
                //    {
                //        options.TimestampFormat = "[HH:mm:ss]";
                //        //options.ColorBehavior = LoggerColorBehavior.Disabled;
                //    }));
        }
        public static ILogger<T> CreateLogger<T>()
            where T: class
        {
            // I don't think that any of the next are possible:
            //    - class A will need in logger of class B.
            //    - class A will create Logger<A> more than one time.
            // Consequently this way must work perfectly. (another way - use dictionary with loggers)
            return Factory.CreateLogger<T>();
        }
    }
}

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
        private static ILoggerFactory Factory = LoggerFactory.Create(builder => builder.AddConsole());
        public static ILogger<T> CreateLogger<T>()
            where T: class
        {
            // I don't think that it's possible that e.g. class A will need in logger of class B.
            // Also I don't think class A will create Logger<A> more than one time.
            // Consequently this way must work perfectly.
            return Factory.CreateLogger<T>();
        }
    }
}

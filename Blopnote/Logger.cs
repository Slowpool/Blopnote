using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    public class Logger
    {
        private static Logger instance;
        public static Logger Instance => instance != null ? instance : instance = new Logger();


        private StreamWriter writer;
        private Logger()
        {
            string path = Path.Combine(Environment.CurrentDirectory, $"logs {Format(DateTime.UtcNow)}.txt");
            writer = new StreamWriter(path);
        }

        ~Logger()
        {
            writer.Dispose();
        }

        private string Format(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy HH-mm-ss");
        }

        public void Log(string info)
        {
            writer.WriteLine($"{Format(DateTime.UtcNow)} {info}");
        }

        public void Log(LogType type, string info)
        {
            writer.WriteLine($"{Format(DateTime.UtcNow)} {type.NewToString()} {info}");
        }
    }
}

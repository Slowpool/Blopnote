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
        public static Logger Instance;

        private StreamWriter writer;
        private Logger()
        {
#warning LOGGER
            //writer = new StreamWriter();
        }
    }
}

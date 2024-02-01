using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Blopnote
{
    internal static class Extensions
    {
        public static void Reset(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }
    }
}

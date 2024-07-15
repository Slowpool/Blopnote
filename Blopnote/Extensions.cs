using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace Blopnote
{
    public static class Extensions
    {
        public static void Reset(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        public static void NameToLower(this DirectoryInfo directory)
        {
            string nameInLowerCase = directory.Name.ToLower();
            directory.MoveTo(Path.Combine(directory.Parent.FullName, "temp"));
            directory.MoveTo(Path.Combine(directory.Parent.FullName, nameInLowerCase));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Blopnote
{
    public class Condition
    {
        private string FileName { get; set; }
        public bool FileHasName => FileName != null;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public static class CursorChanger
    {
        public static void SetWaiting()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void SetDefault()
        {
            Cursor.Current = Cursors.Default;
        }
    }
}

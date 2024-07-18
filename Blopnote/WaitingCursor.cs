using System;

namespace Blopnote
{
    /// <summary>
    /// I created this class for fun.
    /// </summary>
    public class WaitingCursor : IDisposable
    {
        public WaitingCursor()
        {
            CursorChanger.SetWaiting();
        }

        public void Dispose()
        {
            CursorChanger.SetDefault();
        }
    }
}

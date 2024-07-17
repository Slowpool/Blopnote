using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    public enum BlopnoteMessageTypes
    {
        UnknownError,
        FileCreatingError,
        FileOpeningError,
        FileSavingError,
        UrlOpeningError,
        TranslationCompleted,
        BrowserError,
    }
}

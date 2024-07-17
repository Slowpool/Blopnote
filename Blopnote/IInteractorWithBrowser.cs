using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    public interface IInteractorWithBrowser
    {
        // probably it mustn't be public. Maybe here it'd better to use inheritance?
        // UPD: Still DRY violation, becuase the code of interacting still the same everywhere.
#warning unattractive browser interactor
        /// <summary>
        /// Wrap all interactions with browser in this method.
        /// </summary>
        /// <param name="actionWithBrowser"></param>
        void TryInteractWithBrowserOtherwiseShowError(Action actionWithBrowser);
    }
}

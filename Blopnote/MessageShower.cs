using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public static class MessageShower
    {
        private const string BROWSER_ERROR_CAPTION = "Browser error";

        public static void Show(Exception exception)
        {
            string text;
            switch (exception.GetType().Name)
            {
                case "WebDriverTimeoutException":
                    text = "Waiting time has expired\r\n" +
                        "You can try reconnecting browser in the upper menu.";
                    break;

                case "FailedBrowserOpeningException":
                default:
                    text = exception.Message;
                    break;
            }
            MessageBox.Show(caption: BROWSER_ERROR_CAPTION,
                            text: text,
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error);
        }
    }
}

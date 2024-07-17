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
        private const string FILE_ERROR_CAPTION = "File error";

        public static void Show(BlopnoteMessageTypes messageType, Exception exception, string extraInfo = null)
        {
            string caption;
            string text;
            MessageBoxIcon icon = MessageBoxIcon.Error;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            switch (messageType)
            {
                case BlopnoteMessageTypes.FileCreatingError:
                    caption = FILE_ERROR_CAPTION;
                    text = "Failed to create file";
                    break;

                case BlopnoteMessageTypes.FileSavingError:
                    caption = FILE_ERROR_CAPTION;
                    text = "Failed to save file";
                    break;

                case BlopnoteMessageTypes.FileOpeningError:
                    caption = FILE_ERROR_CAPTION;
                    text = "Failed to open file";
                    break;

                case BlopnoteMessageTypes.UrlOpeningError:
                    caption = "Url opening error";
                    text = "Url wasn't opened correctly.";
                    break;

                case BlopnoteMessageTypes.TranslationCompleted:
                    caption = "Completed";
                    text = "Congratulations! Song was successfully written!";
                    icon = MessageBoxIcon.Information;
                    break;

                case BlopnoteMessageTypes.BrowserError:
                    caption = "Browser error";
                    text = "Failed to parse data in browser. Try reconnecting to browser in the upper menu. If problem isn't solved, you can try change used browser (if I've added this feature) or leave your comments to developer.";
                    break;

                case BlopnoteMessageTypes.UnknownError:
                default:
                    caption = "Unknown error";
                    text = "Unexpected error occured.";
                    break;

            }

            text += Environment.NewLine;
            if (extraInfo != null)
            {
                text += extraInfo + Environment.NewLine;
            }
            text += "Cause: " + exception.Message;

            MessageBox.Show(caption: caption,
                            text: text,
                            buttons: buttons,
                            icon: icon);           
        }
    }
}

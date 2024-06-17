using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    internal class URL_item
    {
        private readonly Button CopyButton;
        private readonly RadioButton RadioButton;
        private readonly LinkLabel Label;

        private bool visible;
        internal bool Visible
        {
            get => visible;
            set
            {
                CopyButton.Visible
                    = RadioButton.Visible
                    = Label.Visible
                    = value;
            }
        }

        internal string URL { get; set; }

        internal string Name
        {
            get => Label.Text;
            set
            {
                Label.Text = value;
            }
        }

        internal bool Checked
        {
            get => RadioButton.Checked;
            set
            {
                RadioButton.Checked = value;
            }
        }

        internal URL_item(Button CopyButton, RadioButton RadioButton, LinkLabel Label)
        {
            this.CopyButton = CopyButton;
            this.RadioButton = RadioButton;
            this.Label = Label;
        }

        internal bool HasButton(Button button)
        {
            return CopyButton == button;
        }

        internal bool HasLabel(LinkLabel linkLabel)
        {
            return Label == linkLabel;
        }
    }
}

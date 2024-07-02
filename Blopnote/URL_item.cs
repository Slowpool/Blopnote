using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public class Urlitem
    {
        private readonly Button CopyButton;
        private readonly RadioButton RadioButton;
        private readonly LinkLabel Label;

        private bool visible;
        public bool Visible
        {
            get => visible;
            set
            {
                visible
                    = CopyButton.Visible
                    = RadioButton.Visible
                    = Label.Visible
                    = value;
            }
        }

        public string Url { get; set; }

        public string Name
        {
            get => Label.Text;
            set
            {
                Label.Text = value;
            }
        }

        public bool Checked
        {
            get => RadioButton.Checked;
            set
            {
                RadioButton.Checked = value;
            }
        }

        public Urlitem(Button CopyButton, RadioButton RadioButton, LinkLabel Label)
        {
            this.CopyButton = CopyButton;
            this.RadioButton = RadioButton;
            this.Label = Label;
        }

        public bool HasButton(Button button)
        {
            return CopyButton == button;
        }

        public bool HasLabel(LinkLabel linkLabel)
        {
            return Label == linkLabel;
        }
    }
}

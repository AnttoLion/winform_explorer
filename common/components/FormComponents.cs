using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common.components
{
    public class FInputBox
    {
        private TextBox textBox;
        private Label label;

        public FInputBox(string labeltext)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(150, 31);

            textBox = new TextBox();
            textBox.Location = new Point(50, 50);
            textBox.Size = new Size(300, 20);
            textBox.Font = new Font("Segoe UI Semibold", 15.75F);
        }

        public TextBox GetTextBox()
        {
            return textBox;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetPosition(Point location)
        {
            label.Location = location;
            textBox.Location = new Point(location.X + 200, location.Y);
        }
    }

    public class FCheckBox
    {
        public FCheckBox() { }
    }
}

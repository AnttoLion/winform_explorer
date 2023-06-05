using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common.components
{
    public class ModalButton
    {
        private Button button;
        private Label label;
        protected Keys hotKey;

        public ModalButton(string text, Keys hotKey)
        {
            this.hotKey = hotKey;

            button = new Button();
            button.Text = text;

            button.AutoSize = true;
            button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            button.BackColor = Color.FromArgb(223, 223, 223);
            button.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold);
            button.ForeColor = System.Drawing.Color.DimGray;
            button.Margin = new System.Windows.Forms.Padding(0);
            button.Size = new System.Drawing.Size(53, 32);
            button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.hotKey = hotKey;
        }

        public Button GetButton()
        {
            return button;
        }

        public Keys GetKeys()
        {
            return hotKey;
        }

        public void SetPosition(Point location)
        {
            button.Location = location;
            label.Location = new Point(location.X + button.Width + 8, location.Y);
        }
    }
}

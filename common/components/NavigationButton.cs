using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common.components
{
    public class NavigationButton
    {
        private Button button;
        private GlobalLayout targetForm;

        public NavigationButton(string text, GlobalLayout targetForm)
        {
            this.targetForm = targetForm;

            this.button = new Button();
            this.button.Text = text;

            this.button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(46)))), ((int)(((byte)(78)))));
            this.button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(98)))), ((int)(((byte)(181)))));
            this.button.FlatAppearance.BorderSize = 3;
            this.button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button.Font = new System.Drawing.Font("Segoe UI Semibold", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button.Location = new System.Drawing.Point(156, 263);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(600, 110);
            this.button.TabIndex = 3;
            this.button.UseVisualStyleBackColor = false;
            this.button.MouseEnter += (s, e) => button_Enter(s, e);
            this.button.Enter += (s, e) => button_Enter(s, e);
            this.button.Leave += (s, e) => button_Leave(s, e);
        }

        public Button GetButton()
        {
            return button;
        }

        public GlobalLayout GetTargetForm()
        {
            return targetForm;
        }

        public void SetPosition(Point location)
        {
            button.Location = location;
        }

        private void button_Enter(object sender, EventArgs e)
        {
            button.FlatAppearance.BorderColor = Color.FromArgb(132, 187, 243);
            button.FlatAppearance.BorderSize = 5;
            button.Focus();
        }
        private void button_Leave(object sender, EventArgs e)
        {
            button.FlatAppearance.BorderColor = Color.FromArgb(17, 98, 181);
            button.FlatAppearance.BorderSize = 3;
        }
    }
}

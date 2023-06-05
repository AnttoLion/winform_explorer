using mjc_dev.common.components;
using mjc_dev.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common
{

    public partial class GlobalLayout : BasicLayout
    {
        public Form _prevForm { get; set; }

        private Label _comingSoonTxt;
        private NavigationHistory navigationHistory = new NavigationHistory();

        public GlobalLayout()
        {
            InitializeComponent();
            _initBasicSize();
        }

        public GlobalLayout(string title, string formDescription) : base(title, formDescription)
        {
            InitializeComponent();
            _initBasicSize();
        }

        protected void _initializeHKButtons(HotkeyButton[] hkButtons)
        {
            int startX = 20;
            int startY = 65;
            int spacingX = 280;
            int spacingY = 42;

            int x = startX;
            int y = startY;
            if (hkButtons != null)
            {
                for (int i = 0; i < hkButtons.Length; i++)
                {
                    this._footer.Controls.Add(hkButtons[i].GetButton());
                    this._footer.Controls.Add(hkButtons[i].GetLabel());

                    hkButtons[i].GetButton().TabStop = false;
                    hkButtons[i].GetLabel().TabStop = false;

                    hkButtons[i].GetButton().BringToFront();
                    hkButtons[i].GetLabel().BringToFront();

                    hkButtons[i].SetPosition(new Point(x, y));

                    y += spacingY; // increment x for the next button

                    if ((i + 1) % 3 == 0)
                    {
                        y = startY; // reset x to the start of the line
                        x += spacingX; // increment y for the next line
                    }
                }
            }

            this.KeyDown += (s, e) => GlobalLayout_KeyDown(s, e, hkButtons);
        }

        protected void _addComingSoon()
        {
            this._comingSoonTxt = new System.Windows.Forms.Label();
            this._comingSoonTxt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._comingSoonTxt.Width = 500;
            this._comingSoonTxt.Height = 50;
            this._comingSoonTxt.BackColor = System.Drawing.Color.Transparent;
            this._comingSoonTxt.Font = _fontPoint2;
            this._comingSoonTxt.ForeColor = this._textMainColor;
            this._comingSoonTxt.Text = "Coming Soon..";
            this._comingSoonTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._comingSoonTxt.Location = new System.Drawing.Point((this.Width - this._comingSoonTxt.Width) / 2, 300);
            this.Controls.Add(this._comingSoonTxt);
        }

        protected void _initiallizeNavButtons(NavigationButton[] navButtons)
        {
            int startX = 30;
            int startY = 120;
            int spacingX = 700;
            int spacingY = 140;

            int x = startX;
            int y = startY;
            if (navButtons != null)
            {
                for (int i = 0; i < navButtons.Length; i++)
                {
                    navButtons[i].GetButton().TabIndex = i;
                    this.Controls.Add(navButtons[i].GetButton());

                    navButtons[i].SetPosition(new Point(x, y));

                    y += spacingY; // increment x for the next button

                    if ((i + 1) % 5 == 0)
                    {
                        y = startY; // reset x to the start of the line
                        x += spacingX; // increment y for the next line
                    }

                    var targetForm = navButtons[i].GetTargetForm();
                    navButtons[i].GetButton().Click += (s, args) => _navigateToForm(s, args, targetForm);
                }
            }
        }

        protected void _hotkeyTest(object sender, EventArgs e)
        {
            MessageBox.Show($"Comming Soon.. Hotkey '{sender.ToString()}' clicked!");
            this.ActiveControl = this._footer;
        }

        protected void _closeProgram(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close this program?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        protected void _navigateToForm(object sender, EventArgs e, GlobalLayout targetForm)
        {
            targetForm._prevForm = this;

            targetForm.Show();
            this.Hide();
            targetForm.FormClosed += (ss, sargs) => this.Close();
        }

        protected void _navigateToPrev(object sender, EventArgs e)
        {
            this._prevForm.Show();
            this.Hide();
        }

        private void GlobalLayout_KeyDown(object sender, KeyEventArgs e, HotkeyButton[] hkButtons)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if(this._prevForm != null)
                {
                    _navigateToPrev(sender, e);
                }
                else
                {
                    _closeProgram(sender, e);
                }
                return;
            }
            foreach (HotkeyButton button in hkButtons)
            {
                if (e.KeyCode == button.GetKeys() && e.KeyCode != Keys.Escape)
                {
                    button.GetButton().PerformClick();
                }
            }
        }

        
    }
}

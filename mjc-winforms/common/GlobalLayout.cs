using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_winforms.common.components;

namespace mjc_winforms
{
    public partial class GlobalLayout : BasicLayout
    {
        protected HotkeyButton hkEnterTest = new HotkeyButton("Enter", "Hotkey Test", Keys.Enter);
        protected HotkeyButton hkInsTest = new HotkeyButton("Ins", "Hotkey Test", Keys.Insert);

        protected HotkeyButton hkCloseProgramButton = new HotkeyButton("Esc", "Close Program", Keys.Escape);
        protected HotkeyButton hkOpenSelection = new HotkeyButton("Enter", "Open Selection", Keys.Enter);
        protected HotkeyButton hkAdd = new HotkeyButton("Ins", "Add", Keys.Insert);
        protected HotkeyButton hkSubmit = new HotkeyButton("Enter", "Submit", Keys.Enter);
        protected HotkeyButton hkMoveFields = new HotkeyButton("Tab", "Move between fields", Keys.Tab);
        protected HotkeyButton hkPrevScreen = new HotkeyButton("Esc", "Close Program", Keys.Escape);
        protected HotkeyButton[] hkButtons;

        public GlobalLayout()
        {
            InitializeComponent();
            initHotkeyEvents();
        }
        public GlobalLayout(string title, string formDescription) : base(title, formDescription)
        {
            InitializeComponent();
            initHotkeyEvents();
        }
        protected void initializeButtons(HotkeyButton[] hkButtons)
        {
            this.hkButtons = hkButtons;

            int startX = 20;
            int startY = this.Height - 180;
            int spacingX = 300;
            int spacingY = 40;

            int x = startX;
            int y = startY;
            if(hkButtons != null)
            {
                for (int i = 0; i < hkButtons.Length; i++)
                {
                    Controls.Add(hkButtons[i].GetButton());
                    Controls.Add(hkButtons[i].GetLabel());

                    hkButtons[i].GetButton().BringToFront();
                    hkButtons[i].GetLabel().BringToFront();

                    hkButtons[i].SetPosition(new Point(x, y));

                    y += spacingY; // increment x for the next button

                    if ((i + 1) % 3 == 0) // check if we've added 3 buttons and need to wrap to the next line
                    {
                        y = startY; // reset x to the start of the line
                        x += spacingX; // increment y for the next line
                    }
                }
            }
        }

        private void initHotkeyEvents()
        {
            hkEnterTest.GetButton().Click += new EventHandler(this.HotkeyTest);
            hkInsTest.GetButton().Click += new EventHandler(this.HotkeyTest);

            hkCloseProgramButton.GetButton().Click += new EventHandler(this.closeCurrForm);
            hkOpenSelection.GetButton().Click += new EventHandler(this.openSelection);
            hkAdd.GetButton().Click += new EventHandler(this.addItem);
        }
        private void HotkeyTest(object sender, EventArgs e)
        {
            MessageBox.Show($"Hotkey '{sender.ToString()}' clicked!");
            //MessageBox.Show("Enter clicked!");
        }

        private void closeCurrForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openSelection(object sender, EventArgs e)
        {
        }

        private void addItem(object sender, EventArgs e)
        {
        }

        private void hkey_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You press me");
        }

        private void GlobalLayout_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (HotkeyButton button in hkButtons)
            {
                if (e.KeyCode == button.GetKeys())
                {
                    button.GetButton().PerformClick();
                }
            }
        }

        private void GlobalLayout_Resize(object sender, EventArgs e)
        {
            this.initializeButtons(this.hkButtons);
        }

        private void GlobalLayout_Load(object sender, EventArgs e)
        {
            //this.initializeButtons(this.hkButtons);
        }
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace mjc_dev.common
{

    public partial class GlobalLayout : BasicLayout
    {
        public Form _prevForm { get; set; }

        protected int _accountId = 1;

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

        protected void _initializeHKButtons(HotkeyButton[] hkButtons, bool DefaultEscEvent = true, HotkeyButton[] onEventhkButtons = null)
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

            if (onEventhkButtons == null) onEventhkButtons = hkButtons;
            this.KeyDown += (s, e) => GlobalLayout_KeyDown(s, e, onEventhkButtons, DefaultEscEvent);
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

        protected void _addFormInputs(List<dynamic> Inputs, int startX, int startY, int distanceX, int distanceY, int limitY = int.MaxValue, Control.ControlCollection _controls = null)
        {
            if(_controls == null){
                _controls = this.Controls;
            }
            int posX = startX;
            int posY = startY;
            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i] is FInputBox)
                {
                    FInputBox inputBox = (FInputBox)Inputs[i];
                    inputBox.SetPosition(new Point(posX, posY));
                    _controls.Add(inputBox.GetLabel());
                    _controls.Add(inputBox.GetTextBox());
                    inputBox.GetTextBox().KeyDown += (s, e) => {
                        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                        {
                            SelectNextControl((Control)s, true, true, true, true);
                            e.Handled = true;
                        }
                        if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift))
                        {
                            SelectNextControl((Control)s, false, true, true, true);
                            e.Handled = true;
                        }
                    };
                }
                else if (Inputs[i] is FCheckBox)
                {
                    FCheckBox checkBox = (FCheckBox)Inputs[i];
                    checkBox.SetPosition(new Point(posX + 5, posY));
                    _controls.Add(checkBox.GetCheckBox());
                    checkBox.GetCheckBox().KeyDown += (s, e) => {
                        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                        {
                            SelectNextControl((Control)s, true, true, true, true);
                            e.Handled = true;
                        }
                        if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift))
                        {
                            SelectNextControl((Control)s, false, true, true, true);
                            e.Handled = true;
                        }
                    };
                }
                else if (Inputs[i] is FComboBox)
                {
                    FComboBox comboBox = (FComboBox)Inputs[i];
                    comboBox.SetPosition(new Point(posX, posY));
                    _controls.Add(comboBox.GetLabel());
                    _controls.Add(comboBox.GetComboBox());
                    comboBox.GetComboBox().KeyDown += (s, e) => {
                        if (e.KeyCode == Keys.Enter)
                        {
                            SelectNextControl((Control)s, true, true, true, true);
                            e.Handled = true;
                        }
                        if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift)
                        {
                            SelectNextControl((Control)s, false, true, true, true);
                            e.Handled = true;
                        }
                    };
                }
                else if (Inputs[i] is FGroupLabel)
                {
                    FGroupLabel groupLabel = (FGroupLabel)Inputs[i];
                    groupLabel.SetPosition(new Point(posX, posY));
                    _controls.Add(groupLabel.GetLabel());
                }
                else if (Inputs[i] is FlabelConstant)
                {
                    FlabelConstant labelConstant = (FlabelConstant)Inputs[i];
                    labelConstant.SetPosition(new Point(posX, posY));
                    _controls.Add(labelConstant.GetLabel());
                    _controls.Add(labelConstant.GetConstant());
                }
                else if (Inputs[i] is FDateTime)
                {
                    //Console.WriteLine(posY.ToString());
                    FDateTime dateTime = (FDateTime)Inputs[i];
                    dateTime.SetPosition(new Point(posX, posY));
                    _controls.Add(dateTime.GetLabel());
                    _controls.Add(dateTime.GetDateTimePicker());
                    dateTime.GetDateTimePicker().KeyDown += (s, e) => {
                        if (e.KeyCode == Keys.Enter)
                        {
                            SelectNextControl((Control)s, true, true, true, true);
                            e.Handled = true;
                        }
                        if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift)
                        {
                            SelectNextControl((Control)s, false, true, true, true);
                            e.Handled = true;
                        }
                    };
                }
                else
                {
                    List<dynamic> LineComponents = Inputs[i];
                    int prevWidth = 0;
                    for (int j = 0; j < LineComponents.Count; j++)
                    {
                        if (LineComponents[j] is FInputBox)
                        {
                            FInputBox inputBox = (FInputBox)LineComponents[j];
                            inputBox.SetPosition(new Point(posX + prevWidth, posY));
                            prevWidth += inputBox.GetLabel().Width + inputBox.GetTextBox().Width + 40;

                            _controls.Add(inputBox.GetLabel());
                            _controls.Add(inputBox.GetTextBox());
                            inputBox.GetTextBox().KeyDown += (s, e) => {
                                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                                {
                                    SelectNextControl((Control)s, true, true, true, true);
                                    e.Handled = true;
                                }
                                if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift))
                                {
                                    SelectNextControl((Control)s, false, true, true, true);
                                    e.Handled = true;
                                }
                            };

                        }
                        else if (LineComponents[j] is FCheckBox)
                        {
                            FCheckBox checkBox = (FCheckBox)LineComponents[j];
                            checkBox.SetPosition(new Point(posX + 5 + prevWidth, posY));
                            prevWidth += checkBox.GetCheckBox().Width + 40;

                            _controls.Add(checkBox.GetCheckBox());
                            checkBox.GetCheckBox().KeyDown += (s, e) => {
                                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                                {
                                    SelectNextControl((Control)s, true, true, true, true);
                                    e.Handled = true;
                                }
                                if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift))
                                {
                                    SelectNextControl((Control)s, false, true, true, true);
                                    e.Handled = true;
                                }
                            };

                        }
                    }
                }

                posY = posY + distanceY;
                if (posY > limitY)
                {
                    posY = startY;
                    posX += distanceX;
                }
            }
        }


        protected void GlobalLayout_KeyDown(object sender, KeyEventArgs e, HotkeyButton[] hkButtons, bool DefaultEscEvent)
        {
            if (e.KeyCode == Keys.Escape && DefaultEscEvent)
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
                if(button.GetAdditionalKey() != null)
                {
                    switch (button.GetAdditionalKey())
                    {
                        case "alt":
                            if (!e.Alt) return;
                            break;
                        default:
                            return;
                            break;
                    }
                    //MessageBox.Show("alt");
                }
                if (e.KeyCode == button.GetKeys() && e.KeyCode != Keys.Escape)
                {
                    button.GetButton().PerformClick();
                }
            }
        }
    }
}

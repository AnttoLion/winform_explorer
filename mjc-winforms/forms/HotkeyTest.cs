using mjc_winforms.common.components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_winforms.common;

namespace mjc_winforms.forms
{
    public partial class HotkeyTest : GlobalLayout
    {
        public HotkeyTest() : base("Hotkey Test", "Hotkey Test")
        {
            InitializeComponent(); // call the base class constructor first
            HotkeyButton[] hkButtons = new HotkeyButton[3] { hkCloseProgramButton, hkEnterTest, hkInsTest };
            initializeButtons(hkButtons); // call the initializeButtons method from the base class
        }
    }
}

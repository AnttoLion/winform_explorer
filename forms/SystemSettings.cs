using mjc_dev.common.components;
using mjc_dev.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.forms
{
    public partial class SystemSettings : GlobalLayout
    {

        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);

        public SystemSettings() : base("System Settings", "Manage system settings")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[1] { hkPreviousScreen };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                if (button != hkPreviousScreen)
                    button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
            hkPreviousScreen.GetButton().Click += new EventHandler(_navigateToPrev);
        }
    }
}

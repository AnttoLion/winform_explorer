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
    public partial class CustomerProfile : GlobalLayout
    {

        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);
        private HotkeyButton hkCustomerJump = new HotkeyButton("F4", "Customer Jump", Keys.F4);

        public CustomerProfile() : base("Customer Profiler", "Profile view of customers and their history of purchases")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPreviousScreen, hkCustomerJump };
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
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
    public partial class ReceiveInventory : GlobalLayout
    {

        private HotkeyButton hkReceiveInv = new HotkeyButton("F8", "Receive Inv", Keys.F8);
        private HotkeyButton hkCancel = new HotkeyButton("Esc", "Cancel", Keys.Escape);

        public ReceiveInventory() : base("Recieve Inventory", "Full out to receive inventory")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkReceiveInv, hkCancel };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                if (button != hkCancel)
                    button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
            hkCancel.GetButton().Click += new EventHandler(_navigateToPrev);
        }
    }
}

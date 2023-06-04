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
    public partial class Reconcilliation : GlobalLayout
    {

        private HotkeyButton hkClosesReport = new HotkeyButton("Esc", "Closes Report", Keys.Escape);
        private HotkeyButton hkValidateSale = new HotkeyButton("F1", "Validate Sale(line item)", Keys.F1);
        private HotkeyButton hkAddPayment = new HotkeyButton("F3", "Add Payment", Keys.F3);

        public Reconcilliation() : base("Zone Chart", "Manage Zones to be used by the system")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[3] { hkClosesReport, hkValidateSale, hkAddPayment };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
        }
    }
}
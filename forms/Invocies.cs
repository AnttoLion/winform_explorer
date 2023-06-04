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
    public partial class Invocies : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkEdits = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkInvocieDetails = new HotkeyButton("F2", "Invoice Details", Keys.F2);
        private HotkeyButton hkCustomers = new HotkeyButton("F4", "Customers", Keys.F4);
        private HotkeyButton hkRecurringPymt = new HotkeyButton("F6", "Recurring Payment", Keys.F6);
        private HotkeyButton hkInvocies = new HotkeyButton("F7", "Invocies", Keys.F7);
        private HotkeyButton hkOrderEntry = new HotkeyButton("F8", "Order Entry", Keys.F8);
        private HotkeyButton hkPymtHistory = new HotkeyButton("F9", "Pymt History", Keys.F9);

        public Invocies() : base("Invoice Lookup", "Displays invoices of a customer")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[9] { hkAdds, hkDeletes, hkEdits, hkInvocieDetails, hkCustomers, hkRecurringPymt, hkInvocies, hkOrderEntry, hkPymtHistory };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
        }
    }
}

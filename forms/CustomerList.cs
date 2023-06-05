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
    public partial class CustomerList : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkCustSummary = new HotkeyButton("F2", "Cust Summary", Keys.F2);
        private HotkeyButton Invocies = new HotkeyButton("F5", "Invoices", Keys.F5);
        private HotkeyButton RecurringPayment = new HotkeyButton("F6", "Recurring Payment", Keys.F6);
        private HotkeyButton HistoryInv = new HotkeyButton("F7", "Historical Inv", Keys.F7);
        private HotkeyButton OrderEntry = new HotkeyButton("F8", "Order Entry", Keys.F8);
        private HotkeyButton PaymentHistory = new HotkeyButton("F9", "Payment History", Keys.F9);
        private HotkeyButton ReceivePayment = new HotkeyButton("F10", "Receive Payment", Keys.F10);
        private HotkeyButton ArchivedCustomers = new HotkeyButton("F11", "Archived Customers", Keys.F11);

        public CustomerList() : base("Customer List", "Manage customers in the system")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[11] { hkAdds, hkDeletes, hkSelects, hkCustSummary, Invocies, RecurringPayment, HistoryInv, OrderEntry, PaymentHistory, ReceivePayment, ArchivedCustomers };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
        }
    }
}

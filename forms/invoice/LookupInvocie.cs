using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.common.components;
using mjc_dev.common;
using System.Reflection.Emit;
using mjc_dev.model;


namespace mjc_dev.forms.invoice
{
    public partial class LookupInvocie : GlobalLayout
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

        private FInputBox Customer = new FInputBox("Customer#");
        private FInputBox CustomerName = new FInputBox("Name");
        private FInputBox Address = new FInputBox("Address");
        private FInputBox City = new FInputBox("City");
        private FInputBox State = new FInputBox("State");
        private FInputBox Zipcode = new FInputBox("Zip");
        private FInputBox AccountBalance = new FInputBox("Account Balance");
        private FInputBox YTDPurchases = new FInputBox("YTD Purchases");
        private FInputBox YTDInterest = new FInputBox("YTDInterest");

        private GridViewOrigin InvoiceLookupGrid = new GridViewOrigin();
        private DataGridView ILGridRefer;
        //private DashboardModel model = new DashboardModel();


        public LookupInvocie() : base("Invoice Lookup", "Displays invoices of a customer")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[9] { hkAdds, hkDeletes, hkEdits, hkInvocieDetails, hkCustomers, hkRecurringPymt, hkInvocies, hkOrderEntry, hkPymtHistory };
            _initializeHKButtons(hkButtons);

            InitInputBox();
            InitCustomerList();
        }

        private void InitInputBox()
        {
            List<dynamic> FormComponents = new List<dynamic>();

            FormComponents.Add(Customer);
            FormComponents.Add(CustomerName);
            FormComponents.Add(Address);

            List<dynamic> LineComponents = new List<dynamic>();

            City.GetLabel().Width = 60;
            City.GetTextBox().Width = 160;
            LineComponents.Add(City);

            State.GetLabel().Width = 60;
            State.GetTextBox().Width = 100;
            LineComponents.Add(State);

            Zipcode.GetLabel().Width = 60;
            Zipcode.GetTextBox().Width = 100;
            LineComponents.Add(Zipcode);

            FormComponents.Add(LineComponents);
            FormComponents.Add(AccountBalance);
            FormComponents.Add(YTDPurchases);
            FormComponents.Add(YTDInterest);

            _addFormInputs(FormComponents, 30, 110, 800, 42, 250);
        }

        private void InitCustomerList()
        {
            ILGridRefer = InvoiceLookupGrid.GetGrid();
            ILGridRefer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(157, 196, 235);
            ILGridRefer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 63, 96);
            ILGridRefer.ColumnHeadersDefaultCellStyle.Padding = new Padding(12);
            ILGridRefer.Location = new Point(0, 300);
            ILGridRefer.Width = this.Width;
            ILGridRefer.Height = this.Height - 295;
            this.Controls.Add(ILGridRefer);
            this.ILGridRefer.CellDoubleClick += (sender, e) =>
            {
                //updateCustomer();
            };

            //LoadCustomerList();
        }
    }
}

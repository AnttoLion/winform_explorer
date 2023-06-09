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

using mjc_dev.forms.customer;
using mjc_dev.forms.price;
using mjc_dev.forms.invoice;


namespace mjc_dev.forms
{
    public partial class ReceivableDashboard : GlobalLayout
    {
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);
        private HotkeyButton hkOpenSelection = new HotkeyButton("Enter", "Open Selection", Keys.Enter);

        private NavigationButton CustomerList = new NavigationButton("Customer List", new CustomerList());
        private NavigationButton ReceivePayment = new NavigationButton("Receive Payment", new ReceivePayment());
        private NavigationButton Invoices = new NavigationButton("Invoices", new LookupInvocie());
        private NavigationButton PricerTiers = new NavigationButton("Price Tiers", new PriceTiers());
        private NavigationButton SalesCostCodes = new NavigationButton("Sales/Cost Codes", new SalesCostCodes());
        private NavigationButton SalesTaxCode = new NavigationButton("Sales Tax Code", new SalesTaxCodes());
        private NavigationButton CreditCodes = new NavigationButton("Credit Codes", new CreditCodes());
        private NavigationButton ZoneChart = new NavigationButton("Zone Chart", new ZoneChart());
        private NavigationButton CustomerProfiler = new NavigationButton("Customer Profiler", new CustomerProfile());
        private NavigationButton Reconcilliation = new NavigationButton("Reconcilliation", new Reconcilliation());

        public ReceivableDashboard() : base("Receivables Dashboard", "Manage receivables")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPreviousScreen, hkOpenSelection };
            _initializeHKButtons(hkButtons);
            this.AddHKEvents();

            NavigationButton[] navButtons = new NavigationButton[10] { CustomerList, ReceivePayment, Invoices, PricerTiers, SalesCostCodes, SalesTaxCode, CreditCodes, ZoneChart, CustomerProfiler, Reconcilliation };
            _initiallizeNavButtons(navButtons);
        }

        private void AddHKEvents()
        {
            hkPreviousScreen.GetButton().Click += new EventHandler(_navigateToPrev);
            hkOpenSelection.GetButton().Enabled = false;
        }

        // Left Key Control
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                // Skip 4 tab stops and move to the next 5th tab stop
                int nextTabIndex = this.GetCurrentTabIndex() + 5;
                if (nextTabIndex > this.Controls.Count)
                {
                    nextTabIndex = this.GetCurrentTabIndex();
                }
                this.ActivateControlByTabIndex(nextTabIndex);
                return true; // Return true to indicate that the key was handled
            } else if(keyData == Keys.Left)
            {
                // Skip 4 tab stops and move to the next 5th tab stop
                int nextTabIndex = this.GetCurrentTabIndex() - 5;
                if (nextTabIndex < 0)
                {
                    nextTabIndex = this.GetCurrentTabIndex();
                }
                this.ActivateControlByTabIndex(nextTabIndex);
                return true; // Return true to indicate that the key was handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private int GetCurrentTabIndex()
        {
            Control activeControl = this.ActiveControl;
            if (activeControl != null)
            {
                return activeControl.TabIndex;
            }
            else
            {
                return -1; // No control with focus found
            }
        }

        private void ActivateControlByTabIndex(int tabIndex)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].TabIndex == tabIndex)
                {
                    this.Controls[i].Focus();
                    break;
                }
            }
        }
    }
}

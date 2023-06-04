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
    public partial class InventoryDashboard : GlobalLayout
    {
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);
        private HotkeyButton hkOpenSelection = new HotkeyButton("Enter", "Open Selection", Keys.Enter);

        private NavigationButton SKUList = new NavigationButton("SKU List", new SKUList());
        private NavigationButton Receive = new NavigationButton("Receive", new ReceiveInventory());
        private NavigationButton Deplete = new NavigationButton("Deplete", new DepleteInventory());
        private NavigationButton PriceChange = new NavigationButton("Price Change", new PriceChange());
        private NavigationButton CategoryMargins = new NavigationButton("Category Margins", new CategoryMargin());
        private NavigationButton Vendors = new NavigationButton("Vendors", new VendorList());
        private NavigationButton RecorderReport = new NavigationButton("Recorder Report", new Recorder());
        private NavigationButton ModuleInformation = new NavigationButton("Module Information", new ModuleInformation());

        public InventoryDashboard() : base("Inventory Dashboard", "Manage inventory")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPreviousScreen, hkOpenSelection };
            _initializeHKButtons(hkButtons);
            this.AddHKEvents();

            NavigationButton[] navButtons = new NavigationButton[8] { SKUList, Receive, Deplete, PriceChange, CategoryMargins, Vendors, RecorderReport, ModuleInformation };
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
                if (nextTabIndex >= this.Controls.Count)
                {
                    nextTabIndex = this.GetCurrentTabIndex();
                }
                this.ActivateControlByTabIndex(nextTabIndex);
                return true; // Return true to indicate that the key was handled
            }
            else if (keyData == Keys.Left)
            {
                // Skip 4 tab stops and move to the next 5th tab stop
                int nextTabIndex = this.GetCurrentTabIndex() - 5;
                if (nextTabIndex <= 0)
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
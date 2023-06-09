using mjc_dev.common;
using mjc_dev.common.components;
using mjc_dev.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.forms.customer
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

        private GridViewOrigin customerListGrid = new GridViewOrigin();
        private DataGridView CLGridRefer;
        private DashboardModel model = new DashboardModel();

        public CustomerList() : base("Customer List", "Manage customers in the system")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[11] { hkAdds, hkDeletes, hkSelects, hkCustSummary, Invocies, RecurringPayment, HistoryInv, OrderEntry, PaymentHistory, ReceivePayment, ArchivedCustomers };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitCustomerList();
        }
        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                CustomerDetail detailModal = new CustomerDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedCustomerId = 0;
                if (CLGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in CLGridRefer.SelectedRows)
                    {
                        selectedCustomerId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = model.DeleteCustomer(selectedCustomerId);
                if (refreshData)
                {
                    LoadCustomerList();
                }
            };
            hkSelects.GetButton().Click += (sender, e) =>
            {
                updateCustomer();
            };
        }

        private void InitCustomerList()
        {
            CLGridRefer = customerListGrid.GetGrid();
            CLGridRefer.Location = new Point(0, 95);
            CLGridRefer.Width = this.Width;
            CLGridRefer.Height = this.Height - 295;
            this.Controls.Add(CLGridRefer);
            this.CLGridRefer.CellDoubleClick += (sender, e) =>
            {
                updateCustomer();
            };

            LoadCustomerList();
        }

        private void LoadCustomerList()
        {
            string filter = "";
            var refreshData = model.LoadCustomerData(filter);
            Console.WriteLine(refreshData);
            if (refreshData)
            {
                CLGridRefer.DataSource = model.CustomerDataList;
                CLGridRefer.Columns[0].Visible = false;
                CLGridRefer.Columns[1].HeaderText = "Customer #";
                CLGridRefer.Columns[1].Width = 300;
                CLGridRefer.Columns[2].HeaderText = "Name";
                CLGridRefer.Columns[2].Width = 300;
                CLGridRefer.Columns[3].HeaderText = "Address";
                CLGridRefer.Columns[3].Width = 500;
                CLGridRefer.Columns[4].HeaderText = "City";
                CLGridRefer.Columns[4].Width = 200;
                CLGridRefer.Columns[5].HeaderText = "State";
                CLGridRefer.Columns[5].Width = 200;
                CLGridRefer.Columns[6].HeaderText = "Zipcode";
                CLGridRefer.Columns[6].Width = 200;
            }
        }

        private void updateCustomer()
        {
            CustomerDetail detailModal = new CustomerDetail();

            int rowIndex = CLGridRefer.CurrentCell.RowIndex;

            DataGridViewRow row = CLGridRefer.Rows[rowIndex];

            int vendorId = (int)row.Cells[0].Value;
            List<dynamic> customerData = new List<dynamic>();
            customerData = model.GetCustomerData(vendorId);
            detailModal.setDetails(customerData, customerData[0].id);

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadCustomerList();
            }
        }
    }
}

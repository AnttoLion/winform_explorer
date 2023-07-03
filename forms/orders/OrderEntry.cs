using mjc_dev.common.components;
using mjc_dev.common;
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
using mjc_dev.forms.sku;

namespace mjc_dev.forms.orders
{
    public partial class OrderEntry : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkSwitchColumn = new HotkeyButton("Alt + S", "Switch column", Keys.S);
        private HotkeyButton hkOpenCustomer = new HotkeyButton("F5", "Open Customer", Keys.F5);
        private HotkeyButton hkCheckStok = new HotkeyButton("F6", "Stok", Keys.F6);
        private HotkeyButton hkHeldOrders = new HotkeyButton("F7", "Held Orders", Keys.F7);
        private HotkeyButton hkProfiler = new HotkeyButton("F8", "Profiler", Keys.F8);
        private HotkeyButton hkHeldOrdersForCustomer = new HotkeyButton("F9", "Held Orders for Customer", Keys.F9);

        private DataGridView OEGridRefer;
        private int OEGridSelectedIndex = 0;

        private string searchKey;

        private CustomersModel CustomersModelObj = new CustomersModel();
        private SKUModel SKUModelObj = new SKUModel();
        private OrderItemsModel OrderItemsModalObj = new OrderItemsModel();

        public OrderEntry() : base("Order Entry - Select a Customer", "Select a customer to start an order for")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[8] { hkAdds, hkSelects, hkSwitchColumn, hkOpenCustomer, hkCheckStok, hkHeldOrders, hkProfiler, hkHeldOrdersForCustomer };
            _initializeHKButtons(hkButtons);
            //_addComingSoon();

            InitHKButtonEvents();
            InitOrderItemsList();

            InitGridFooter();

            //ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
        }

        private void InitHKButtonEvents()
        {
            hkAdds.GetButton().Click += (s, e) =>
            {
                addProcessOrder(s, e);
            };
            hkSelects.GetButton().Click += (s, e) =>
            {
                int sRId = (int)OEGridRefer.SelectedRows[0].Cells[0].Value;
                addProcessOrder(s, e, sRId);
            };
        }

        private void InitOrderItemsList()
        {
            GridViewOrigin OrderEntryLookupGrid = new GridViewOrigin();
            OEGridRefer = OrderEntryLookupGrid.GetGrid();
            OEGridRefer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(157, 196, 235);
            OEGridRefer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 63, 96);
            OEGridRefer.ColumnHeadersDefaultCellStyle.Padding = new Padding(12);
            OEGridRefer.Location = new Point(0, 95);
            OEGridRefer.Width = this.Width;
            OEGridRefer.Height = 745;
            OEGridRefer.AllowUserToAddRows = false;

            OEGridRefer.Columns.Clear();

            OEGridRefer.Columns.Add("id", "id");
            OEGridRefer.Columns["id"].Visible = false;

            OEGridRefer.Columns.Add("customerNumber", "Customer#");
            OEGridRefer.Columns["customerNumber"].Width = 200;
            //OEGridRefer.Columns["customerNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OEGridRefer.Columns.Add("customerName", "Name");
            OEGridRefer.Columns["customerName"].Width = 300;

            OEGridRefer.Columns.Add("address1", "Address");
            OEGridRefer.Columns["address1"].Width = 500;

            OEGridRefer.Columns.Add("city", "City");
            OEGridRefer.Columns["city"].Width = 200;

            OEGridRefer.Columns.Add("state", "State");
            OEGridRefer.Columns["state"].Width = 200;

            OEGridRefer.Columns.Add("zipcode", "Zip");
            OEGridRefer.Columns["zipcode"].Width = 200;

            OEGridRefer.Columns.Add("SC", "SC");
            OEGridRefer.Columns["SC"].Width = 200;

            OEGridRefer.EditingControlShowing += OEGridRefer_EditingControlShowing;
            this.OEGridRefer.CellDoubleClick += (s, e) =>
            {
                int sRId = (int)OEGridRefer.SelectedRows[0].Cells[0].Value;
                addProcessOrder(s, e, sRId);
            };

            this.Controls.Add(OEGridRefer);

            this.LoadSKUList();
        }

        private void InitGridFooter()
        {
            List<dynamic> GridFooterComponents = new List<dynamic>();

            _addFormInputs(GridFooterComponents, 30, 720, 650, 42, 820);

            List<dynamic> GridFooterComponents1 = new List<dynamic>();

            _addFormInputs(GridFooterComponents1, 680, 720, 650, 42, 820);
        }

        public void LoadSKUList(bool keepSelection = true)
        {
            OEGridRefer.Rows.Clear();

            DataTable dataTable = CustomersModelObj.LoadCustomerTable();

            foreach (DataRow row in dataTable.Rows)
            {
                int rowIndex = OEGridRefer.Rows.Add();
                DataGridViewRow newRow = OEGridRefer.Rows[rowIndex];
                newRow.Cells["id"].Value = row["id"];
                newRow.Cells["customerNumber"].Value = row["customerNumber"];
                newRow.Cells["customerName"].Value = row["customerName"];
                newRow.Cells["address1"].Value = row["address1"];
                newRow.Cells["city"].Value = row["city"];
                newRow.Cells["state"].Value = row["state"];
                newRow.Cells["zipcode"].Value = row["zipcode"];
            }
        }

        private void OEGridRefer_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewComboBoxEditingControl comboBoxEditingControl = e.Control as DataGridViewComboBoxEditingControl;

            if (comboBoxEditingControl != null)
            {
                comboBoxEditingControl.DropDownHeight = 300; // Set the desired height for the drop-down menu
            }
        }

        private void addProcessOrder(object sender, EventArgs e, int customerId = 0)
        {
            this.Hide();
            ProcessOrder processForm = new ProcessOrder(customerId);
            _navigateToForm(sender, e, processForm);
        }
    }
}
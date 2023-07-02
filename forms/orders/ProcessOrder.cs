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
using static System.Windows.Forms.AxHost;
using System.Net;
using System.Reflection.Emit;
using mjc_dev.model;
using static mjc_dev.forms.sku.SKUInformation;
using System.Xml.Linq;

namespace mjc_dev.forms.orders
{
    public partial class ProcessOrder : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkSelect = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkSwitchColumn = new HotkeyButton("Alt + S", "Switch column", Keys.S);
        private HotkeyButton hkOpenCustomer = new HotkeyButton("F5", "Open Customer", Keys.F5);
        private HotkeyButton hkCheckStok = new HotkeyButton("F6", "Stok", Keys.F6);
        private HotkeyButton hkHeldOrders = new HotkeyButton("F7", "Held Orders", Keys.F7);
        private HotkeyButton hkProfiler = new HotkeyButton("F8", "Profiler", Keys.F8);
        private HotkeyButton hkHeldOrdersForCustomer = new HotkeyButton("F9", "Held Orders for Customer", Keys.F9);

        private FComboBox Customer = new FComboBox("Customer#", 150);
        private FlabelConstant CustomerName = new FlabelConstant("Name", 150);
        private FlabelConstant Terms = new FlabelConstant("Terms", 150);
        private FlabelConstant Zone = new FlabelConstant("Zone", 150);
        private FlabelConstant Position = new FlabelConstant("PO#", 150);

        private FlabelConstant Requested = new FlabelConstant("Requested");
        private FlabelConstant Filled = new FlabelConstant("Filled");
        private FlabelConstant QtyOnHold = new FlabelConstant("Qty on Hand");
        private FlabelConstant QtyAllocated = new FlabelConstant("Qty Allocated");
        private FlabelConstant QtyAvailable = new FlabelConstant("Qty Available");
        private FlabelConstant Subtotal = new FlabelConstant("Subtotal");
        private FlabelConstant TaxPercent = new FlabelConstant("7.250% Tax");
        private FlabelConstant TotalSale = new FlabelConstant("Total Sale");

        private DataGridView OEGridRefer;
        private int OEGridSelectedIndex = 0;

        private string searchKey;

        private CustomersModel CustomersModelObj = new CustomersModel();
        private OrderItemsModel OrderItemsModalObj = new OrderItemsModel();

        public ProcessOrder() : base("Order Entry - Select a Customer", "Select a customer to start an order for")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[8] { hkAdds, hkSelect, hkSwitchColumn, hkOpenCustomer, hkCheckStok, hkHeldOrders, hkProfiler, hkHeldOrdersForCustomer };
            _initializeHKButtons(hkButtons);
            //_addComingSoon();

            InitHKButtonEvents();
            InitCustomerInfo();
            InitOrderItemsList();

            InitGridFooter();

            //ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
        }

        private void InitHKButtonEvents()
        {
            hkAdds.GetButton().Click += (s, e) => insertButton_Click(s, e);
        }

        private void InitCustomerInfo()
        {
            List<dynamic> FormComponents = new List<dynamic>();

            FormComponents.Add(Customer);
            FormComponents.Add(CustomerName);
            FormComponents.Add(Terms);
            FormComponents.Add(Zone);
            FormComponents.Add(Position);

            _addFormInputs(FormComponents, 30, 110, 650, 42, 180);

            List<KeyValuePair<int, string>> CustomerList = new List<KeyValuePair<int, string>>();
            CustomerList = CustomersModelObj.GetCustomerList();
            foreach (KeyValuePair<int, string> item in CustomerList)
            {
                int id = item.Key;
                string name = item.Value;
                Customer.GetComboBox().Items.Add(new FComboBoxItem(id, name));
            }

            Customer.GetComboBox().SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);

            Customer.GetComboBox().SelectedIndex = 0;
            ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FComboBoxItem selectedItem = (FComboBoxItem)Customer.GetComboBox().SelectedItem;
            int customerId = selectedItem.Id;

            var customerData = CustomersModelObj.GetCustomerData(customerId);
            if (customerData != null)
            {
                if(customerData.customerName != "") CustomerName.SetContext(customerData.customerName);
                else CustomerName.SetContext("n/a");

                if (customerData.terms != "") Terms.SetContext(customerData.terms);
                else Terms.SetContext("n/a");

                if(customerData.zipcode != "") Zone.SetContext(customerData.zipcode);
                else Zone.SetContext("n/a");

                if(customerData.poRequired != "") Position.SetContext(customerData.poRequired);
                else Position.SetContext("n/a");
            }
        }

        private void InitOrderItemsList()
        {
            GridViewOrigin OrderEntryLookupGrid = new GridViewOrigin();
            OEGridRefer = OrderEntryLookupGrid.GetGrid();
            OEGridRefer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(157, 196, 235);
            OEGridRefer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 63, 96);
            OEGridRefer.ColumnHeadersDefaultCellStyle.Padding = new Padding(12);
            OEGridRefer.Location = new Point(0, 200);
            OEGridRefer.Width = this.Width;
            OEGridRefer.Height = 490;
            OEGridRefer.ReadOnly = false;

            OEGridRefer.ReadOnly = false;
            OEGridRefer.EditMode = DataGridViewEditMode.EditOnEnter;

            OEGridRefer.Columns.Add("id", "id");
            OEGridRefer.Columns["id"].Visible = false;

            OEGridRefer.Columns.Add("orderId", "orderId");
            OEGridRefer.Columns["orderId"].Visible = false;

            OEGridRefer.Columns.Add("skuId", "skuId");
            OEGridRefer.Columns["skuId"].Visible = false;

            DataGridViewComboBoxColumn skuColumn = new DataGridViewComboBoxColumn();

            skuColumn.Items.Add("fff");
            skuColumn.Items.Add("ggg");

            //DataGridViewCellStyle style = new DataGridViewCellStyle(); style.BackColor = Color.LightBlue; skuColumn.DefaultCellStyle = style;
            skuColumn.Name = "sku"; 
            skuColumn.HeaderText = "SK#"; 
            skuColumn.Width = 300; 
            OEGridRefer.Columns.Add(skuColumn);

            OEGridRefer.Columns.Add("quantity", "Quantity");
            OEGridRefer.Columns["quantity"].Width = 200;
            OEGridRefer.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OEGridRefer.Columns.Add("description", "Description");
            OEGridRefer.Columns["description"].Width = 400;

            OEGridRefer.Columns.Add("tax", "Tax");
            OEGridRefer.Columns["tax"].Width = 200;

            OEGridRefer.Columns.Add("disc", "Disc%");
            OEGridRefer.Columns["disc"].Width = 200;

            OEGridRefer.Columns.Add("unitPrice", "Unit Price");
            OEGridRefer.Columns["unitPrice"].Width = 200;
            OEGridRefer.Columns["unitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OEGridRefer.Columns.Add("lineTotal", "Line Total");
            OEGridRefer.Columns["lineTotal"].Width = 200;
            OEGridRefer.Columns["lineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            OEGridRefer.Columns.Add("SC", "SC");
            OEGridRefer.Columns["SC"].Width = 200;

            this.Controls.Add(OEGridRefer);

            //LoadSKUList();
        }

        private void InitGridFooter()
        {
            List<dynamic> GridFooterComponents = new List<dynamic>();

            GridFooterComponents.Add(Requested);
            GridFooterComponents.Add(Filled);

            _addFormInputs(GridFooterComponents, 30, 720, 650, 42, 820);

            List<dynamic> GridFooterComponents1 = new List<dynamic>();

            GridFooterComponents1.Add(QtyOnHold);
            GridFooterComponents1.Add(QtyAllocated);
            GridFooterComponents1.Add(QtyAvailable);
            GridFooterComponents1.Add(Subtotal);
            GridFooterComponents1.Add(TaxPercent);
            GridFooterComponents1.Add(TotalSale);

            _addFormInputs(GridFooterComponents1, 680, 720, 650, 42, 820);
        }

        public void LoadSKUList(bool keepSelection = true)
        {
            var refreshData = OrderItemsModalObj.LoadOrderItemsList(this.searchKey);
            if (refreshData)
            {
                OEGridRefer.DataSource = OrderItemsModalObj.OIList;
            }

            OEGridRefer.Columns[0].Visible = false;
            OEGridRefer.Columns[1].HeaderText = "orderId";
            OEGridRefer.Columns[1].Visible = false;
            OEGridRefer.Columns[2].HeaderText = "skuId";
            OEGridRefer.Columns[2].Visible = false;
            OEGridRefer.Columns[3].HeaderText = "SKU#";
            OEGridRefer.Columns[3].Width = 300;
            OEGridRefer.Columns[4].HeaderText = "Quantity";
            OEGridRefer.Columns[4].Width = 200;
            OEGridRefer.Columns[5].HeaderText = "Description";
            OEGridRefer.Columns[5].Width = 400;
            OEGridRefer.Columns[6].HeaderText = "Tax";
            OEGridRefer.Columns[6].Width = 200;
            OEGridRefer.Columns[7].HeaderText = "Disc%";
            OEGridRefer.Columns[7].Width = 200;
            OEGridRefer.Columns[8].HeaderText = "Unit Price";
            OEGridRefer.Columns[8].Width = 200;
            OEGridRefer.Columns[9].HeaderText = "Line Total";
            OEGridRefer.Columns[9].Width = 200;
            OEGridRefer.Columns[10].HeaderText = "SC";
            OEGridRefer.Columns[10].Width = 200;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow lastRow = OEGridRefer.Rows[OEGridRefer.Rows.Count - 1];
            string lastRowValue = lastRow.Cells[OEGridRefer.Columns["sku"].Index].Value?.ToString(); // Assuming the first cell of each row should have a value

            if (string.IsNullOrEmpty(lastRowValue))
            {
                OEGridRefer.CurrentCell = OEGridRefer[OEGridRefer.Columns["sku"].Index, lastRow.Index];
                // Last row is empty, do not add a new row
                return;
            }

            OEGridRefer.Rows.Add();
        }

    }
}
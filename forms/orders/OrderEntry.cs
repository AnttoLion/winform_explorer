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

namespace mjc_dev.forms.orders
{
    public partial class OrderEntry : GlobalLayout
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

        private GridViewOrigin OrderEntryLookupGrid = new GridViewOrigin();
        private DataGridView OEGridRefer = new DataGridView();
        private int OEGridSelectedIndex = 0;

        private string searchKey;

        private CustomersModel CustomersModelObj = new CustomersModel();
        private OrderItemsModel OrderModelObj = new OrderItemsModel();

        public OrderEntry() : base("Order Entry - Select a Customer", "Select a customer to start an order for")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[8] { hkAdds, hkSelect, hkSwitchColumn, hkOpenCustomer, hkCheckStok, hkHeldOrders, hkProfiler, hkHeldOrdersForCustomer };
            _initializeHKButtons(hkButtons);
            //_addComingSoon();

            InitCustomerInfo();
            //InitCustomerList();

            //ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
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
                Customer.GetComboBox().Items.Add(new CategoryComboBoxItem(id, name));
            }

            Customer.GetComboBox().SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);

            Customer.GetComboBox().SelectedIndex = 0;
            ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryComboBoxItem selectedItem = (CategoryComboBoxItem)Customer.GetComboBox().SelectedItem;
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

        private void InitCustomerList()
        {
            OEGridRefer = OrderEntryLookupGrid.GetGrid();
            OEGridRefer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(157, 196, 235);
            OEGridRefer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 63, 96);
            OEGridRefer.ColumnHeadersDefaultCellStyle.Padding = new Padding(12);
            OEGridRefer.Location = new Point(0, 300);
            OEGridRefer.Width = this.Width;
            OEGridRefer.Height = this.Height - 295;
            this.Controls.Add(OEGridRefer);
            this.OEGridRefer.CellDoubleClick += (sender, e) =>
            {
                //updateCustomer();
            };

            //LoadCustomerList();
        }

        public void LoadSKUList(bool archivedView = false, bool keepSelection = true)
        {
            if (this.searchKey == "")
            {
                this._changeFormText("SKU List");
            }
            else
            {
                this._changeFormText("SKU List searched by " + this.searchKey);
            }
            var refreshData = OrderModelObj.LoadOrderData(this.searchKey, archivedView);
            if (refreshData)
            {
                OEGridRefer.DataSource = OrderModelObj.SKUDataList;
                OEGridRefer.Columns[0].Visible = false;
                OEGridRefer.Columns[1].HeaderText = "SKU#";
                OEGridRefer.Columns[1].Width = 300;
                OEGridRefer.Columns[2].HeaderText = "Category";
                OEGridRefer.Columns[2].Width = 300;
                OEGridRefer.Columns[3].HeaderText = "Description";
                OEGridRefer.Columns[3].Width = 500;
                OEGridRefer.Columns[4].HeaderText = "Qty Avail";
                OEGridRefer.Columns[4].Width = 300;
                OEGridRefer.Columns[5].HeaderText = "Qty Tracking";
                OEGridRefer.Columns[5].Width = 300;
            }

            if (keepSelection)
            {
                OEGridRefer.ClearSelection();
                if (OEGridSelectedIndex >= 0 && OEGridSelectedIndex < OEGridRefer.Rows.Count)
                {
                    OEGridRefer.Rows[OEGridSelectedIndex].Selected = true;
                    OEGridRefer.CurrentCell =   [1, OEGridSelectedIndex];
                }
            }
        }
    }
}

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
using System.Security.Policy;
using mjc_dev.model;

namespace mjc_dev.forms
{
    public partial class CustomerProfile : GlobalLayout
    {

        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);
        private HotkeyButton hkCustomerJump = new HotkeyButton("F4", "Customer Jump", Keys.F4);

        private FComboBox Customer = new FComboBox("Customer#", 150);
        private FlabelConstant CustomerName = new FlabelConstant("Name", 150);

        private DataGridView POGridRefer;
        private int POGridSelectedIndex = 0;
        private string searchKey;

        private CustomersModel CustomersModelObj = new CustomersModel();
        public CustomerProfile(int customerId = 0) : base("Customer Profiler", "Profile view of customers and their history of purchases")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPreviousScreen, hkCustomerJump };
            _initializeHKButtons(hkButtons);
            //            _addComingSoon();
            /*
                        foreach (HotkeyButton button in hkButtons)
                        {
                            if (button != hkPreviousScreen)
                                button.GetButton().Click += new EventHandler(_hotkeyTest);
                        }
                        hkPreviousScreen.GetButton().Click += new EventHandler(_navigateToPrev);*/


            InitHKButtonEvents();
            InitCustomerInfo(customerId);
            InitCustomerProfileList();
        }
        private void InitHKButtonEvents()
        {

        }
        private void InitCustomerInfo(int customerId = 0)
        {
            List<dynamic> FormComponents = new List<dynamic>();

            FormComponents.Add(Customer);
            FormComponents.Add(CustomerName);

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

            if (customerId == 0)
            {
                Customer.GetComboBox().SelectedIndex = 0;
                ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
            }
            else
            {
                int index = Customer.GetComboBox().Items.Cast<FComboBoxItem>().ToList().FindIndex(item => item.Id == customerId);
                Customer.GetComboBox().SelectedIndex = index;
                ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FComboBoxItem selectedItem = (FComboBoxItem)Customer.GetComboBox().SelectedItem;
            int customerId = selectedItem.Id;

            var customerData = CustomersModelObj.GetCustomerData(customerId);
            if (customerData != null)
            {
                if (customerData.customerName != "") CustomerName.SetContext(customerData.customerName);
                else CustomerName.SetContext("n/a");
            }
        }

        private void InitCustomerProfileList()
        {
            GridViewOrigin OrderEntryLookupGrid = new GridViewOrigin();
            POGridRefer = OrderEntryLookupGrid.GetGrid();
            POGridRefer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(157, 196, 235);
            POGridRefer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 63, 96);
            POGridRefer.ColumnHeadersDefaultCellStyle.Padding = new Padding(12);
            POGridRefer.Location = new Point(0, 200);
            POGridRefer.Width = this.Width;
            POGridRefer.Height = 490;
            POGridRefer.ReadOnly = false;

            POGridRefer.ReadOnly = false;
            POGridRefer.EditMode = DataGridViewEditMode.EditOnEnter;

            POGridRefer.Columns.Add("Invoice#", "Invoice#");
            POGridRefer.Columns[0].Width = 300;
            POGridRefer.Columns.Add("SKU#", "SKU#");
            POGridRefer.Columns[1].Width = 300;
            POGridRefer.Columns.Add("Description", "Description");
            POGridRefer.Columns[2].Width = 500;
            POGridRefer.Columns.Add("Date", "Date");
            POGridRefer.Columns[3].Width = 300;
            POGridRefer.Columns.Add("Qty", "Qty");
            POGridRefer.Columns[4].Width = 300;
            POGridRefer.Columns.Add("Price", "Price");
            POGridRefer.Columns[5].Width = 100;
            POGridRefer.Columns.Add("SC", "SC");
            POGridRefer.Columns[6].Width = 100;
            this.Controls.Add(POGridRefer);
        }
    }
}
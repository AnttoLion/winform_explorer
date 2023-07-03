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
using mjc_dev.forms.sku;

namespace mjc_dev.forms.orders
{
    public partial class ProcessOrder : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelect = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkAddMessage = new HotkeyButton("F2", "Add message", Keys.F2);
        private HotkeyButton hkCustomerProfiler = new HotkeyButton("F4", "Customer Profiler", Keys.F4);
        private HotkeyButton hkSKUInfo = new HotkeyButton("F5", "SKU Info", Keys.F5);
        private HotkeyButton hkSortLines = new HotkeyButton("Alt+S", "Sort lines", Keys.S, "alt");
        private HotkeyButton hkCloseOrder = new HotkeyButton("ESC", "Close order", Keys.Escape);

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

        private DataGridView POGridRefer;
        private int POGridSelectedIndex = 0;

        private string searchKey;

        private CustomersModel CustomersModelObj = new CustomersModel();
        private SKUModel SKUModelObj = new SKUModel();
        private OrderItemsModel OrderItemsModalObj = new OrderItemsModel();

        public ProcessOrder(int customerId) : base("Process an Order", "Fill out the customer order")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[8] { hkAdds, hkDeletes, hkSelect, hkAddMessage, hkCustomerProfiler, hkSKUInfo, hkSortLines, hkCloseOrder };
            _initializeHKButtons(hkButtons, false);
            //_addComingSoon();

            InitHKButtonEvents();
            InitCustomerInfo(customerId);
            InitOrderItemsList();

            InitGridFooter();

            //ComboBox_SelectedIndexChanged(Customer.GetComboBox(), EventArgs.Empty);
        }

        private void InitHKButtonEvents()
        {
            hkAdds.GetButton().Click += (s, e) => insertButton_Click(s, e);
            hkSortLines.GetButton().Click += (s, e) =>
            {
                MessageBox.Show("");
            };
            hkSelect.GetButton().Click += (s, e) =>
            {
                EditItem(s, e);
            };
            hkCloseOrder.GetButton().Click += (sender, e) =>
            {
                CloseOrderActions CloseOrderActionsModal = new CloseOrderActions();
                this.Enabled = false;
                CloseOrderActionsModal.Show();
                CloseOrderActionsModal.FormClosed += (ss, sargs) =>
                {
                    this.Enabled = true;
                    int saveFlag = CloseOrderActionsModal.GetSaveFlage();
                    MessageBox.Show(saveFlag.ToString());
                };
            };
            hkDeletes.GetButton().Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    POGridRefer.Rows.Remove(POGridRefer.SelectedRows[0]);
                    //e.CancelEvent();
                }
            };
        }

        private void InitCustomerInfo(int customerId = 0)
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

            if(customerId == 0)
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

            Position.GetLabel().Focus();
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
            POGridRefer = OrderEntryLookupGrid.GetGrid();
            POGridRefer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(157, 196, 235);
            POGridRefer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 63, 96);
            POGridRefer.ColumnHeadersDefaultCellStyle.Padding = new Padding(12);
            POGridRefer.Location = new Point(0, 200);
            POGridRefer.Width = this.Width;
            POGridRefer.Height = 490;

            POGridRefer.ReadOnly = false;
            POGridRefer.EditMode = DataGridViewEditMode.EditOnEnter;

            POGridRefer.AllowUserToAddRows = false;

            POGridRefer.Columns.Add("id", "id");
            POGridRefer.Columns["id"].Visible = false;

            POGridRefer.Columns.Add("orderId", "orderId");
            POGridRefer.Columns["orderId"].Visible = false;

            POGridRefer.Columns.Add("skuId", "skuId");
            POGridRefer.Columns["skuId"].Visible = false;

            POGridRefer.Columns.Add("sku", "SKU#");
            POGridRefer.Columns["sku"].Width = 200;
            POGridRefer.Columns["sku"].ReadOnly = true;

            POGridRefer.Columns.Add("quantity", "Quantity");
            POGridRefer.Columns["quantity"].Width = 200;
            POGridRefer.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            POGridRefer.Columns.Add("description", "Description");
            POGridRefer.Columns["description"].Width = 400;

            POGridRefer.Columns.Add("tax", "Tax");
            POGridRefer.Columns["tax"].Width = 200;

            POGridRefer.Columns.Add("disc", "Disc%");
            POGridRefer.Columns["disc"].Width = 200;

            POGridRefer.Columns.Add("unitPrice", "Unit Price");
            POGridRefer.Columns["unitPrice"].Width = 200;
            POGridRefer.Columns["unitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            POGridRefer.Columns.Add("lineTotal", "Line Total");
            POGridRefer.Columns["lineTotal"].Width = 200;
            POGridRefer.Columns["lineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            POGridRefer.Columns.Add("SC", "SC");
            POGridRefer.Columns["SC"].Width = 200;

            POGridRefer.EditingControlShowing += POGridRefer_EditingControlShowing;

            this.Controls.Add(POGridRefer);

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
                POGridRefer.DataSource = OrderItemsModalObj.OIList;
            }

            POGridRefer.Columns[0].Visible = false;
            POGridRefer.Columns[1].HeaderText = "orderId";
            POGridRefer.Columns[1].Visible = false;
            POGridRefer.Columns[2].HeaderText = "skuId";
            POGridRefer.Columns[2].Visible = false;
            POGridRefer.Columns[3].HeaderText = "SKU#";
            POGridRefer.Columns[3].Width = 300;
            POGridRefer.Columns[4].HeaderText = "Quantity";
            POGridRefer.Columns[4].Width = 200;
            POGridRefer.Columns[5].HeaderText = "Description";
            POGridRefer.Columns[5].Width = 400;
            POGridRefer.Columns[6].HeaderText = "Tax";
            POGridRefer.Columns[6].Width = 200;
            POGridRefer.Columns[7].HeaderText = "Disc%";
            POGridRefer.Columns[7].Width = 200;
            POGridRefer.Columns[8].HeaderText = "Unit Price";
            POGridRefer.Columns[8].Width = 200;
            POGridRefer.Columns[9].HeaderText = "Line Total";
            POGridRefer.Columns[9].Width = 200;
            POGridRefer.Columns[10].HeaderText = "SC";
            POGridRefer.Columns[10].Width = 200;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            /*
            DataGridViewRow lastRow = POGridRefer.Rows[POGridRefer.Rows.Count - 1];
            string lastRowValue = lastRow.Cells[POGridRefer.Columns["sku"].Index].Value?.ToString(); // Assuming the first cell of each row should have a value

            if (string.IsNullOrEmpty(lastRowValue))
            {
                POGridRefer.CurrentCell = POGridRefer[POGridRefer.Columns["sku"].Index, lastRow.Index];
                // Last row is empty, do not add a new row
                return;
            }

            POGridRefer.Rows.Add();
            */

            SKUListForProcessOrder SLFPOForm = new SKUListForProcessOrder();
            _navigateToForm(sender, e, SLFPOForm);
            this.Hide();

            SLFPOForm.VisibleChanged += (ss, ee) => {
                if (SLFPOForm.Visible == false && SLFPOForm.SelectFlag == true)
                {
                    int rowIndex = POGridRefer.Rows.Add();
                    DataGridViewRow newRow = POGridRefer.Rows[rowIndex];
                    //MessageBox.Show(SLFPOForm.GetSelectedSKUName().ToString());
                    newRow.Cells["skuId"].Value = SLFPOForm.GetSelectedSKUId();
                    newRow.Cells["sku"].Value = SLFPOForm.GetSelectedSKUName();
                }
            };
        }

        private void EditItem(object sender, EventArgs e)
        {
            SKUListForProcessOrder SLFPOForm = new SKUListForProcessOrder();
            _navigateToForm(sender, e, SLFPOForm);
            this.Hide();

            SLFPOForm.VisibleChanged += (ss, ee) => {
                if (SLFPOForm.Visible == false && SLFPOForm.SelectFlag == true)
                {
                    //int rowIndex = POGridRefer.SelectedRows[0];
                    DataGridViewRow newRow = POGridRefer.SelectedRows[0];
                    //MessageBox.Show(SLFPOForm.GetSelectedSKUName().ToString());
                    newRow.Cells["skuId"].Value = SLFPOForm.GetSelectedSKUId();
                    newRow.Cells["sku"].Value = SLFPOForm.GetSelectedSKUName();
                }
            };
        }

        private void POGridRefer_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewComboBoxEditingControl comboBoxEditingControl = e.Control as DataGridViewComboBoxEditingControl;

            if (comboBoxEditingControl != null)
            {
                comboBoxEditingControl.DropDownHeight = 300; // Set the desired height for the drop-down menu
            }
        }
    }
}
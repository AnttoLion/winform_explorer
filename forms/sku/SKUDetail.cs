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
using static System.Windows.Forms.AxHost;
using System.Net;
using System.Reflection.Emit;
using mjc_dev.forms.price;

namespace mjc_dev.forms.sku
{
    public partial class SKUDetail : GlobalLayout
    {
        private HotkeyButton hkSKUMemo = new HotkeyButton("F2", "SKU Memo", Keys.F2);
        private HotkeyButton QuickCalcPrice = new HotkeyButton("F3", "Quick Calc Price", Keys.F3);
        private HotkeyButton MiscManagement = new HotkeyButton("F4", "Misc Management", Keys.F4);
        private HotkeyButton ResetPrices = new HotkeyButton("F5", "Reset Prices", Keys.F5);
        private HotkeyButton SetArchived = new HotkeyButton("F9", "Set Archived", Keys.F9);

        private FGroupLabel SKUInfo = new FGroupLabel("SKU Info");
        private FInputBox SKUName = new FInputBox("SKU#");
        private FComboBox categoryCombo = new FComboBox("Category");
        private FInputBox description = new FInputBox("Description");
        private FInputBox measurementUnit = new FInputBox("Unit of Measure");
        private FInputBox weight = new FInputBox("Weight/UOM");
        private FInputBox costCode = new FInputBox("Sales/Cost Code");
        private FInputBox assetAcct = new FInputBox("SKU Asset Art");

        private FCheckBox taxable = new FCheckBox("taxable");
        private FCheckBox maintainQtys = new FCheckBox("Maintain Qtys");
        private FCheckBox allowDiscount = new FCheckBox("Allow discount");
        private FCheckBox commissionable = new FCheckBox("Commissionable");

        private FInputBox orderForm = new FInputBox("OrderForm");
        private FDateTime lastSold = new FDateTime("LastSold");
        private FInputBox manufacturer = new FInputBox("Manufacturer");
        private FInputBox location = new FInputBox("Location");

        private FGroupLabel quantityTracking = new FGroupLabel("QuantityTracking");
        private FInputBox quantity = new FInputBox("Quantity");
        private FInputBox qtyAllocated = new FInputBox("QtyAllocated");
        private FInputBox qtyAvaiable = new FInputBox("QtyAvailable");
        private FInputBox criticalQty = new FInputBox("CirticalQty");
        private FInputBox recorderQty = new FInputBox("RecorderQty");

        private FGroupLabel sales = new FGroupLabel("Sales");
        private FInputBox soldThisMonth = new FInputBox("Sold this Month");
        private FInputBox soldYTD = new FInputBox("Sold YTD");

        private FGroupLabel prices = new FGroupLabel("Prices");
        private FCheckBox freezePrices = new FCheckBox("Freeze prices");
        private FInputBox coreCost = new FInputBox("Core Cost");
        private FInputBox invValue = new FInputBox("Inv Value");

        private FInputBox[] priceTiers;

        private DashboardModel model = new DashboardModel();
        private int selectId = 0;
        private int skuId = 0;
        private int categoryId = 0;
        private string memo;

        public SKUDetail() : base("SKU Information", "Manage details of SKU")
        {
            this.Text = "Sku detail";
            InitializeComponent();
            _initBasicSize();
            this.KeyDown += (s, e) => Form_KeyDown(s, e);

            HotkeyButton[] hkButtons = new HotkeyButton[5] { hkSKUMemo, QuickCalcPrice, MiscManagement, ResetPrices, SetArchived};
            _initializeHKButtons(hkButtons, false);
            AddHotKeyEvents();

            InitForm();
            this.Load += new System.EventHandler(this.Add_Load);
        }

        private void AddHotKeyEvents()
        {
            SetArchived.GetButton().Click += (sender, e) =>
            {
                DialogResult result = MessageBox.Show("Do you want to set archived?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    model.UpdateSKUArchieved(true, this.skuId);
                }
                else if (result == DialogResult.No)
                {
                    model.UpdateSKUArchieved(false, this.skuId);
                }
            };
            hkSKUMemo.GetButton().Click += (sender, e) =>
            {
                SKUMemo MemoModal = new SKUMemo(skuId, memo);
                this.Enabled = false;
                MemoModal.Show();
                MemoModal.FormClosed += (ss, sargs) =>
                {
                    this.memo = MemoModal.getMemo();
                    this.Enabled = true;
                };

            };
        }

        private void InitForm()
        {

            Panel _panel = new System.Windows.Forms.Panel();
            _panel.BackColor = System.Drawing.Color.Transparent;
            _panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            _panel.Location = new System.Drawing.Point(0, 95);
            _panel.Size = new Size(this.Width-15, this.Height - 340);
            _panel.AutoScroll = true;
            this.Controls.Add(_panel);

            List<dynamic> FormComponents = new List<dynamic>();

            FormComponents.Add(SKUInfo);
            FormComponents.Add(SKUName);
            FormComponents.Add(categoryCombo);
            FormComponents.Add(description);
            FormComponents.Add(measurementUnit);
            FormComponents.Add(weight);
            FormComponents.Add(costCode);
            FormComponents.Add(assetAcct);

            List<dynamic> LineComponents = new List<dynamic>();

            LineComponents.Add(taxable);
            LineComponents.Add(maintainQtys);
            FormComponents.Add(LineComponents);

            List<dynamic> LineComponents2 = new List<dynamic>();

            LineComponents2.Add(allowDiscount);
            LineComponents2.Add(commissionable);
            FormComponents.Add(LineComponents2);

            FormComponents.Add(orderForm);
            FormComponents.Add(lastSold);
            FormComponents.Add(manufacturer);
            FormComponents.Add(location);
            _addFormInputs(FormComponents, 30, 20, 800, 50, 700, _panel.Controls);

            List<dynamic> FormComponents2 = new List<dynamic>();
            FormComponents2.Add(quantityTracking);
            FormComponents2.Add(quantity);
            FormComponents2.Add(qtyAllocated);
            FormComponents2.Add(qtyAvaiable);
            FormComponents2.Add(criticalQty);
            FormComponents2.Add(recorderQty);

            FormComponents2.Add(sales);
            FormComponents2.Add(soldThisMonth);
            FormComponents2.Add(soldYTD);

            FormComponents2.Add(prices);
            FormComponents2.Add(freezePrices);
            FormComponents2.Add(coreCost);
            FormComponents2.Add(invValue);

            string filter = "";
            var refreshData = model.LoadPriceTierData(filter);
            if (refreshData)
            {
                List<PriceTierData> pDatas = model.PriceTierDataList;

                priceTiers = new FInputBox[pDatas.Count];
                for (int i = 0; i< pDatas.Count; i++)
                {
                    priceTiers[i] = new FInputBox(pDatas[i].name.ToString());
                    FormComponents2.Add(priceTiers[i]);
                }
            }

            _addFormInputs(FormComponents2, 700, 20, 800, 50, int.MaxValue, _panel.Controls);
        }

        public void setDetails(List<dynamic> data, int id)
        {
            this.skuId = id;
            this.selectId = (int)data[0].category;
            if(data[0].memo != null) this.memo = data[0].memo;

            this.SKUName.GetTextBox().Text = data[0].sku.ToString();
            this.description.GetTextBox().Text = data[0].description.ToString();
            this.measurementUnit.GetTextBox().Text = data[0].measurementUnit.ToString();
            this.weight.GetTextBox().Text = data[0].weight.ToString();
            this.costCode.GetTextBox().Text = data[0].costCode.ToString();
            this.assetAcct.GetTextBox().Text = data[0].assetAccount.ToString();
            this.taxable.GetCheckBox().Checked = (bool)data[0].taxable;
            this.maintainQtys.GetCheckBox().Checked = (bool)data[0].manageStock;
            this.allowDiscount.GetCheckBox().Checked = (bool)data[0].allowDiscounts;
            this.orderForm.GetTextBox().Text = data[0].orderFrom.ToString();

            this.lastSold.GetDateTimePicker().Value = data[0].lastSold.ToLocalTime();
            if(data[0].lastSold != null) this.lastSold.GetDateTimePicker().Value = data[0].lastSold.ToLocalTime();

            this.manufacturer.GetTextBox().Text = data[0].manufacturer.ToString();
            this.location.GetTextBox().Text = data[0].location.ToString();

            this.quantity.GetTextBox().Text = data[0].quantity.ToString();
            this.qtyAllocated.GetTextBox().Text = data[0].qtyAllocated.ToString();
            this.qtyAvaiable.GetTextBox().Text = data[0].qtyAvailable.ToString();
            this.criticalQty.GetTextBox().Text = data[0].qtyCritical.ToString();
            this.recorderQty.GetTextBox().Text = data[0].qtyReorder.ToString();

            this.freezePrices.GetCheckBox().Checked = (bool)data[0].freezePrices;

            this.soldThisMonth.GetTextBox().Text = data[0].soldMonthToDate.ToString();
            this.soldYTD.GetTextBox().Text = data[0].soldYearToDate.ToString();

            this.coreCost.GetTextBox().Text = data[0].coreCost.ToString();
            this.invValue.GetTextBox().Text = data[0].inventoryValue.ToString();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            bool initFlag = true;
            categoryCombo.GetComboBox().Items.Clear();
            List<KeyValuePair<int, string>> CategoryList = new List<KeyValuePair<int, string>>();
            CategoryList = model.GetCategoryItems();
            foreach (KeyValuePair<int, string> item in CategoryList)
            {
                int id = item.Key;
                string name = item.Value;
                if (initFlag && selectId == 0)
                {
                    selectId = id;
                    initFlag = false;
                }
                categoryCombo.GetComboBox().Items.Add(new CategoryComboBoxItem(id, name));
            }

            foreach (CategoryComboBoxItem item in categoryCombo.GetComboBox().Items)
            {
                if (item.Id == selectId)
                {
                    categoryCombo.GetComboBox().SelectedItem = item;
                    break;
                }
            }
        }

        private void saveSKU(object sender, EventArgs e)
        {
            string s_sku_name = SKUName.GetTextBox().Text;

            CategoryComboBoxItem seletedItem = (CategoryComboBoxItem)categoryCombo.GetComboBox().SelectedItem;
            if (seletedItem == null)
            {
                MessageBox.Show("Please select a correct category.");
                return;
            }
            int i_category = seletedItem.Id;

            string s_description = description.GetTextBox().Text;
            string s_measurement_unit = measurementUnit.GetTextBox().Text;
            int i_weight; bool is_i_weight = int.TryParse(weight.GetTextBox().Text, out i_weight);
            if (!is_i_weight) i_weight = 0;
            int i_cost_code; bool is_i_cost_code = int.TryParse(costCode.GetTextBox().Text, out i_cost_code);
            if (!is_i_cost_code) i_cost_code = 0;
            int i_asset_acct; bool is_i_asset_acct = int.TryParse(assetAcct.GetTextBox().Text, out i_asset_acct);
            if (!is_i_asset_acct) i_asset_acct = 0;

            bool b_taxable = taxable.GetCheckBox().Checked;
            bool b_maintain_qty = maintainQtys.GetCheckBox().Checked;
            bool b_allow_discount = allowDiscount.GetCheckBox().Checked;
            bool b_commissionable = commissionable.GetCheckBox().Checked;

            int i_order_from; bool is_i_order_from = int.TryParse(orderForm.GetTextBox().Text, out i_order_from);
            if (!is_i_order_from) i_order_from = 0;

            DateTime d_last_sold = lastSold.GetDateTimePicker().Value;

            string s_manufacturer = manufacturer.GetTextBox().Text;
            string s_location = location.GetTextBox().Text;
            string memo = this.memo;

            int i_quantity; bool is_i_quantity = int.TryParse(quantity.GetTextBox().Text, out i_quantity);
            if (!is_i_quantity) i_quantity = 0;
            int i_qty_allocated; bool is_i_qty_allocated = int.TryParse(qtyAllocated.GetTextBox().Text, out i_qty_allocated);
            if (!is_i_qty_allocated) i_qty_allocated = 0;
            int i_qty_available; bool is_i_qty_available = int.TryParse(qtyAvaiable.GetTextBox().Text, out i_qty_available);
            if (!is_i_qty_available) i_qty_available = 0;
            int i_qty_critical; bool is_i_qty_critical = int.TryParse(criticalQty.GetTextBox().Text, out i_qty_critical);
            if (!is_i_qty_critical) i_qty_critical = 0;
            int i_qty_reorder; bool is_i_qty_reorder = int.TryParse(recorderQty.GetTextBox().Text, out i_qty_reorder);
            if (!is_i_qty_reorder) i_qty_reorder = 0;

            int i_sold_this_month; bool is_i_solid_this_month = int.TryParse(soldThisMonth.GetTextBox().Text, out i_sold_this_month);
            if (!is_i_solid_this_month) i_sold_this_month = 0;
            int i_sold_ytd; bool is_i_sold_ytd = int.TryParse(soldYTD.GetTextBox().Text, out i_sold_ytd);
            if (!is_i_sold_ytd) i_sold_ytd = 0;

            bool b_freeze_prices = freezePrices.GetCheckBox().Checked;

            int i_core_cost; bool is_i_core_cost = int.TryParse(coreCost.GetTextBox().Text, out i_core_cost);
            if (!is_i_core_cost) i_core_cost = 0;
            int i_inv_value; bool is_i_inv_value = int.TryParse(invValue.GetTextBox().Text, out i_inv_value);
            if (!is_i_inv_value) i_inv_value = 0;

            if (s_sku_name == "")
            {
                MessageBox.Show("Please fill String field.");
                return;
            }
            bool refreshData = false;
            if (skuId == 0) refreshData = model.AddSKU(s_sku_name, i_category, s_description, s_measurement_unit, i_weight, i_cost_code, i_asset_acct, b_taxable, b_maintain_qty, b_allow_discount, b_commissionable, i_order_from, d_last_sold, s_manufacturer, s_location, i_quantity, i_qty_allocated, i_qty_available, i_qty_critical, i_qty_reorder, i_sold_this_month, i_sold_ytd, b_freeze_prices, i_core_cost, i_inv_value, memo);
            else refreshData = model.UpdateSKU(s_sku_name, i_category, s_description, s_measurement_unit, i_weight, i_cost_code, i_asset_acct, b_taxable, b_maintain_qty, b_allow_discount, b_commissionable, i_order_from, d_last_sold, s_manufacturer, s_location, i_quantity, i_qty_allocated, i_qty_available, i_qty_critical, i_qty_reorder, i_sold_this_month, i_sold_ytd, b_freeze_prices, i_core_cost, i_inv_value, memo, skuId);
            string modeText = skuId == 0 ? "creating" : "updating";
            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this._navigateToPrev(sender, e);
            }
            else MessageBox.Show("An Error occured while " + modeText + " the vendor.");
        }

        public class CategoryComboBoxItem
        {
            public int Id { get; set; }
            public string Text { get; set; }

            public CategoryComboBoxItem(int id, string text)
            {
                Id = id;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Do you want to save?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    saveSKU(sender, e);
                }
                else if (result == DialogResult.No)
                {
                    _navigateToPrev(sender, e);
                }
            }
        }
    }
}

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

namespace mjc_dev.forms.modals
{
    public partial class SkuDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

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
        private FInputBox priceTier1 = new FInputBox("Price Tier 1");
        private FInputBox priceTier2 = new FInputBox("Price Tier 2");




        private DashboardModel model = new DashboardModel();
        private int selectId = 0;
        private int skuId = 0;
        private int categoryId = 0;

        public SkuDetail() : base("Sku detail")
        {
            this.Text = "Sku detail";
            InitializeComponent();
            this.Size = new Size(1200, 930);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitForm();
            InitMBOKButton();

            this.Load += new System.EventHandler(this.Add_Load);
        }

        private void InitForm()
        {
            {
                SKUInfo.SetPosition(new Point(30, 30));
                this.Controls.Add(SKUInfo.GetLabel());

                SKUName.SetPosition(new Point(30, 80));
                this.Controls.Add(SKUName.GetLabel());
                this.Controls.Add(SKUName.GetTextBox());

                categoryCombo.SetPosition(new Point(30, 130));
                this.Controls.Add(categoryCombo.GetLabel());
                this.Controls.Add(categoryCombo.GetComboBox());

                description.SetPosition(new Point(30, 180));
                this.Controls.Add(description.GetLabel());
                this.Controls.Add(description.GetTextBox());

                measurementUnit.SetPosition(new Point(30, 230));
                this.Controls.Add(measurementUnit.GetLabel());
                this.Controls.Add(measurementUnit.GetTextBox());

                weight.SetPosition(new Point(30, 280));
                this.Controls.Add(weight.GetLabel());
                this.Controls.Add(weight.GetTextBox());

                costCode.SetPosition(new Point(30, 330));
                this.Controls.Add(costCode.GetLabel());
                this.Controls.Add(costCode.GetTextBox());

                assetAcct.SetPosition(new Point(30, 380));
                this.Controls.Add(assetAcct.GetLabel());
                this.Controls.Add(assetAcct.GetTextBox());

                //checkbox
                taxable.SetPosition(new Point(30, 430));
                this.Controls.Add(taxable.GetCheckBox());
                //checkbox
                maintainQtys.SetPosition(new Point(220, 430));
                this.Controls.Add(maintainQtys.GetCheckBox());
                //checkbox
                allowDiscount.SetPosition(new Point(30, 480));
                this.Controls.Add(allowDiscount.GetCheckBox());
                //checkbox
                commissionable.SetPosition(new Point(220, 480));
                this.Controls.Add(commissionable.GetCheckBox());

                orderForm.SetPosition(new Point(30, 530));
                this.Controls.Add(orderForm.GetLabel());
                this.Controls.Add(orderForm.GetTextBox());

                lastSold.SetPosition(new Point(30, 580));
                this.Controls.Add(lastSold.GetLabel());
                this.Controls.Add(lastSold.GetDateTimePicker());

                manufacturer.SetPosition(new Point(30, 630));
                this.Controls.Add(manufacturer.GetLabel());
                this.Controls.Add(manufacturer.GetTextBox());

                location.SetPosition(new Point(30, 680));
                this.Controls.Add(location.GetLabel());
                this.Controls.Add(location.GetTextBox());
            }
            {
                quantityTracking.SetPosition(new Point(630, 30));
                this.Controls.Add(quantityTracking.GetLabel());

                quantity.SetPosition(new Point(630, 80));
                this.Controls.Add(quantity.GetLabel());
                this.Controls.Add(quantity.GetTextBox());

                qtyAllocated.SetPosition(new Point(630, 130));
                this.Controls.Add(qtyAllocated.GetLabel());
                this.Controls.Add(qtyAllocated.GetTextBox());

                qtyAvaiable.SetPosition(new Point(630, 180));
                this.Controls.Add(qtyAvaiable.GetLabel());
                this.Controls.Add(qtyAvaiable.GetTextBox());

                criticalQty.SetPosition(new Point(630, 230));
                this.Controls.Add(criticalQty.GetLabel());
                this.Controls.Add(criticalQty.GetTextBox());

                recorderQty.SetPosition(new Point(630, 280));
                this.Controls.Add(recorderQty.GetLabel());
                this.Controls.Add(recorderQty.GetTextBox());

                sales.SetPosition(new Point(630, 330));
                this.Controls.Add(sales.GetLabel());

                soldThisMonth.SetPosition(new Point(630, 380));
                this.Controls.Add(soldThisMonth.GetLabel());
                this.Controls.Add(soldThisMonth.GetTextBox());

                soldYTD.SetPosition(new Point(630, 430));
                this.Controls.Add(soldYTD.GetTextBox());
                this.Controls.Add(soldYTD.GetLabel());

                prices.SetPosition(new Point(630, 480));
                this.Controls.Add(prices.GetLabel());

                freezePrices.SetPosition(new Point(630, 530));
                this.Controls.Add(freezePrices.GetCheckBox());

                coreCost.SetPosition(new Point(630, 580));
                this.Controls.Add(coreCost.GetLabel());
                this.Controls.Add(coreCost.GetTextBox());

                invValue.SetPosition(new Point(630, 630));
                this.Controls.Add(invValue.GetLabel());
                this.Controls.Add(invValue.GetTextBox());

                priceTier1.SetPosition(new Point(630, 680));
                this.Controls.Add(priceTier1.GetLabel());
                this.Controls.Add(priceTier1.GetTextBox());

                priceTier2.SetPosition(new Point(630, 730));
                this.Controls.Add(priceTier2.GetLabel());
                this.Controls.Add(priceTier2.GetTextBox());
            }
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(1025, this.Height - 120);
            MBOk_button.Click += (sender, e) => this.Add_SKUBUTTON(sender, e);
            this.Controls.Add(MBOk_button);
        }

        public void setDetails(List<dynamic> data, int id)
        {
            this.skuId = id;
            this.selectId = (int)data[0].category;

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
            this.priceTier1.GetTextBox().Text = "";
            this.priceTier2.GetTextBox().Text = "";
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

        private void Add_SKUBUTTON(object sender, EventArgs e)
        {
            string s_sku_name = SKUName.GetTextBox().Text;

            CategoryComboBoxItem seletedItem = (CategoryComboBoxItem)categoryCombo.GetComboBox().SelectedItem;
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
            int i_price_tier1; bool is_i_price_tier1 = int.TryParse(priceTier1.GetTextBox().Text, out i_price_tier1);
            if (!is_i_price_tier1) i_price_tier1 = 0;
            int i_price_tier2; bool is_i_price_tier2 = int.TryParse(priceTier2.GetTextBox().Text, out i_price_tier2);
            if (!is_i_price_tier2) i_price_tier2 = 0;

            if (s_sku_name == "")
            {
                MessageBox.Show("Please fill String field.");
                return;
            }
            bool refreshData = false;
            if (skuId == 0) refreshData = model.AddSKU(s_sku_name, i_category, s_description, s_measurement_unit, i_weight, i_cost_code, i_asset_acct, b_taxable, b_maintain_qty, b_allow_discount, b_commissionable, i_order_from, d_last_sold, s_manufacturer, s_location, i_quantity, i_qty_allocated, i_qty_available, i_qty_critical, i_qty_reorder, i_sold_this_month, i_sold_ytd, b_freeze_prices, i_core_cost, i_inv_value, i_price_tier1, i_price_tier2);
            else refreshData = model.UpdateSKU(s_sku_name, i_category, s_description, s_measurement_unit, i_weight, i_cost_code, i_asset_acct, b_taxable, b_maintain_qty, b_allow_discount, b_commissionable, i_order_from, d_last_sold, s_manufacturer, s_location, i_quantity, i_qty_allocated, i_qty_available, i_qty_critical, i_qty_reorder, i_sold_this_month, i_sold_ytd, b_freeze_prices, i_core_cost, i_inv_value, i_price_tier1, i_price_tier2, skuId);
            string modeText = skuId == 0 ? "creating" : "updating";
            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
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
    }
}

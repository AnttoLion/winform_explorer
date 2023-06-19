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
using static mjc_dev.forms.sku.SKUInformation;

namespace mjc_dev.forms.sku
{
    public partial class CrossRefDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

        private FComboBox SKUName = new FComboBox("SKU#");
        private FComboBox Vendor = new FComboBox("Vendor");
        private FInputBox CrossRef = new FInputBox("CrossReference");

        private int priceTierId;

        private SKUCrossRefModal SKUCrossRefModalObj = new SKUCrossRefModal();
        private SKUModel SKUModelObj = new SKUModel();
        private VendorsModel VendorsModelObj = new VendorsModel();

        private int SKUCRId = 0;

        public CrossRefDetail(int _skuId) : base("Add PriceTier")
        {
            InitializeComponent();
            this.Size = new Size(600, 310);
            this.StartPosition = FormStartPosition.CenterScreen;


            InitMBOKButton();
            InitInputBox();

            loadSKUCombo(_skuId);
            loadVendorCombo();

            this.Load += (s, e) => CrossRefDetail_Load(s, e);
        }

        public CrossRefDetail(int _skuId, int _id) : base("Add PriceTier")
        {
            InitializeComponent();
            this.Size = new Size(600, 310);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SKUCRId = _id;

            InitMBOKButton();
            InitInputBox();

            loadSKUCombo();
            loadVendorCombo();

            loadCrossRef(_id);

            this.Load += (s, e) => CrossRefDetail_Load(s, e);
        }

        private void CrossRefDetail_Load(object sender, EventArgs e)
        {
            Vendor.GetComboBox().Select();
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(425, 200);
            MBOk_button.Click += (sender, e) => {
                saveCrossRef();
            };
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            SKUName.SetPosition(new Point(30, 30));
            this.Controls.Add(SKUName.GetLabel());
            this.Controls.Add(SKUName.GetComboBox());
            SKUName.GetComboBox().Enabled = false;

            Vendor.SetPosition(new Point(30, 80));
            this.Controls.Add(Vendor.GetLabel());
            this.Controls.Add(Vendor.GetComboBox());
            Vendor.GetComboBox().KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift))
                {
                    SelectNextControl((Control)s, false, true, true, true);
                    e.Handled = true;
                }
            };

            CrossRef.SetPosition(new Point(30, 130));
            this.Controls.Add(CrossRef.GetLabel());
            this.Controls.Add(CrossRef.GetTextBox());
        }

        private void loadSKUCombo(int SKUId = 0) {
            bool initFlag = true;
            SKUName.GetComboBox().Items.Clear();
            List<KeyValuePair<int, string>> SKUNameList = new List<KeyValuePair<int, string>>();
            SKUNameList = SKUModelObj.GetSKUItems();
            foreach (KeyValuePair<int, string> item in SKUNameList)
            {
                int id = item.Key;
                string name = item.Value;
                
                if (initFlag && SKUId == 0)
                {
                    SKUId = id;
                    initFlag = false;
                }
                
                SKUName.GetComboBox().Items.Add(new FComboBoxItem(id, name));
            }

            foreach (FComboBoxItem item in SKUName.GetComboBox().Items)
            {
                if (item.Id == SKUId)
                {
                    SKUName.GetComboBox().SelectedItem = item;
                    break;
                }
            }
        }

        private void loadVendorCombo()
        {
            Vendor.GetComboBox().Items.Clear();
            List<KeyValuePair<int, string>> VendorList = new List<KeyValuePair<int, string>>();
            VendorList = VendorsModelObj.GetVendorItems();
            foreach (KeyValuePair<int, string> item in VendorList)
            {
                int id = item.Key;
                string name = item.Value;

                Vendor.GetComboBox().Items.Add(new FComboBoxItem(id, name));
            }
        }

        private void loadCrossRef(int id) {
            List<dynamic> skuCrossRefData = new List<dynamic>();
            skuCrossRefData = SKUCrossRefModalObj.GetSKUCrossRef(id);


            foreach (FComboBoxItem item in Vendor.GetComboBox().Items)
            {
                if (item.Id == skuCrossRefData[0].vendorId)
                {
                    Vendor.GetComboBox().SelectedItem = item;
                    break;
                }
            }

            CrossRef.GetTextBox().Text = skuCrossRefData[0].crossReference;
        }


        private void saveCrossRef()
        {
            if (SKUName.GetComboBox().SelectedItem == null)
            {
                MessageBox.Show("please select a category");
                return;
            }
            FComboBoxItem SKUItem = (FComboBoxItem)SKUName.GetComboBox().SelectedItem;
            int sku_id = SKUItem.Id;

            if (Vendor.GetComboBox().SelectedItem == null)
            {
                MessageBox.Show("please select a category");
                return;
            }
            FComboBoxItem VendorItem = (FComboBoxItem)Vendor.GetComboBox().SelectedItem;
            int vendor_id = VendorItem.Id;

            string cross_ref = CrossRef.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(cross_ref))
            {
                MessageBox.Show("please enter SKU Name");
                this.CrossRef.GetTextBox().Select();
                return;
            }

            bool refreshData = false;

            if (SKUCRId == 0)
                refreshData = SKUCrossRefModalObj.AddSKUCrossRef(sku_id, vendor_id, cross_ref, this._accountId);
            else refreshData = SKUCrossRefModalObj.UpdateSKUCrossRef(SKUCRId, sku_id, vendor_id, cross_ref, this._accountId);

            string modeText = SKUCRId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the pricetier.");

        }
    }
}

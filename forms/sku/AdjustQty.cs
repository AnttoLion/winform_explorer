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

namespace mjc_dev.forms.sku
{
    public partial class AdjustQty : BasicModal
    {
        private ModalButton MBSave = new ModalButton("F1 Save New Qty", Keys.F1);
        private Button MBSave_button;

        private FInputBox SKUName = new FInputBox("SKU#");
        private FInputBox CurrentQty = new FInputBox("CurrentQty");
        private FInputBox NewQty = new FInputBox("New Qty");

        private SKUModel SKUModelObj = new SKUModel();

        private int SKUId = 0;

        public AdjustQty(int _skuId, string _skuName) : base("AdjustQty")
        {
            InitializeComponent();
            //_setModalStyle2();

            this.Size = new Size(360, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SKUId = _skuId;

            InitButton();
            InitInputBox(_skuId, _skuName);

            this.Load += (s, e) => AdjustQty_Load(s, e);
        }

        private void AdjustQty_Load(object sender, EventArgs e)
        {
            NewQty.GetTextBox().Select();
        }

        private void InitButton()
        {
            ModalButton_HotKeyDown(MBSave);
            MBSave_button = MBSave.GetButton();
            MBSave_button.Location = new Point(140, 190);
            MBSave_button.Click += (sender, e) => {
                MessageBox.Show("");
                saveNewQty();
            };
            this.Controls.Add(MBSave_button);
        }

        private void InitInputBox(int _skuId, string _skuName)
        {
            SKUName.SetPosition(new Point(30, 30));
            SKUName.GetLabel().Text = _skuName;
            this.Controls.Add(SKUName.GetLabel());

            int quantity = SKUModelObj.GetQuantity(_skuId);

            CurrentQty.SetPosition(new Point(30, 75));
            CurrentQty.GetLabel().Text = "Adjust Qty:  " + quantity.ToString();
            this.Controls.Add(CurrentQty.GetLabel());

            NewQty.SetPosition(new Point(30, 120));
            this.Controls.Add(NewQty.GetLabel());
            this.Controls.Add(NewQty.GetTextBox());
            NewQty.GetTextBox().Location = new Point(140, 120);
            NewQty.GetTextBox().Width = 160;
        }

        private void saveNewQty()
        {
            string cross_ref = NewQty.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(cross_ref))
            {
                MessageBox.Show("Please enter SKU Name");
                NewQty.GetTextBox().Select();
                return;
            }

            int newQty;
            if (!int.TryParse(cross_ref, out newQty))
            {
                MessageBox.Show("Please enter a valid integer quantity");
                NewQty.GetTextBox().Select();
                return;
            }

            SKUModelObj.UpdateQuantity(newQty, this.SKUId);
            this.Close();
        }
    }
}

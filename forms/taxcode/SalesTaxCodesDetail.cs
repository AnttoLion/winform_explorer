using mjc_dev.common;
using mjc_dev.common.components;
using mjc_dev.forms.price;
using mjc_dev.forms.salescostcode;
using mjc_dev.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace mjc_dev.forms.taxcode
{
    public partial class SalesTaxCodesDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;
        private FInputBox Name = new FInputBox("Name");
        private FInputBox Classification = new FInputBox("Classification");
        private FInputBox Rate = new FInputBox("Rate");

        private int salesTaxCodeId = 0;
        private SalesTaxCodeModel SalesTaxCodeModelObj = new SalesTaxCodeModel();

        public SalesTaxCodesDetail() : base("Add SalesTaxCode")
        {
            InitializeComponent();

            this.Size = new Size(600, 320);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitMBOKButton();
            InitInputBox();

            this.Load += (s, e) => SalesTaxCodeDetail_Load(s, e);
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(425, 220);
            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            Name.SetPosition(new Point(30, 30));
            this.Controls.Add(Name.GetLabel());
            this.Controls.Add(Name.GetTextBox());

            Classification.SetPosition(new Point(30, 80));
            this.Controls.Add(Classification.GetLabel());
            this.Controls.Add(Classification.GetTextBox());

            Rate.SetPosition(new Point(30, 130));
            this.Controls.Add(Rate.GetLabel());
            this.Controls.Add(Rate.GetTextBox());
        }
        private void SalesTaxCodeDetail_Load(object sender, EventArgs e)
        {
            Name.GetTextBox().Select();
        }

        public void setDetails(int _id)
        {
            salesTaxCodeId = _id;
            var salesTaxCodeData = SalesTaxCodeModelObj.GetSalesTaxCodeData(_id);

            this.Name.GetTextBox().Text = salesTaxCodeData.name;
            this.Classification.GetTextBox().Text = salesTaxCodeData.classification;
            this.Rate.GetTextBox().Text = salesTaxCodeData.rate.ToString();
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            string name = this.Name.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("please enter Name.");
                this.Name.GetTextBox().Select();
                return;
            }
            string classification = this.Classification.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(classification))
            {
                MessageBox.Show("please enter Classification");
                this.Classification.GetTextBox().Select();
                return;
            }
            bool convertRate =  double.TryParse(this.Rate.GetTextBox().Text, out double rate);
            if (!convertRate)
            {
                MessageBox.Show("please enter correct rate");
                this.Rate.GetTextBox().Select();
                return;
            }

            bool refreshData = false;
            if (salesTaxCodeId == 0)
                refreshData = SalesTaxCodeModelObj.AddSalesTaxCode(name, classification, rate);
            else refreshData = SalesTaxCodeModelObj.UpdateSalesTaxCode(name, classification, rate, salesTaxCodeId);

            string modeText = salesTaxCodeId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the salescostcode.");
        }
    }
}

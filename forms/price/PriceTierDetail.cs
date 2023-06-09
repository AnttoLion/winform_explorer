using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.common;
using mjc_dev.common.components;
using mjc_dev.model;

namespace mjc_dev.forms.price
{
    public partial class PriceTierDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

        private FInputBox priceTierName = new FInputBox("Name");
        private FInputBox profitMargin = new FInputBox("profit margin");
        private FInputBox priceTierCode = new FInputBox("price tier code");

        private int priceTierId;
        private DashboardModel model = new DashboardModel();

        public PriceTierDetail() : base("Add PriceTier")
        {
            InitializeComponent();
            this.Size = new Size(600, 310);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitMBOKButton();
            InitInputBox();
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(425, 200);
            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            priceTierName.SetPosition(new Point(30, 30));
            this.Controls.Add(priceTierName.GetLabel());
            this.Controls.Add(priceTierName.GetTextBox());

            profitMargin.SetPosition(new Point(30, 80));
            this.Controls.Add(profitMargin.GetLabel());
            this.Controls.Add(profitMargin.GetTextBox());

            priceTierCode.SetPosition(new Point(30, 130));
            this.Controls.Add(priceTierCode.GetLabel());
            this.Controls.Add(priceTierCode.GetTextBox());
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            string name = this.priceTierName.GetTextBox().Text;
            double profitMargin = double.Parse(this.profitMargin.GetTextBox().Text);
            string pricetiercode = this.priceTierCode.GetTextBox().Text;

            bool refreshData = false;

            if (priceTierId == 0)
                refreshData = model.AddPriceTier(name, profitMargin, pricetiercode);
            else refreshData = model.UpdatePriceTier(name, profitMargin, pricetiercode, priceTierId);

            string modeText = priceTierId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the pricetier.");
        }
        public void setDetails(string name, double profitmargin, string pricetiercode, int pricetierId)
        {
            this.priceTierName.GetTextBox().Text = name;
            this.profitMargin.GetTextBox().Text = profitmargin.ToString();
            this.priceTierCode.GetTextBox().Text = pricetiercode;
            priceTierId = pricetierId;
        }
    }
}

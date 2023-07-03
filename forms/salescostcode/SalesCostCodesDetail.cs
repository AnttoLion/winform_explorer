using mjc_dev.common;
using mjc_dev.common.components;
using mjc_dev.forms.price;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace mjc_dev.forms.salescostcode
{
    public partial class SalesCostCodesDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

        private FInputBox ScCode = new FInputBox("Sc Code");
        private FInputBox SalesAccount = new FInputBox("Sales Account");
        private FInputBox CostAccount = new FInputBox("Cost Account");
        private FInputBox Title = new FInputBox("Title");

        private int salesCostCodeId = 0;
        private SalesCostCodeModel SalesCostCodeModelObj = new SalesCostCodeModel();

        public SalesCostCodesDetail() : base("Add SalesCostCode")
        {
            InitializeComponent();

            this.Size = new Size(600, 360);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitMBOKButton();
            InitInputBox();

            this.Load += (s, e) => SalesCostCodeDetail_Load(s, e);
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(425, 250);
            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            ScCode.SetPosition(new Point(30, 30));
            this.Controls.Add(ScCode.GetLabel());
            this.Controls.Add(ScCode.GetTextBox());

            SalesAccount.SetPosition(new Point(30, 80));
            this.Controls.Add(SalesAccount.GetLabel());
            this.Controls.Add(SalesAccount.GetTextBox());

            CostAccount.SetPosition(new Point(30, 130));
            this.Controls.Add(CostAccount.GetLabel());
            this.Controls.Add(CostAccount.GetTextBox());

            Title.SetPosition(new Point(30, 180));
            this.Controls.Add(Title.GetLabel());
            this.Controls.Add(Title.GetTextBox());
        }

        public void setDetails(int _id)
        {
            salesCostCodeId = _id;
            var salesCostCodeData = SalesCostCodeModelObj.GetSalesCostCodeData(_id);

            this.ScCode.GetTextBox().Text = salesCostCodeData.scCode;
            this.SalesAccount.GetTextBox().Text = salesCostCodeData.salesAccount;
            this.CostAccount.GetTextBox().Text = salesCostCodeData.costAccount;
            this.Title.GetTextBox().Text = salesCostCodeData.title;
        }

        private void SalesCostCodeDetail_Load(object sender, EventArgs e)
        {
            ScCode.GetTextBox().Select();
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            string scCode = this.ScCode.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(scCode))
            {
                MessageBox.Show("please enter SC Code.");
                this.ScCode.GetTextBox().Select();
                return;
            }
            if (scCode.Length>2)
            {
                MessageBox.Show("The length of the SC Code must be less than 3 characters.");
                this.ScCode.GetTextBox().Select();
                return;
            }
            string salesAccount = this.SalesAccount.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(salesAccount))
            {
                MessageBox.Show("please enter Sales Account");
                this.SalesAccount.GetTextBox().Select();
                return;
            }
            string costAccount = this.CostAccount.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(costAccount))
            {
                MessageBox.Show("please enter Cost Account");
                this.CostAccount.GetTextBox().Select();
                return;
            }
            string title = this.Title.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("please enter Title");
                this.Title.GetTextBox().Select();
                return;
            }

            bool refreshData = false;
            if (salesCostCodeId == 0)
                refreshData = SalesCostCodeModelObj.AddSalesCostCode(scCode, salesAccount, costAccount, title);
            else refreshData = SalesCostCodeModelObj.UpdateSalesCostCode(scCode, salesAccount, costAccount, title, salesCostCodeId);

            string modeText = salesCostCodeId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the salescostcode.");
        }
    }
}

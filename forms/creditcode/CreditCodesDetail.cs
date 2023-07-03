using mjc_dev.common;
using mjc_dev.common.components;
using mjc_dev.forms.price;
using mjc_dev.forms.salescostcode;
using mjc_dev.forms.taxcode;
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

namespace mjc_dev.forms.creditcode
{
    public partial class CreditCodesDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;
        private FInputBox CreditCode = new FInputBox("Credit Code");
        private FInputBox PaymentAllowed = new FInputBox("Payment Allowed");
        private FInputBox CreditLimit = new FInputBox("Credit Limit");

        private int creditCodeId = 0;
        private CreditCodeModel CreditCodeModelObj = new CreditCodeModel();
        public CreditCodesDetail() : base("Add CreditCode")
        {
            InitializeComponent();

            this.Size = new Size(600, 320);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitMBOKButton();
            InitInputBox();

            this.Load += (s, e) => CreditCodeDetail_Load(s, e);
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
            CreditCode.SetPosition(new Point(30, 30));
            this.Controls.Add(CreditCode.GetLabel());
            this.Controls.Add(CreditCode.GetTextBox());

            PaymentAllowed.SetPosition(new Point(30, 80));
            this.Controls.Add(PaymentAllowed.GetLabel());
            this.Controls.Add(PaymentAllowed.GetTextBox());

            CreditLimit.SetPosition(new Point(30, 130));
            this.Controls.Add(CreditLimit.GetLabel());
            this.Controls.Add(CreditLimit.GetTextBox());
        }
        private void CreditCodeDetail_Load(object sender, EventArgs e)
        {
            CreditCode.GetTextBox().Select();
        }
        public void setDetails(int _id)
        {
            creditCodeId = _id;
            var creditCodeData = CreditCodeModelObj.GetCreditCodeData(_id);

            this.CreditCode.GetTextBox().Text = creditCodeData.code;
            this.PaymentAllowed.GetTextBox().Text = creditCodeData.paymentAllowed;
            this.CreditLimit.GetTextBox().Text = creditCodeData.creditLimit;
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            string creditCode = this.CreditCode.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(creditCode))
            {
                MessageBox.Show("please enter Credit Code.");
                this.CreditCode.GetTextBox().Select();
                return;
            }
            if (creditCode.Length > 3)
            {
                MessageBox.Show("The length of the Credit Code must be less than 4 characters.");
                this.CreditCode.GetTextBox().Select();
                return;
            }
            string paymentAllowed = this.PaymentAllowed.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(paymentAllowed))
            {
                MessageBox.Show("please enter Payment Allowed");
                this.PaymentAllowed.GetTextBox().Select();
                return;
            }
            string creditLimit = this.CreditLimit.GetTextBox().Text;
            if (string.IsNullOrWhiteSpace(creditLimit))
            {
                MessageBox.Show("please enter Credit Limit");
                this.CreditLimit.GetTextBox().Select();
                return;
            }

            bool refreshData = false;
            if (creditCodeId == 0)
                refreshData = CreditCodeModelObj.AddCreditCode(creditCode, paymentAllowed, creditLimit);
            else refreshData = CreditCodeModelObj.UpdateCreditCode(creditCode, paymentAllowed, creditLimit, creditCodeId);

            string modeText = creditCodeId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the credit Code.");
        }
    }
}

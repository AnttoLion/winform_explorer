using mjc_dev.common;
using mjc_dev.common.components;
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

namespace mjc_dev.forms.customer
{
    public partial class CustomerDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

        private FGroupLabel CustomerInfo = new FGroupLabel("CustomerInfo");
        private FInputBox CustomerNum = new FInputBox("Cust #");
        private FInputBox CustomerName = new FInputBox("Customer Name");
        private FInputBox AddressLine1 = new FInputBox("Address Line 1");
        private FInputBox AddressLine2 = new FInputBox("Address Line 2");
        private FInputBox City = new FInputBox("City");
        private FInputBox State = new FInputBox("State");
        private FInputBox Zip = new FInputBox("Zip");
        private FInputBox BusPhone = new FInputBox("Bus.Phone");
        private FInputBox EMail = new FInputBox("E-mail");
        private FInputBox Fax = new FInputBox("Fax");
        private FDateTime DateOpened = new FDateTime("Date opened");
        private FInputBox Salesman = new FInputBox("Salesman");
        private FInputBox Resale = new FInputBox("Resale");
        private FInputBox StmtCust = new FInputBox("Stmt Cust#");
        private FInputBox StmtName = new FInputBox("        Name");
        private FComboBox PriceTier = new FComboBox("Price Tier");
        private FInputBox Terms = new FInputBox("Terms");
        private FInputBox Limit = new FInputBox("Limit");
        private FInputBox Memo = new FInputBox("Memo");

        private FCheckBox Taxable = new FCheckBox("Taxable");
        private FCheckBox SendStatements = new FCheckBox("Send Statements");
        private FInputBox CoreTracking = new FInputBox("Core Tracking");
        private FInputBox CoreBalance = new FInputBox("Core Balance");
        private FInputBox PrintCoreTot = new FInputBox("Print Core Tot");
        private FInputBox AccountType = new FInputBox("Account Type");
        private FInputBox PORequired = new FInputBox("PO# Required");
        private FInputBox CreditCode = new FInputBox("Credit Code");
        private FInputBox InterestRate = new FInputBox("Interest Rate");
        private FInputBox AcctBalance = new FInputBox("Acct Balance");
        private FInputBox YTDPurchases = new FInputBox("YTDPurchases");
        private FInputBox YTDInterest = new FInputBox("YTDInterest");
        private FDateTime DateLastPurch = new FDateTime("DateLastPurch");

        private CustomersModel CustomersModelObj = new CustomersModel();
        private PriceTiersModel PriceTiersModelObj = new PriceTiersModel();

        private int customerId;
        private int selectId = 0;

        public CustomerDetail()
        {
            InitializeComponent();
            this.Size = new Size(1200, 950);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new System.EventHandler(this.CustomerDetail_Load);
            InitMBOKButton();
            InitInputBox();
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(1025, this.Height - 115);
            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            {/*
                CustomerInfo.SetPosition(new Point(30, 30));
                this.Controls.Add(CustomerInfo.GetLabel());

                CustomerNum.SetPosition(new Point(30, 80));
                this.Controls.Add(CustomerNum.GetLabel());
                this.Controls.Add(CustomerNum.GetTextBox());

                AddressLine1.SetPosition(new Point(30, 130));
                this.Controls.Add(AddressLine1.GetLabel());
                this.Controls.Add(AddressLine1.GetTextBox());

                AddressLine2.SetPosition(new Point(30, 180));
                this.Controls.Add(AddressLine2.GetLabel());
                this.Controls.Add(AddressLine2.GetTextBox());

                City.SetPosition(new Point(30, 230));
                this.Controls.Add(City.GetLabel());
                this.Controls.Add(City.GetTextBox());

                State.SetPosition(new Point(30, 280));
                this.Controls.Add(State.GetLabel());
                this.Controls.Add(State.GetTextBox());

                Zip.SetPosition(new Point(30, 330));
                this.Controls.Add(Zip.GetLabel());
                this.Controls.Add(Zip.GetTextBox());

                BusPhone.SetPosition(new Point(30, 380));
                this.Controls.Add(BusPhone.GetLabel());
                this.Controls.Add(BusPhone.GetTextBox());

                EMail.SetPosition(new Point(30, 430));
                this.Controls.Add(EMail.GetLabel());
                this.Controls.Add(EMail.GetTextBox());

                DateOpened.SetPosition(new Point(30, 480));
                this.Controls.Add(DateOpened.GetLabel());
                this.Controls.Add(DateOpened.GetTextBox());

                Salesman.SetPosition(new Point(30, 530));
                this.Controls.Add(Salesman.GetLabel());
                this.Controls.Add(Salesman.GetTextBox());

                Resale.SetPosition(new Point(30, 580));
                this.Controls.Add(Resale.GetLabel());
                this.Controls.Add(Resale.GetTextBox());

                StmtCust.SetPosition(new Point(30, 630));
                this.Controls.Add(StmtCust.GetLabel());
                this.Controls.Add(StmtCust.GetTextBox());

                StmtName.SetPosition(new Point(30, 680));
                this.Controls.Add(StmtName.GetLabel());
                this.Controls.Add(StmtName.GetTextBox());

                PriceTier.SetPosition(new Point(30, 730));
                this.Controls.Add(PriceTier.GetLabel());
                this.Controls.Add(PriceTier.GetTextBox());

                Terms.SetPosition(new Point(30, 780));
                this.Controls.Add(Terms.GetLabel());
                this.Controls.Add(Terms.GetTextBox());

                Limit.SetPosition(new Point(30, 830));
                this.Controls.Add(Limit.GetLabel());
                this.Controls.Add(Limit.GetTextBox());

                Memo.SetPosition(new Point(30, 880));
                this.Controls.Add(Memo.GetLabel());
                this.Controls.Add(Memo.GetTextBox());*/
            }
            {
                CustomerNum.SetPosition(new Point(30, 30));
                this.Controls.Add(CustomerNum.GetLabel());
                this.Controls.Add(CustomerNum.GetTextBox());

                CustomerName.SetPosition(new Point(30, 80));
                this.Controls.Add(CustomerName.GetLabel());
                this.Controls.Add(CustomerName.GetTextBox());

                AddressLine1.SetPosition(new Point(30, 130));
                this.Controls.Add(AddressLine1.GetLabel());
                this.Controls.Add(AddressLine1.GetTextBox());

                AddressLine2.SetPosition(new Point(30, 180));
                this.Controls.Add(AddressLine2.GetLabel());
                this.Controls.Add(AddressLine2.GetTextBox());

                City.SetPosition(new Point(30, 230));
                this.Controls.Add(City.GetLabel());
                this.Controls.Add(City.GetTextBox());

                State.SetPosition(new Point(30, 280));
                this.Controls.Add(State.GetLabel());
                this.Controls.Add(State.GetTextBox());

                Zip.SetPosition(new Point(30, 330));
                this.Controls.Add(Zip.GetLabel());
                this.Controls.Add(Zip.GetTextBox());

                BusPhone.SetPosition(new Point(30, 380));
                this.Controls.Add(BusPhone.GetLabel());
                this.Controls.Add(BusPhone.GetTextBox());

                EMail.SetPosition(new Point(30, 430));
                this.Controls.Add(EMail.GetLabel());
                this.Controls.Add(EMail.GetTextBox());

                Fax.SetPosition(new Point(30, 480));
                this.Controls.Add(Fax.GetLabel());
                this.Controls.Add(Fax.GetTextBox());

                DateOpened.SetPosition(new Point(30, 530));
                this.Controls.Add(DateOpened.GetLabel());
                this.Controls.Add(DateOpened.GetDateTimePicker());

                Salesman.SetPosition(new Point(30, 580));
                this.Controls.Add(Salesman.GetLabel());
                this.Controls.Add(Salesman.GetTextBox());

                Resale.SetPosition(new Point(30, 630));
                this.Controls.Add(Resale.GetLabel());
                this.Controls.Add(Resale.GetTextBox());

                StmtCust.SetPosition(new Point(30, 680));
                this.Controls.Add(StmtCust.GetLabel());
                this.Controls.Add(StmtCust.GetTextBox());

                StmtName.SetPosition(new Point(30, 730));
                this.Controls.Add(StmtName.GetLabel());
                this.Controls.Add(StmtName.GetTextBox());

                PriceTier.SetPosition(new Point(30, 780));
                this.Controls.Add(PriceTier.GetLabel());
                this.Controls.Add(PriceTier.GetComboBox());

                Terms.SetPosition(new Point(30, 830));
                this.Controls.Add(Terms.GetLabel());
                this.Controls.Add(Terms.GetTextBox());
            }
            {
                Limit.SetPosition(new Point(630, 30));
                this.Controls.Add(Limit.GetLabel());
                this.Controls.Add(Limit.GetTextBox());

                Memo.SetPosition(new Point(630, 80));
                this.Controls.Add(Memo.GetLabel());
                this.Controls.Add(Memo.GetTextBox());

                Taxable.SetPosition(new Point(630, 130));
                this.Controls.Add(Taxable.GetCheckBox());

                SendStatements.SetPosition(new Point(830, 130));
                this.Controls.Add(SendStatements.GetCheckBox());

                CoreTracking.SetPosition(new Point(630, 180));
                this.Controls.Add(CoreTracking.GetLabel());
                this.Controls.Add(CoreTracking.GetTextBox());

                CoreBalance.SetPosition(new Point(630, 230));
                this.Controls.Add(CoreBalance.GetLabel());
                this.Controls.Add(CoreBalance.GetTextBox());

                PrintCoreTot.SetPosition(new Point(630, 280));
                this.Controls.Add(PrintCoreTot.GetLabel());
                this.Controls.Add(PrintCoreTot.GetTextBox());

                AccountType.SetPosition(new Point(630, 330));
                this.Controls.Add(AccountType.GetLabel());
                this.Controls.Add(AccountType.GetTextBox());

                PORequired.SetPosition(new Point(630, 380));
                this.Controls.Add(PORequired.GetLabel());
                this.Controls.Add(PORequired.GetTextBox());

                CreditCode.SetPosition(new Point(630, 430));
                this.Controls.Add(CreditCode.GetLabel());
                this.Controls.Add(CreditCode.GetTextBox());

                InterestRate.SetPosition(new Point(630, 480));
                this.Controls.Add(InterestRate.GetLabel());
                this.Controls.Add(InterestRate.GetTextBox());

                AcctBalance.SetPosition(new Point(630, 530));
                this.Controls.Add(AcctBalance.GetLabel());
                this.Controls.Add(AcctBalance.GetTextBox());

                YTDPurchases.SetPosition(new Point(630, 580));
                this.Controls.Add(YTDPurchases.GetLabel());
                this.Controls.Add(YTDPurchases.GetTextBox());

                YTDInterest.SetPosition(new Point(630, 630));
                this.Controls.Add(YTDInterest.GetLabel());
                this.Controls.Add(YTDInterest.GetTextBox());

                DateLastPurch.SetPosition(new Point(630, 680));
                this.Controls.Add(DateLastPurch.GetLabel());
                this.Controls.Add(DateLastPurch.GetDateTimePicker());
            }
        }

        public void setDetails(List<dynamic> data, int id)
        {
            this.customerId = id;

            this.CustomerNum.GetTextBox().Text = data[0].customerNumber.ToString();
            this.CustomerNum.GetTextBox().Text = data[0].customerName.ToString();
            this.AddressLine1.GetTextBox().Text = data[0].address1.ToString();
            this.AddressLine2.GetTextBox().Text = data[0].address2.ToString();
            this.City.GetTextBox().Text = data[0].city.ToString();
            this.State.GetTextBox().Text = data[0].state.ToString();
            this.Zip.GetTextBox().Text = data[0].zipcode.ToString();
            this.BusPhone.GetTextBox().Text = data[0].businessPhone.ToString();
            this.Fax.GetTextBox().Text = data[0].fax.ToString();
            this.EMail.GetTextBox().Text = data[0].email.ToString();
            this.DateOpened.GetDateTimePicker().Value = data[0].dateOpened.ToLocalTime();
            this.Salesman.GetTextBox().Text = data[0].salesman.ToString();
            this.Resale.GetTextBox().Text = data[0].resale.ToString();
            this.StmtCust.GetTextBox().Text = data[0].statementCustomerNumber.ToString();
            this.StmtName.GetTextBox().Text = data[0].statementName.ToString();
            //            this.pricetimer_textbox.Text = data[0].priceTierId.ToString();
            selectId = (int)data[0].priceTierId;

            this.Terms.GetTextBox().Text = data[0].terms.ToString();
            this.Limit.GetTextBox().Text = data[0].limit.ToString();
            this.Memo.GetTextBox().Text = data[0].memo.ToString();
            this.Taxable.GetCheckBox().Checked = data[0].taxable.ToString() == "True" ? true : false;
            this.SendStatements.GetCheckBox().Checked = data[0].sendStatements.ToString() == "True" ? true : false;

            this.CoreTracking.GetTextBox().Text = data[0].coreTracking.ToString();
            this.CoreBalance.GetTextBox().Text = data[0].coreBalance.ToString();
            this.PrintCoreTot.GetTextBox().Text = data[0].priceCoreTotal.ToString();
            this.AccountType.GetTextBox().Text = data[0].accountType.ToString();
            this.PORequired.GetTextBox().Text = data[0].poRequired.ToString();
            this.CreditCode.GetTextBox().Text = data[0].creditCodeId.ToString();
            this.InterestRate.GetTextBox().Text = data[0].interestRate.ToString();
            this.AcctBalance.GetTextBox().Text = data[0].accountBalance.ToString();
            this.YTDPurchases.GetTextBox().Text = data[0].yearToDatePurchases.ToString();
            this.YTDInterest.GetTextBox().Text = data[0].yearToDateInterest.ToString();
            this.DateLastPurch.GetDateTimePicker().Value = data[0].dateLastPurchased.ToLocalTime();

        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            string customer_num = CustomerNum.GetTextBox().Text;
            string customer_name = CustomerName.GetTextBox().Text;
            string address1 = AddressLine1.GetTextBox().Text;
            string address2 = AddressLine2.GetTextBox().Text;
            string city = City.GetTextBox().Text;
            string state = State.GetTextBox().Text;
            string zipcode = Zip.GetTextBox().Text;
            string business_phone = BusPhone.GetTextBox().Text;
            string fax = Fax.GetTextBox().Text;
            string email = EMail.GetTextBox().Text;

            DateTime date_opened = DateOpened.GetDateTimePicker().Value;

            string salesman = Salesman.GetTextBox().Text;

            string core_tracking = CoreTracking.GetTextBox().Text;
            string acct_type = AccountType.GetTextBox().Text;

            bool resale = Resale.GetTextBox().Text == "1" ? true : false;

            string stmt_num = StmtCust.GetTextBox().Text;
            string stmt_name = StmtName.GetTextBox().Text;

            if (PriceTier.GetComboBox().SelectedItem == null)
            {
                MessageBox.Show("please select PriceTier");
                //PriceTier.GetComboBox().Select();
                return;
            }
            PriceTierComboBoxItem selectedItem = (PriceTierComboBoxItem)PriceTier.GetComboBox().SelectedItem;
            int pricetier = selectedItem.Id;

            string terms = Terms.GetTextBox().Text;
            string limit = Limit.GetTextBox().Text;
            string memo = Memo.GetTextBox().Text;

            bool taxable = Taxable.GetCheckBox().Checked;
            bool send_stm = SendStatements.GetCheckBox().Checked;

            double core_balance; bool is_corebalance = double.TryParse(CoreBalance.GetTextBox().Text, out core_balance);
            if (!is_corebalance) core_balance = 0;

            bool print_core_tot = PrintCoreTot.GetTextBox().Text == "1" ? true : false;
            bool porequired = PORequired.GetTextBox().Text == "1" ? true : false;

            int credit_card; bool is_credit_card = int.TryParse(CreditCode.GetTextBox().Text, out credit_card);
            double acct_balance; bool is_acct_balance = double.TryParse(AcctBalance.GetTextBox().Text, out acct_balance);
            int ytd_purch; bool is_ytd_purch = int.TryParse(YTDPurchases.GetTextBox().Text, out ytd_purch);
            if (!is_credit_card) credit_card = 0;
            if (!is_acct_balance) acct_balance = 0;
            if (!is_ytd_purch) ytd_purch = 0;

            double interest_rate; bool is_interest_rate = double.TryParse(InterestRate.GetTextBox().Text, out interest_rate);
            double ytd_interest; bool is_ytd_interest = double.TryParse(YTDInterest.GetTextBox().Text, out ytd_interest);
            if (!is_interest_rate) interest_rate = 0;
            if (!is_ytd_interest) ytd_interest = 0;

            DateTime last_date_purch = DateLastPurch.GetDateTimePicker().Value;

            if (customer_num == "" || customer_name == "")
            {
                MessageBox.Show("Please fill String field.");
                return;
            }

            if (customer_num.Length > 2)
            {
                MessageBox.Show("Cust # Field mustn't limit 3.");
                return;
            }

            //            if (!is_pricetier || !is_corebalance || !is_credit_card || is_acct_balance || is_ytd_purch || is_ytd_purch || is_interest_rate || is_ytd_interest)
            bool refreshData = false;

            if (customerId == 0) refreshData = CustomersModelObj.AddCustomer(customer_num, customer_name, address1, address2, city, state, zipcode, business_phone, fax, email, date_opened, salesman, resale, stmt_num, stmt_name, pricetier, terms, limit, memo, taxable, send_stm, core_tracking, core_balance, print_core_tot, acct_type, porequired, credit_card, interest_rate, acct_balance, ytd_purch, ytd_interest, last_date_purch);
            else refreshData = CustomersModelObj.UpdateCustomer(customer_num, customer_name, address1, address2, city, state, zipcode, business_phone, fax, email, date_opened, salesman, resale, stmt_num, stmt_name, pricetier, terms, limit, memo, taxable, send_stm, core_tracking, core_balance, print_core_tot, acct_type, porequired, credit_card, interest_rate, acct_balance, ytd_purch, ytd_interest, last_date_purch, customerId);

            string modeText = customerId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the customer.");
        }

        private void CustomerDetail_Load(object sender, EventArgs e)
        {
            bool initFlag = true;
            PriceTier.GetComboBox().Items.Clear();
            List<KeyValuePair<int, string>> PriceTierList = new List<KeyValuePair<int, string>>();
            PriceTierList = PriceTiersModelObj.GetPriceTierItems();
            foreach (KeyValuePair<int, string> item in PriceTierList)
            {
                int id = item.Key;
                string name = item.Value;
                if (initFlag && selectId == 0)
                {
                    selectId = id;
                    initFlag = false;
                }
                PriceTier.GetComboBox().Items.Add(new PriceTierComboBoxItem(id, name));
            }

            foreach (PriceTierComboBoxItem item in PriceTier.GetComboBox().Items)
            {
                if (item.Id == selectId)
                {
                    PriceTier.GetComboBox().SelectedItem = item;
                    break;
                }
            }
        }
    }

    public class PriceTierComboBoxItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public PriceTierComboBoxItem(int id, string text)
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

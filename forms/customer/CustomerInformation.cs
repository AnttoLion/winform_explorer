using mjc_dev.common;
using mjc_dev.common.components;
using mjc_dev.forms.sku;
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
    public partial class CustomerInformation : GlobalLayout
    {
        private HotkeyButton hkCustomerMemo = new HotkeyButton("F2", "Customer Memo", Keys.F2);
        private HotkeyButton hkPriceLevels = new HotkeyButton("F3", "Price Levels", Keys.F3);
        private HotkeyButton hkShipToInfo = new HotkeyButton("F4", "Ship-to Info", Keys.F4);
        private HotkeyButton hkCreditCards = new HotkeyButton("F6", "Credit Cards", Keys.F6);
        private HotkeyButton hkSetArchived = new HotkeyButton("F9", "Set Archived", Keys.F9);

        private FGroupLabel CustomerInfo = new FGroupLabel("CustomerInfo");
        private FInputBox CustomerNum = new FInputBox("Cust #");
        private FInputBox CustomerName = new FInputBox("Customer Name");
        private FInputBox AddressLine1 = new FInputBox("Address Line 1");
        private FInputBox AddressLine2 = new FInputBox("Address Line 2");

        private FInputBox City = new FInputBox("City", 200);
        private FInputBox State = new FInputBox("State", 80);
        private FInputBox Zip = new FInputBox("Zip", 80);

        private FInputBox BusPhone = new FInputBox("Bus.Phone");
        private FInputBox EMail = new FInputBox("E-mail");
        private FInputBox Fax = new FInputBox("Fax", 100);
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
        private string memo = "";

        public CustomerInformation() : base("Customer Information", "Manage details of Customer")
        {
            this.Text = "Customer Detail";
            InitializeComponent();
            _initBasicSize();
            this.KeyDown += (s, e) => Form_KeyDown(s, e);

            HotkeyButton[] hkButtons = new HotkeyButton[5] { hkCustomerMemo, hkPriceLevels, hkShipToInfo, hkCreditCards, hkSetArchived };
            _initializeHKButtons(hkButtons, false);
            AddHotKeyEvents();

            this.Load += new System.EventHandler(this.CustomerDetail_Load);
//            InitMBOKButton();
            InitForms();
        }

        private void AddHotKeyEvents()
        {
            hkSetArchived.GetButton().Click += (sender, e) =>
            {
                DialogResult result = MessageBox.Show("Do you want to set archived?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CustomersModelObj.UpdateCustomerArchived(true, this.customerId);
                }
                else if (result == DialogResult.No)
                {
                    CustomersModelObj.UpdateCustomerArchived(false, this.customerId);
                }
            };
            hkCustomerMemo.GetButton().Click += (sender, e) =>
            {
                CustomerMemo MemoModal = new CustomerMemo(customerId, memo);
                this.Enabled = false;
                MemoModal.Show();
                MemoModal.FormClosed += (ss, sargs) =>
                {
                    this.memo = MemoModal.getMemo();
                    this.Enabled = true;
                };

            };
        }

        private void InitMBOKButton()
        {
//            ModalButton_HotKeyDown(MBOk);
//            MBOk_button = MBOk.GetButton();
//            MBOk_button.Location = new Point(1025, this.Height - 115);
//            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
//            this.Controls.Add(MBOk_button);
        }

        private void InitForms()
        {
            Panel _panel = new System.Windows.Forms.Panel();
            _panel.BackColor = System.Drawing.Color.Transparent;
            _panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            _panel.Location = new System.Drawing.Point(0, 95);
            _panel.Size = new Size(this.Width - 15, this.Height - 340);
            _panel.AutoScroll = true;
            this.Controls.Add(_panel);

            List<dynamic> FormComponents = new List<dynamic>();

            FormComponents.Add(CustomerInfo);
            FormComponents.Add(CustomerNum);
            FormComponents.Add(CustomerName);
            FormComponents.Add(AddressLine1);
            FormComponents.Add(AddressLine2);

            List<dynamic> LineComponents = new List<dynamic>();

            City.GetTextBox().Width = 200;
            LineComponents.Add(City);
            State.GetTextBox().Width = 100;
            LineComponents.Add(State);
            Zip.GetTextBox().Width = 80;
            LineComponents.Add(Zip);
            FormComponents.Add(LineComponents);


            List<dynamic> LineComponents2 = new List<dynamic>();

            LineComponents2.Add(BusPhone);
            Fax.GetTextBox().Width = 160;
            LineComponents2.Add(Fax);
            FormComponents.Add(LineComponents2);


            FormComponents.Add(EMail);
            FormComponents.Add(DateOpened);
            FormComponents.Add(Salesman);
            FormComponents.Add(Resale);
            FormComponents.Add(StmtCust);
            FormComponents.Add(StmtName);
            FormComponents.Add(PriceTier);
            FormComponents.Add(Terms);

            List<dynamic> LineComponents3 = new List<dynamic>();

            Limit.GetTextBox().Width = 120;
            Limit.GetLabel().Width = 80;
            LineComponents3.Add(Limit);
            Memo.GetLabel().Width = 100;
            Memo.GetTextBox().Width = 320;
            LineComponents3.Add(Memo);
            FormComponents.Add(LineComponents3);

            _addFormInputs(FormComponents, 30, 20, 800, 43, int.MaxValue, _panel.Controls);

            List<dynamic> FormComponents2 = new List<dynamic>();

            List<dynamic> LineComponents4 = new List<dynamic>();

            LineComponents4.Add(Taxable);
            LineComponents4.Add(SendStatements);
            FormComponents2.Add(LineComponents4);

            FormComponents2.Add(CoreTracking);
            FormComponents2.Add(CoreBalance);
            FormComponents2.Add(PrintCoreTot);
            FormComponents2.Add(AccountType);
            FormComponents2.Add(PORequired);
            FormComponents2.Add(CreditCode);
            FormComponents2.Add(InterestRate);
            FormComponents2.Add(AcctBalance);
            FormComponents2.Add(YTDPurchases);
            FormComponents2.Add(YTDInterest);
            FormComponents2.Add(DateLastPurch);

            _addFormInputs(FormComponents2, 1000, 20, 800, 43, int.MaxValue, _panel.Controls);
        }

        public void setDetails(int _id)
        {
            List<dynamic> data = new List<dynamic>();
            data = CustomersModelObj.GetCustomerDataById(_id);
            if (data[0].memo != null && !data[0].memo.Equals(DBNull.Value)) this.memo = data[0].memo;

            var customerData = data[0];

            this.customerId = customerData.id;

            this.CustomerNum.GetTextBox().Text = customerData.customerNumber.ToString();
            this.CustomerName.GetTextBox().Text = customerData.customerName.ToString();

            this.AddressLine1.GetTextBox().Text = customerData.address1.ToString();
            this.AddressLine2.GetTextBox().Text = customerData.address2.ToString();
            this.City.GetTextBox().Text = customerData.city.ToString();
            this.State.GetTextBox().Text = customerData.state.ToString();
            this.Zip.GetTextBox().Text = customerData.zipcode.ToString();
            this.BusPhone.GetTextBox().Text = customerData.businessPhone.ToString();
            this.Fax.GetTextBox().Text = customerData.fax.ToString();
            this.EMail.GetTextBox().Text = customerData.email.ToString();
            this.DateOpened.GetDateTimePicker().Value = customerData.dateOpened.ToLocalTime();
            this.Salesman.GetTextBox().Text = customerData.salesman.ToString();
            this.Resale.GetTextBox().Text = customerData.resale.ToString();
            this.StmtCust.GetTextBox().Text = customerData.statementCustomerNumber.ToString();
            this.StmtName.GetTextBox().Text = customerData.statementName.ToString();
            //            this.pricetimer_textbox.Text = data[0].priceTierId.ToString();
            selectId = (int)customerData.priceTierId;

            this.Terms.GetTextBox().Text = customerData.terms.ToString();
            this.Limit.GetTextBox().Text = customerData.limit.ToString();
            this.Memo.GetTextBox().Text = customerData.memo.ToString();
            this.Taxable.GetCheckBox().Checked = customerData.taxable.ToString() == "True" ? true : false;
            this.SendStatements.GetCheckBox().Checked = customerData.sendStatements.ToString() == "True" ? true : false;
       
            this.CoreTracking.GetTextBox().Text = customerData.coreTracking.ToString();
            this.CoreBalance.GetTextBox().Text = customerData.coreBalance.ToString();
            this.PrintCoreTot.GetTextBox().Text = customerData.printCoreTotal.ToString();
            this.AccountType.GetTextBox().Text = customerData.accountType.ToString();
            this.PORequired.GetTextBox().Text = customerData.poRequired.ToString();
            this.CreditCode.GetTextBox().Text = customerData.creditCodeId.ToString();
            this.InterestRate.GetTextBox().Text = customerData.interestRate.ToString();
            this.AcctBalance.GetTextBox().Text = customerData.accountBalance.ToString();
          
            this.YTDInterest.GetTextBox().Text = customerData.yearToDateInterest.ToString();
            this.DateLastPurch.GetDateTimePicker().Value = customerData.dateLastPurchased.ToLocalTime();

        }

        private void SaveCustomer(object sender, KeyEventArgs e)
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

            if (state.Length > 2)
            {
                MessageBox.Show("State Field mustn't limit 3.");
                return;
            }

            if (PriceTier.GetComboBox().SelectedItem == null)
            {
                MessageBox.Show("please select PriceTier");
                //PriceTier.GetComboBox().Select();
                return;
            }

            bool refreshData = false;

            if (customerId == 0) refreshData = CustomersModelObj.AddCustomer(customer_num, customer_name, address1, address2, city, state, zipcode, business_phone, fax, email, date_opened, salesman, resale, stmt_num, stmt_name, pricetier, terms, limit, memo, taxable, send_stm, core_tracking, core_balance, print_core_tot, acct_type, porequired, credit_card, interest_rate, acct_balance, ytd_purch, ytd_interest, last_date_purch);
            else refreshData = CustomersModelObj.UpdateCustomer(customer_num, customer_name, address1, address2, city, state, zipcode, business_phone, fax, email, date_opened, salesman, resale, stmt_num, stmt_name, pricetier, terms, limit, memo, taxable, send_stm, core_tracking, core_balance, print_core_tot, acct_type, porequired, credit_card, interest_rate, acct_balance, ytd_purch, ytd_interest, last_date_purch, customerId);

            string modeText = customerId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this._navigateToPrev(sender, e);
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

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Do you want to save?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveCustomer(sender, e);
                }
                else if (result == DialogResult.No)
                {
                    _navigateToPrev(sender, e);
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

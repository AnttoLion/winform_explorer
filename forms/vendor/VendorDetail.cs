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

namespace mjc_dev.forms.vendor
{
    public partial class VendorDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

        private FInputBox VendorNum = new FInputBox("Vendor#");
        private FInputBox VendorName = new FInputBox("Name");
        private FInputBox AddressLine1 = new FInputBox("Address Line 1");
        private FInputBox AddressLine2 = new FInputBox("Address Line 2");
        private FInputBox City = new FInputBox("City");
        private FInputBox State = new FInputBox("State");
        private FInputBox Zip = new FInputBox("Zip");
        private FInputBox BusPhone = new FInputBox("Bus.Phone");
        private FInputBox Fax = new FInputBox("Fax");

        private DashboardModel model = new DashboardModel();
        private int vendorId = 0;

        public VendorDetail() : base("Add Vendor")
        {
            InitializeComponent();
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitMBOKButton();
            InitInputBox();
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(425, 500);
            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            VendorNum.SetPosition(new Point(30, 30));
            this.Controls.Add(VendorNum.GetLabel());
            this.Controls.Add(VendorNum.GetTextBox());

            VendorName.SetPosition(new Point(30, 80));
            this.Controls.Add(VendorName.GetLabel());
            this.Controls.Add(VendorName.GetTextBox());

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

            Fax.SetPosition(new Point(30, 430));
            this.Controls.Add(Fax.GetLabel());
            this.Controls.Add(Fax.GetTextBox());
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            string vendorName = this.VendorName.GetTextBox().Text;
            string address1 = this.AddressLine1.GetTextBox().Text;
            string address2 = this.AddressLine2.GetTextBox().Text;
            string city = this.City.GetTextBox().Text;
            string state = this.State.GetTextBox().Text;
            string zipcode = this.Zip.GetTextBox().Text;
            string busphone = this.BusPhone.GetTextBox().Text;
            string fax = this.Fax.GetTextBox().Text;

            bool refreshData = false;

            if (vendorId == 0) refreshData = model.AddVendor(vendorName, address1, address2, city, state, zipcode, busphone, fax);
            else refreshData = model.UpdateVendor(vendorName, address1, address2, city, state, zipcode, busphone, fax, vendorId);

            string modeText = vendorId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the vendor.");
        }
        public void setDetails(List<dynamic> data, int id)
        {
            this.vendorId = id;

            this.VendorNum.GetTextBox().Text = data[0].id.ToString();
            this.VendorName.GetTextBox().Text = data[0].vendorName.ToString();
            this.AddressLine1.GetTextBox().Text = data[0].address1.ToString();
            this.AddressLine2.GetTextBox().Text = data[0].address2.ToString();
            this.City.GetTextBox().Text = data[0].city.ToString();
            this.State.GetTextBox().Text = data[0].state.ToString();
            this.Zip.GetTextBox().Text = data[0].zipcode.ToString();
            this.BusPhone.GetTextBox().Text = data[0].businessPhone.ToString();
            this.Fax.GetTextBox().Text = data[0].fax.ToString();
        }
    }
}

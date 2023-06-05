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

namespace mjc_dev.forms.modals
{
    public partial class CategoryDetail : BasicModal
    {
        private ModalButton MBOk = new ModalButton("(Enter) OK", Keys.Enter);
        private Button MBOk_button;

        private FInputBox categoryName = new FInputBox("Name");
        private FInputBox calculateAs = new FInputBox("calculateAs");

        private int categoryId;
        private DashboardModel model = new DashboardModel();

        public CategoryDetail() : base("Add Category")
        {
            InitializeComponent();
            this.Size = new Size(600, 260);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitMBOKButton();
            InitInputBox();
        }

        private void InitMBOKButton()
        {
            ModalButton_HotKeyDown(MBOk);
            MBOk_button = MBOk.GetButton();
            MBOk_button.Location = new Point(425, 150);
            MBOk_button.Click += (sender, e) => ok_button_Click(sender, e);
            this.Controls.Add(MBOk_button);
        }

        private void InitInputBox()
        {
            categoryName.SetPosition(new Point(30, 30));
            this.Controls.Add(categoryName.GetLabel());
            this.Controls.Add(categoryName.GetTextBox());

            calculateAs.SetPosition(new Point(30, 80));
            this.Controls.Add(calculateAs.GetLabel());
            this.Controls.Add(calculateAs.GetTextBox());
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            string name = this.categoryName.GetTextBox().Text;
            int calculateAs = int.Parse(this.calculateAs.GetTextBox().Text);

            bool refreshData = false;
            if (categoryId == 0)
                refreshData = model.AddCategory(name, calculateAs);
            else refreshData = model.UpdateCategory(name, calculateAs, categoryId);

            string modeText = categoryId == 0 ? "creating" : "updating";

            if (refreshData)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("An Error occured while " + modeText + " the category.");
        }
        public void setDetails(string categoryName, string calculateAs, int category_id)
        {
            this.categoryName.GetTextBox().Text = categoryName;
            this.calculateAs.GetTextBox().Text = calculateAs.ToString();
            this.categoryId = category_id;
        }
    }
}

using mjc_dev.common.components;
using mjc_dev.common;
using mjc_dev.forms.category;
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
    public partial class Allocations : GlobalLayout
    {

        private HotkeyButton hkPrevScreen = new HotkeyButton("Esc", "Previous screen", Keys.Escape);
        private HotkeyButton OpenOrder = new HotkeyButton("Enter", "Open order", Keys.Enter);

        private GridViewOrigin allocationsGrid = new GridViewOrigin();
        private DataGridView ALGridRefer;
        private DashboardModel model = new DashboardModel();

        public Allocations() : base("Allocations for SKU#", "Review held orders with sku quantity allocated")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPrevScreen, OpenOrder };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitAllocationsGrid();
        }

        private void AddHotKeyEvents()
        {
        }

        private void InitAllocationsGrid()
        {
            ALGridRefer = allocationsGrid.GetGrid();
            ALGridRefer.Location = new Point(0, 95);
            ALGridRefer.Width = this.Width;
            ALGridRefer.Height = this.Height - 295;
            this.Controls.Add(ALGridRefer);
            this.ALGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.addcategory_btn_Click);

            ALGridRefer.Columns.Add("Name", "Date");
            ALGridRefer.Columns.Add("Age", "Cust#");
            ALGridRefer.Columns.Add("Country", "Held Status");
            ALGridRefer.Columns.Add("Country", "Proc By");
            ALGridRefer.Columns.Add("Country", "Qty");
            ALGridRefer.Columns.Add("Country", "Price");
            ALGridRefer.Columns.Add("Country", "Subtotal");

            ALGridRefer.Columns[0].Width = 200;
            ALGridRefer.Columns[1].Width = 300;
            ALGridRefer.Columns[2].Width = 200;
            ALGridRefer.Columns[3].Width = 200;
            ALGridRefer.Columns[4].Width = 200;
            ALGridRefer.Columns[5].Width = 200;
            ALGridRefer.Columns[6].Width = 200;
            //ALGridRefer.Columns[7].Width = 300;

            LoadCategoryList();
        }

        private void LoadCategoryList()
        {
            return;
            string filter = "";
            var refreshData = model.LoadCategoryData(filter);
            if (refreshData)
            {
                ALGridRefer.DataSource = model.CategoryDataList;
                ALGridRefer.Columns[0].Visible = false;
                ALGridRefer.Columns[1].HeaderText = "Category";
                ALGridRefer.Columns[1].Width = 300;
                ALGridRefer.Columns[2].HeaderText = "Cal As";
                ALGridRefer.Columns[2].Width = 300;
            }
        }

        private void updateCategory()
        {
            CategoryDetail detailModal = new CategoryDetail();

            int rowIndex = ALGridRefer.CurrentCell.RowIndex;

            if (ALGridRefer.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in ALGridRefer.SelectedRows)
                {
                    int categoryId = (int)row.Cells[0].Value;
                    string categoryName = row.Cells[1].Value.ToString();
                    string calculateAs = row.Cells[2].Value.ToString();

                    detailModal.setDetails(categoryName, calculateAs, categoryId);
                }
            }

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadCategoryList();
            }
        }

        private void addcategory_btn_Click(object sender, DataGridViewCellEventArgs e)
        {
            updateCategory();
        }
    }
}

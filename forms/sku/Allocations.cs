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

        private GridViewOrigin categoryListGrid = new GridViewOrigin();
        private DataGridView CLGridRefer;
        private DashboardModel model = new DashboardModel();

        public Allocations() : base("Cross References", "List all serials that SKUs may also be searched by based on listings by other vendors")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPrevScreen, OpenOrder };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitCategoryListGrid();
        }

        private void AddHotKeyEvents()
        {
        }

        private void InitCategoryListGrid()
        {
            CLGridRefer = categoryListGrid.GetGrid();
            CLGridRefer.Location = new Point(0, 95);
            CLGridRefer.Width = this.Width;
            CLGridRefer.Height = this.Height - 295;
            this.Controls.Add(CLGridRefer);
            this.CLGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.addcategory_btn_Click);

            CLGridRefer.Columns.Add("Name", "Date");
            CLGridRefer.Columns.Add("Age", "Cust#");
            CLGridRefer.Columns.Add("Country", "Held Status");
            CLGridRefer.Columns.Add("Country", "Proc By");
            CLGridRefer.Columns.Add("Country", "Qty");
            CLGridRefer.Columns.Add("Country", "Price");
            CLGridRefer.Columns.Add("Country", "Subtotal");

            CLGridRefer.Columns[0].Width = 300;
            CLGridRefer.Columns[1].Width = 300;
            CLGridRefer.Columns[2].Width = 300;
            CLGridRefer.Columns[3].Width = 300;
            CLGridRefer.Columns[4].Width = 300;
            CLGridRefer.Columns[5].Width = 300;
            CLGridRefer.Columns[6].Width = 300;
            CLGridRefer.Columns[7].Width = 300;


            LoadCategoryList();
        }

        private void LoadCategoryList()
        {
            return;
            string filter = "";
            var refreshData = model.LoadCategoryData(filter);
            if (refreshData)
            {
                CLGridRefer.DataSource = model.CategoryDataList;
                CLGridRefer.Columns[0].Visible = false;
                CLGridRefer.Columns[1].HeaderText = "Category";
                CLGridRefer.Columns[1].Width = 300;
                CLGridRefer.Columns[2].HeaderText = "Cal As";
                CLGridRefer.Columns[2].Width = 300;
            }
        }

        private void updateCategory()
        {
            CategoryDetail detailModal = new CategoryDetail();

            int rowIndex = CLGridRefer.CurrentCell.RowIndex;

            if (CLGridRefer.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in CLGridRefer.SelectedRows)
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

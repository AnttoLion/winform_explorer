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

namespace mjc_dev.forms.sales
{
    public partial class SalesHisotry : GlobalLayout
    {

        private HotkeyButton hkNavigatePreviousYears = new HotkeyButton("<= / =>", "Previous screen", Keys.None);

        private GridViewOrigin slaesHistoryGrid = new GridViewOrigin();
        private DataGridView SHGridRefer;
        private DashboardModel model = new DashboardModel();

        public SalesHisotry() : base("History for SKU", "Sales history for the selected SKU#")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[1] { hkNavigatePreviousYears };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitSalesHistoryGrid();
        }

        private void AddHotKeyEvents()
        {
        }

        private void InitSalesHistoryGrid()
        {
            SHGridRefer = slaesHistoryGrid.GetGrid();
            SHGridRefer.Location = new Point(0, 95);
            SHGridRefer.Width = this.Width;
            SHGridRefer.Height = this.Height - 295;
            this.Controls.Add(SHGridRefer);
            this.SHGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.addcategory_btn_Click);

            SHGridRefer.Columns.Add("Name", "Quantities");
            SHGridRefer.Columns.Add("Age", "2023");
            SHGridRefer.Columns.Add("Country", "2022");
            SHGridRefer.Columns.Add("Country", "2021");

            SHGridRefer.Columns[0].Width = 200;
            SHGridRefer.Columns[1].Width = 300;
            SHGridRefer.Columns[2].Width = 200;
            SHGridRefer.Columns[3].Width = 200;
            //SHGridRefer.Columns[4].Width = 200;
            //SHGridRefer.Columns[5].Width = 200;
            //SHGridRefer.Columns[6].Width = 200;
            //SHGridRefer.Columns[7].Width = 300;

            LoadCategoryList();
        }

        private void LoadCategoryList()
        {
            return;
            string filter = "";
            var refreshData = model.LoadCategoryData(filter);
            if (refreshData)
            {
                SHGridRefer.DataSource = model.CategoryDataList;
                SHGridRefer.Columns[0].Visible = false;
                SHGridRefer.Columns[1].HeaderText = "Category";
                SHGridRefer.Columns[1].Width = 300;
                SHGridRefer.Columns[2].HeaderText = "Cal As";
                SHGridRefer.Columns[2].Width = 300;
            }
        }

        private void updateCategory()
        {
            CategoryDetail detailModal = new CategoryDetail();

            int rowIndex = SHGridRefer.CurrentCell.RowIndex;

            if (SHGridRefer.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in SHGridRefer.SelectedRows)
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

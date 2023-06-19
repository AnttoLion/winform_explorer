using mjc_dev.common.components;
using mjc_dev.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mjc_dev.model;

namespace mjc_dev.forms.category
{
    public partial class CategoryMargin : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkSetsPrices = new HotkeyButton("F4", "Sets Prices", Keys.F4);

        private GridViewOrigin categoryListGrid = new GridViewOrigin();
        private DataGridView CLGridRefer;
        private CategoriesModel CategoriesModelObj = new CategoriesModel();

        public CategoryMargin() : base("Category Margins", "Manage category margins used to calcuate prices")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[4] { hkAdds, hkDeletes, hkSelects, hkSetsPrices };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitCategoryListGrid();

            this.VisibleChanged += (s, e) => 
            {
                this.LoadCategoryList();
            };
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                CategoryDetail detailModal = new CategoryDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadCategoryList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedCategoryId = 0;
                if (CLGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in CLGridRefer.SelectedRows)
                    {
                        selectedCategoryId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = CategoriesModelObj.DeleteCategory(selectedCategoryId);
                if (refreshData)
                {
                    LoadCategoryList();
                }
            };
            hkSelects.GetButton().Click += (sender, e) =>
            {
                updateCategory();
            };
        }

        private void InitCategoryListGrid()
        {
            CLGridRefer = categoryListGrid.GetGrid();
            CLGridRefer.Location = new Point(0, 95);
            CLGridRefer.Width = this.Width;
            CLGridRefer.Height = this.Height - 295;
            this.Controls.Add(CLGridRefer);
            this.CLGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.addcategory_btn_Click);
        }

        private void LoadCategoryList()
        {
            string filter = "";
            var refreshData = CategoriesModelObj.LoadCategoryData(filter);
            if (refreshData)
            {
                CLGridRefer.DataSource = CategoriesModelObj.CategoryDataList;
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

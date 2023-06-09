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
    public partial class CrossReference : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkSetsPrices = new HotkeyButton("F4", "Sets Prices", Keys.F4);

        private GridViewOrigin categoryListGrid = new GridViewOrigin();
        private DataGridView CLGridRefer;
        private DashboardModel model = new DashboardModel();

        public CrossReference() : base("Cross References", "List all serials that SKUs may also be searched by based on listings by other vendors")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[4] { hkAdds, hkDeletes, hkSelects, hkSetsPrices };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitCategoryListGrid();
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
            };
            hkSelects.GetButton().Click += (sender, e) =>
            {
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

            CLGridRefer.Columns.Add("Name", "Cross-Reference");
            CLGridRefer.Columns.Add("Age", "Manufacturer");
            CLGridRefer.Columns.Add("Country", "SKU#");
            CLGridRefer.Columns.Add("Country", "Description");

            CLGridRefer.Columns[0].Width = 300;
            CLGridRefer.Columns[1].Width = 300;
            CLGridRefer.Columns[2].Width = 300;
            CLGridRefer.Columns[3].Width = 300;


            LoadCategoryList();
        }

        private void LoadCategoryList()
        {
            return;
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

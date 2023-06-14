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
using System.Runtime.Remoting.Channels;
using mjc_dev.forms.sales;

namespace mjc_dev.forms.sku
{
    public partial class SKUList : GlobalLayout
    {
        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkCrossRefLookup = new HotkeyButton("F2", "Cross Ref Lookup", Keys.F2);
        private HotkeyButton hkView = new HotkeyButton("F3", "View Allocations", Keys.F3);
        private HotkeyButton hkAdjustQty = new HotkeyButton("F4", "Adjust Qty", Keys.F4);
        private HotkeyButton hkSKUHistory = new HotkeyButton("F5", "Profile History", Keys.F5);
        private HotkeyButton hkProfileHistory = new HotkeyButton("F6", "SKU History", Keys.F6);
        private HotkeyButton hkArchivedSKUs = new HotkeyButton("F8", "Archived SKUs", Keys.F8);

        private GridViewOrigin SKUListGrid = new GridViewOrigin();
        private DataGridView SKUGridRefer;
        private DashboardModel model = new DashboardModel();

        public SKUList() : base("SKU List", "List of SKUs")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[9] { hkAdds, hkDeletes, hkSelects, hkCrossRefLookup, hkView, hkAdjustQty, hkSKUHistory, hkProfileHistory, hkArchivedSKUs };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitPriceTierGrid();


            this.VisibleChanged += (ss, sargs) => {
                this.LoadSKUList();
            };
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                this.Hide();
                SKUDetail detailModal = new SKUDetail();
                _navigateToForm(sender,e, detailModal);
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                DialogResult result = MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int selectedSKUId = 0;
                    if (SKUGridRefer.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow row in SKUGridRefer.SelectedRows)
                        {
                            selectedSKUId = (int)row.Cells[0].Value;
                        }
                    }
                    bool refreshData = model.DeleteSKU(selectedSKUId);
                    if (refreshData)
                    {
                        LoadSKUList();
                    }
                }
            };
            hkSelects.GetButton().Click += (sender, e) =>
            {
                updateSKU(sender, e);
            };
            hkCrossRefLookup.GetButton().Click += (sender, e) =>
            {
                this.Hide();
                CrossReference CrossRefModal = new CrossReference();
                _navigateToForm(sender, e, CrossRefModal);
            };
            hkView.GetButton().Click += (sender, e) =>
            {
                this.Hide();
                Allocations CrossRefModal = new Allocations();
                _navigateToForm(sender, e, CrossRefModal);
            };
            hkSKUHistory.GetButton().Click += (sender, e) =>
            {
                this.Hide();
                SalesHisotry CrossRefModal = new SalesHisotry();
                _navigateToForm(sender, e, CrossRefModal);
            };
        }

        private void InitPriceTierGrid()
        {
            SKUGridRefer = SKUListGrid.GetGrid();
            SKUGridRefer.Location = new Point(0, 95);
            SKUGridRefer.Width = this.Width;
            SKUGridRefer.Height = this.Height - 295;
            this.Controls.Add(SKUGridRefer);
            this.SKUGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SKUGridView_CellDoubleClick);

            LoadSKUList();
        }


        public void LoadSKUList()
        {
            string filter = "";
            var refreshData = model.LoadSKUData(filter);
            if (refreshData)
            {
                SKUGridRefer.DataSource = model.SKUDataList;
                SKUGridRefer.Columns[0].Visible = false;
                SKUGridRefer.Columns[1].HeaderText = "SKU#";
                SKUGridRefer.Columns[1].Width = 300;
                SKUGridRefer.Columns[2].HeaderText = "Category";
                SKUGridRefer.Columns[2].Width = 300;
                SKUGridRefer.Columns[3].HeaderText = "Description";
                SKUGridRefer.Columns[3].Width = 500;
                SKUGridRefer.Columns[4].HeaderText = "Qty Avail";
                SKUGridRefer.Columns[4].Width = 300;
                SKUGridRefer.Columns[5].HeaderText = "Qty Tracking";
                SKUGridRefer.Columns[5].Width = 300;
            }
        }

        private void updateSKU(object sender, EventArgs e)
        {
            SKUDetail detailModal = new SKUDetail();

            int rowIndex = SKUGridRefer.CurrentCell.RowIndex;

            DataGridViewRow row = SKUGridRefer.Rows[rowIndex];
            int skuId = (int)row.Cells[0].Value;

            List<dynamic> skuData = new List<dynamic>();
            skuData = model.GetSKUData(skuId);
            detailModal.setDetails(skuData, skuData[0].id);

            this.Hide();
            _navigateToForm(sender, e, detailModal);
        }

        private void SKUGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            updateSKU(sender, e);
        }
    }
}

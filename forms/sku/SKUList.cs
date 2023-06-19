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
using Microsoft.VisualBasic;

namespace mjc_dev.forms.sku
{
    public partial class SKUList : GlobalLayout
    {
        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkCrossRefLookup = new HotkeyButton("F1", "Cross Ref Lookup", Keys.F1);
        private HotkeyButton hkViewAllocations = new HotkeyButton("F2", "View Allocations", Keys.F2);
        private HotkeyButton hkAdjustQty = new HotkeyButton("F4", "Adjust Qty", Keys.F4);
        private HotkeyButton hkSKUHistory = new HotkeyButton("F5", "Profile History", Keys.F5);
        private HotkeyButton hkProfileHistory = new HotkeyButton("F6", "SKU History", Keys.F6);
        private HotkeyButton hkArchivedSKUs = new HotkeyButton("F8", "Archived SKUs", Keys.F8);
        private HotkeyButton hkActiveSKUs = new HotkeyButton("F9", "Active SKUs", Keys.F9);

        private GridViewOrigin SKUListGrid = new GridViewOrigin();
        private DataGridView SKUGridRefer;
        private int SKUGridSelectedIndex = 0;

        private string searchKey = "";

        private SKUModel SKUModelObj = new SKUModel();

        public SKUList(bool ArchivedView = false) : base("SKU List", "List of SKUs")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[10] { hkAdds, hkDeletes, hkSelects, hkCrossRefLookup, hkViewAllocations, hkAdjustQty, hkSKUHistory, hkProfileHistory, hkArchivedSKUs, hkActiveSKUs };
            _initializeHKButtons(hkButtons);

            hkActiveSKUs.SetPosition(hkArchivedSKUs.GetButton().Location);
            hkActiveSKUs.GetButton().Hide();
            hkActiveSKUs.GetLabel().Hide();

            AddHotKeyEvents();

            InitSKUGrid();

            this.VisibleChanged += (ss, sargs) => {
                Console.WriteLine(ss);
                this.LoadSKUList();
            };

            this.KeyDown += (s, e) => 
            { 
                if(e.KeyCode == Keys.F && e.Control)
                {
                    this.Enabled = false;
                    SearchInput searchInputModal = new SearchInput();
                    searchInputModal.SetSearchKey(this.searchKey);
                    searchInputModal.Show();

                    searchInputModal.FormClosed += (ss, ee) =>
                    {
                        if(this.searchKey != searchInputModal.GetSearchKey())
                        {
                            this.searchKey = searchInputModal.GetSearchKey();
                            this.LoadSKUList(false, false);
                        }
                        this.Enabled = true;
                    };
                }
            };
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                this.Hide();
                SKUInformation detailModal = new SKUInformation();
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
                    bool refreshData = SKUModelObj.DeleteSKU(selectedSKUId);
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

                int rowIndex = SKUGridRefer.SelectedRows[0].Index;
                this.SKUGridSelectedIndex = SKUGridRefer.SelectedRows[0].Index;

                DataGridViewRow row = SKUGridRefer.Rows[rowIndex];
                int skuId = (int)row.Cells[0].Value;

                CrossReference CrossRefModal = new CrossReference(skuId, row.Cells[1].Value.ToString());
                _navigateToForm(sender, e, CrossRefModal);
                this.Hide();
            };
            hkViewAllocations.GetButton().Click += (sender, e) =>
            {
                this.SKUGridSelectedIndex = SKUGridRefer.SelectedRows[0].Index;

                Allocations AllocationsModal = new Allocations();
                _navigateToForm(sender, e, AllocationsModal);
                this.Hide();
            };
            hkAdjustQty.GetButton().Click += (sender, e) =>
            {
                int rowIndex = SKUGridRefer.SelectedRows[0].Index;

                this.SKUGridSelectedIndex = rowIndex;

                DataGridViewRow row = SKUGridRefer.Rows[rowIndex];
                int skuId = (int)row.Cells[0].Value;
                string skuName = row.Cells[1].Value.ToString();

                this.Enabled = false;
                AdjustQty AdjustQtyModal = new AdjustQty(skuId, skuName);
                AdjustQtyModal.Show();
                AdjustQtyModal.FormClosed += (ss, sargs) =>
                {
                    this.Enabled = true;
                    this.LoadSKUList();
                };
            };
            hkSKUHistory.GetButton().Click += (sender, e) =>
            {
                this.SKUGridSelectedIndex = SKUGridRefer.SelectedRows[0].Index;

                SalesHisotry SalesHistoryModal = new SalesHisotry();
                _navigateToForm(sender, e, SalesHistoryModal);
                this.Hide();
            };
            hkProfileHistory.GetButton().Click += (sender, e) =>
            {
                this.SKUGridSelectedIndex = SKUGridRefer.SelectedRows[0].Index;

                SKUProfile SKUProfileModal = new SKUProfile();
                _navigateToForm(sender, e, SKUProfileModal);
                this.Hide();
            };
            hkArchivedSKUs.GetButton().Click += (sender, e) => {

                hkCrossRefLookup.GetButton().Hide();
                hkCrossRefLookup.GetLabel().Hide();

                hkViewAllocations.GetButton().Hide();
                hkViewAllocations.GetLabel().Hide();

                hkAdjustQty.GetButton().Hide();
                hkAdjustQty.GetLabel().Hide();

                hkArchivedSKUs.GetButton().Hide();
                hkArchivedSKUs.GetLabel().Hide();

                hkActiveSKUs.GetButton().Show();
                hkActiveSKUs.GetLabel().Show();

                this._changeFormText("ARCHIVED - SKU List");

                LoadSKUList(true, false);
            };

            hkActiveSKUs.GetButton().Click += (sender, e) => {

                hkCrossRefLookup.GetButton().Show();
                hkCrossRefLookup.GetLabel().Show();

                hkViewAllocations.GetButton().Show();
                hkViewAllocations.GetLabel().Show();

                hkAdjustQty.GetButton().Show();
                hkAdjustQty.GetLabel().Show();

                hkArchivedSKUs.GetButton().Show();
                hkArchivedSKUs.GetLabel().Show();

                hkActiveSKUs.GetButton().Hide();
                hkActiveSKUs.GetLabel().Hide();

                this._changeFormText("SKU List");

                LoadSKUList(false, false);
            };
        }

        private void InitSKUGrid()
        {
            SKUGridRefer = SKUListGrid.GetGrid();
            SKUGridRefer.Location = new Point(0, 95);
            SKUGridRefer.Width = this.Width - 20;
            SKUGridRefer.Height = this.Height - 330;
            this.Controls.Add(SKUGridRefer);
            this.SKUGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SKUGridView_CellDoubleClick);
            this.SKUGridRefer.SelectionChanged += (s, e) => SKUGridRefer_SelectionChanged(s, e);
        }


        public void LoadSKUList(bool archivedView = false, bool keepSelection = true)
        {
            if(this.searchKey == "")
            {
                this._changeFormText("SKU List");
            }
            else
            {
                this._changeFormText("SKU List searched by " + this.searchKey);
            }
            var refreshData = SKUModelObj.LoadSKUData(this.searchKey, archivedView);
            if (refreshData)
            {
                SKUGridRefer.DataSource = SKUModelObj.SKUDataList;
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

            if(keepSelection)
            {
                SKUGridRefer.ClearSelection();
                if (SKUGridSelectedIndex >= 0 && SKUGridSelectedIndex < SKUGridRefer.Rows.Count)
                {
                    SKUGridRefer.Rows[SKUGridSelectedIndex].Selected = true;
                    SKUGridRefer.CurrentCell = SKUGridRefer[1, SKUGridSelectedIndex];
                }
            }
        }

        private void updateSKU(object sender, EventArgs e)
        {
            SKUInformation detailModal = new SKUInformation();

            int rowIndex = SKUGridRefer.SelectedRows[0].Index;

            this.SKUGridSelectedIndex = rowIndex;

            DataGridViewRow row = SKUGridRefer.Rows[rowIndex];
            int skuId = (int)row.Cells[0].Value;

            List<dynamic> skuData = new List<dynamic>();
            skuData = SKUModelObj.GetSKUData(skuId);
            detailModal.setDetails(skuData, skuData[0].id);

            this.Hide();
            _navigateToForm(sender, e, detailModal);
        }

        private void SKUGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            updateSKU(sender, e);
        }

        private void SKUGridRefer_SelectionChanged(object sender, EventArgs e)
        {

            if (SKUGridRefer.SelectedRows.Count > 0)
            {
                
            }
        }
    }
}

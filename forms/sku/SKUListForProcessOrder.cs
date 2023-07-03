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
    public partial class SKUListForProcessOrder : GlobalLayout
    {
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkCrossRefLookup = new HotkeyButton("F2", "Cross Ref Lookup", Keys.F2);
        private HotkeyButton hkView = new HotkeyButton("F3", "View", Keys.F3);

        private GridViewOrigin SKUListGrid = new GridViewOrigin();
        private DataGridView SKUGridRefer;
        private int SKUGridSelectedIndex = 0;

        private string searchKey = "";

        private SKUModel SKUModelObj = new SKUModel();

        public SKUListForProcessOrder() : base("SKU List", "Select a SKU to open")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[3] {hkSelects, hkCrossRefLookup, hkView };
            _initializeHKButtons(hkButtons);

            AddHotKeyEvents();

            InitSKUGrid();

            this.VisibleChanged += (ss, sargs) => {
                Console.WriteLine(ss);
                this.LoadSKUList();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F && e.Control)
                {
                    this.Enabled = false;
                    SearchInput searchInputModal = new SearchInput();
                    searchInputModal.SetSearchKey(this.searchKey);
                    searchInputModal.Show();

                    searchInputModal.FormClosed += (ss, ee) =>
                    {
                        if (this.searchKey != searchInputModal.GetSearchKey())
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
            hkSelects.GetButton().Click += (sender, e) =>
            {
                selectSKU(sender, e);
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
            if (this.searchKey == "")
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

            if (keepSelection)
            {
                SKUGridRefer.ClearSelection();
                if (SKUGridSelectedIndex >= 0 && SKUGridSelectedIndex < SKUGridRefer.Rows.Count)
                {
                    SKUGridRefer.Rows[SKUGridSelectedIndex].Selected = true;
                    SKUGridRefer.CurrentCell = SKUGridRefer[1, SKUGridSelectedIndex];
                }
            }
        }

        private void selectSKU(object sender, EventArgs e)
        {
            this.Hide();
            _navigateToPrev(sender, e);   
        }

        public int GetSelectedSKUId()
        {
            return (int)SKUGridRefer.SelectedRows[0].Cells[0].Value;
        }

        public string GetSelectedSKUName()
        {
            return (string)SKUGridRefer.SelectedRows[0].Cells[1].Value;
        }

        private void SKUGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectSKU(sender, e);
        }

        private void SKUGridRefer_SelectionChanged(object sender, EventArgs e)
        {

            if (SKUGridRefer.SelectedRows.Count > 0)
            {

            }
        }
    }
}

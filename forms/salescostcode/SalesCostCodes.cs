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

namespace mjc_dev.forms.salescostcode
{
    public partial class SalesCostCodes : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkEdits = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);

        private GridViewOrigin salesCostCodeListGrid = new GridViewOrigin();
        private DataGridView SCCLGridRefer;
        private SalesCostCodeModel SalesCostCodesModelObj = new SalesCostCodeModel();

        public SalesCostCodes() : base("Sales/Cost Codes", "Sales/cost codes on record")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[4] { hkAdds, hkDeletes, hkEdits, hkPreviousScreen };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitSalesCostCodeListGrid();

            this.Load += (s, e) =>
            {
                this.LoadSalesCostCodeList();
            };
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                SalesCostCodesDetail detailModal = new SalesCostCodesDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadSalesCostCodeList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedSalesCostCodeId = 0;
                if (SCCLGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in SCCLGridRefer.SelectedRows)
                    {
                        selectedSalesCostCodeId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = SalesCostCodesModelObj.DeleteSalesCostCode(selectedSalesCostCodeId);
                if (refreshData)
                {
                    LoadSalesCostCodeList();
                }
            };
            hkEdits.GetButton().Click += (sender, e) =>
            {
                updateSalesCostCode(sender, e);
            };
        }
        private void InitSalesCostCodeListGrid()
        {
            SCCLGridRefer = salesCostCodeListGrid.GetGrid();
            SCCLGridRefer.Location = new Point(0, 95);
            SCCLGridRefer.Width = this.Width;
            SCCLGridRefer.Height = this.Height - 295;
            this.Controls.Add(SCCLGridRefer);
            this.SCCLGridRefer.CellDoubleClick += (sender, e) =>
            {
                updateSalesCostCode(sender, e);
            };
        }

        private void LoadSalesCostCodeList()
        {
            string filter = "";
            var refreshData = SalesCostCodesModelObj.LoadSalesCostCodeData(filter);
            if (refreshData)
            {
                SCCLGridRefer.DataSource = SalesCostCodesModelObj.SalesCostCodeDataList;
                SCCLGridRefer.Columns[0].Visible = false;
                SCCLGridRefer.Columns[1].HeaderText = "SC Code";
                SCCLGridRefer.Columns[1].Width = 300;
                SCCLGridRefer.Columns[2].HeaderText = "Sales Account";
                SCCLGridRefer.Columns[2].Width = 300;
                SCCLGridRefer.Columns[3].HeaderText = "Cost Account";
                SCCLGridRefer.Columns[3].Width = 300;
                SCCLGridRefer.Columns[4].HeaderText = "Title";
                SCCLGridRefer.Columns[4].Width = 300;
            }
        }
        private void updateSalesCostCode(object sender, EventArgs e)
        {
            SalesCostCodesDetail detailModal = new SalesCostCodesDetail();

            int rowIndex = SCCLGridRefer.CurrentCell.RowIndex;

            DataGridViewRow row = SCCLGridRefer.Rows[rowIndex];

            int id = (int)row.Cells[0].Value;

            detailModal.setDetails(id);

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadSalesCostCodeList();
            }
        }
    }
}

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
using mjc_dev.forms.salescostcode;

namespace mjc_dev.forms.taxcode
{
    public partial class SalesTaxCodes : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkEdits = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);

        private GridViewOrigin salesTaxCodeListGrid = new GridViewOrigin();
        private DataGridView STCLGridRefer;
        private SalesTaxCodeModel SalesTaxCodesModelObj = new SalesTaxCodeModel();
        public SalesTaxCodes() : base("Sales Tax Codes", "Manage sales tax codes used by the system")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[4] { hkAdds, hkDeletes, hkEdits, hkPreviousScreen };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitSalesTaxCodeListGrid();

            this.Load += (s, e) =>
            {
                this.LoadSalesTaxCodeList();
            };
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                SalesTaxCodesDetail detailModal = new SalesTaxCodesDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadSalesTaxCodeList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedSalesTaxCodeId = 0;
                if (STCLGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in STCLGridRefer.SelectedRows)
                    {
                        selectedSalesTaxCodeId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = SalesTaxCodesModelObj.DeleteSalesTaxCode(selectedSalesTaxCodeId);
                if (refreshData)
                {
                    LoadSalesTaxCodeList();
                }
            };
            hkEdits.GetButton().Click += (sender, e) =>
            {
                updateSalesTaxCode(sender, e);
            };
        }

        private void InitSalesTaxCodeListGrid()
        {
            STCLGridRefer = salesTaxCodeListGrid.GetGrid();
            STCLGridRefer.Location = new Point(0, 95);
            STCLGridRefer.Width = this.Width;
            STCLGridRefer.Height = this.Height - 295;
            this.Controls.Add(STCLGridRefer);
            this.STCLGridRefer.CellDoubleClick += (sender, e) =>
            {
                updateSalesTaxCode(sender, e);
            };
        }
        private void LoadSalesTaxCodeList()
        {
            string filter = "";
            var refreshData = SalesTaxCodesModelObj.LoadSalesTaxCodeData(filter);
            if (refreshData)
            {
                STCLGridRefer.DataSource = SalesTaxCodesModelObj.SalesTaxCodeDataList;
                STCLGridRefer.Columns[0].Visible = false;
                STCLGridRefer.Columns[1].HeaderText = "Tax Identifier";
                STCLGridRefer.Columns[1].Width = 400;
                STCLGridRefer.Columns[2].HeaderText = "Classification";
                STCLGridRefer.Columns[2].Width = 400;
                STCLGridRefer.Columns[3].HeaderText = "Rate";
                STCLGridRefer.Columns[3].Width = 400;
            }
        }
        private void updateSalesTaxCode(object sender, EventArgs e)
        {
            SalesTaxCodesDetail detailModal = new SalesTaxCodesDetail();

            int rowIndex = STCLGridRefer.CurrentCell.RowIndex;

            DataGridViewRow row = STCLGridRefer.Rows[rowIndex];

            int id = (int)row.Cells[0].Value;

            detailModal.setDetails(id);

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadSalesTaxCodeList();
            }
        }
    }
}

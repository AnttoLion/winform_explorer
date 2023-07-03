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
using mjc_dev.forms.taxcode;

namespace mjc_dev.forms.creditcode
{
    public partial class CreditCodes : GlobalLayout
    {
        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkEdits = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);

        private GridViewOrigin creditCodeListGrid = new GridViewOrigin();
        private DataGridView CCLGridRefer;
        private CreditCodeModel CreditCodesModelObj = new CreditCodeModel();

        public CreditCodes() : base("Credit Codes", "Manage credit codes on record")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[4] { hkAdds, hkDeletes, hkEdits, hkPreviousScreen };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitCreditCodeListGrid();

            this.Load += (s, e) =>
            {
                this.LoadCreditCodeList();
            };
        }
        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                CreditCodesDetail detailModal = new CreditCodesDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadCreditCodeList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedSalesTaxCodeId = 0;
                if (CCLGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in CCLGridRefer.SelectedRows)
                    {
                        selectedSalesTaxCodeId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = CreditCodesModelObj.DeleteCreditCode(selectedSalesTaxCodeId);
                if (refreshData)
                {
                    LoadCreditCodeList();
                }
            };
            hkEdits.GetButton().Click += (sender, e) =>
            {
                updateSalesTaxCode(sender, e);
            };
        }

        private void InitCreditCodeListGrid()
        {
            CCLGridRefer = creditCodeListGrid.GetGrid();
            CCLGridRefer.Location = new Point(0, 95);
            CCLGridRefer.Width = this.Width;
            CCLGridRefer.Height = this.Height - 295;
            this.Controls.Add(CCLGridRefer);
            this.CCLGridRefer.CellDoubleClick += (sender, e) =>
            {
                updateSalesTaxCode(sender, e);
            };
        }
        private void LoadCreditCodeList()
        {
            string filter = "";
            var refreshData = CreditCodesModelObj.LoadCreditCodeData(filter);
            if (refreshData)
            {
                CCLGridRefer.DataSource = CreditCodesModelObj.CreditCodeDataList;
                CCLGridRefer.Columns[0].Visible = false;
                CCLGridRefer.Columns[1].HeaderText = "Credit Code";
                CCLGridRefer.Columns[1].Width = 400;
                CCLGridRefer.Columns[2].HeaderText = "Payment Allowed";
                CCLGridRefer.Columns[2].Width = 400;
                CCLGridRefer.Columns[3].HeaderText = "Credit Limit";
                CCLGridRefer.Columns[3].Width = 400;
            }
        }
        private void updateSalesTaxCode(object sender, EventArgs e)
        {
            CreditCodesDetail detailModal = new CreditCodesDetail();

            int rowIndex = CCLGridRefer.CurrentCell.RowIndex;

            DataGridViewRow row = CCLGridRefer.Rows[rowIndex];

            int id = (int)row.Cells[0].Value;

            detailModal.setDetails(id);

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadCreditCodeList();
            }
        }
    }
}

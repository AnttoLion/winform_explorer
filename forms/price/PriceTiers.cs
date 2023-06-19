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

namespace mjc_dev.forms.price
{
    public partial class PriceTiers : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkEdits = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);

        private GridViewOrigin priceTireGrid = new GridViewOrigin();
        private DataGridView PTGridRefer;
        private PriceTiersModel PriceTiersModelObj = new PriceTiersModel();

        public PriceTiers() : base("Price Tiers", "Tiers of pricing to be assigned to a customer to calculate what prices they're charged")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[4] { hkAdds, hkDeletes, hkEdits, hkPreviousScreen };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitPriceTierGrid();

            this.VisibleChanged += (s, e) =>
            {
                this.LoadPriceTierList();
            };
        }

        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                PriceTierDetail detailModal = new PriceTierDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadPriceTierList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedPriceTierId = 0;
                if (PTGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in PTGridRefer.SelectedRows)
                    {
                        selectedPriceTierId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = PriceTiersModelObj.DeletePriceTier(selectedPriceTierId);
                if (refreshData)
                {
                    LoadPriceTierList();
                }
            };
            hkEdits.GetButton().Click += (sender, e) =>
            {
                updatePriceTier();
            };
        }

        private void InitPriceTierGrid()
        {
            PTGridRefer = priceTireGrid.GetGrid();
            PTGridRefer.Location = new Point(0, 95);
            PTGridRefer.Width = this.Width;
            PTGridRefer.Height = this.Height - 295;
            this.Controls.Add(PTGridRefer);
            this.PTGridRefer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pricetierGridView_CellDoubleClick);
        }

        private void LoadPriceTierList()
        {
            string filter = "";
            var refreshData = PriceTiersModelObj.LoadPriceTierData(filter);
            if (refreshData)
            {
                PTGridRefer.DataSource = PriceTiersModelObj.PriceTierDataList;
                PTGridRefer.Columns[0].HeaderText = "Price Tier #";
                PTGridRefer.Columns[0].Width = 300;
                PTGridRefer.Columns[1].HeaderText = "Price Tier";
                PTGridRefer.Columns[1].Width = 300;
                PTGridRefer.Columns[2].HeaderText = "Profit Margin";
                PTGridRefer.Columns[2].Width = 600;
                PTGridRefer.Columns[3].Visible = false;
            }
        }

        private void updatePriceTier()
        {
            PriceTierDetail detailModal = new PriceTierDetail();

            int rowIndex = PTGridRefer.CurrentCell.RowIndex;
            DataGridViewRow row = PTGridRefer.Rows[rowIndex];

            int pricetierId = (int)row.Cells[0].Value;
            string name = row.Cells[1].Value.ToString();

            double profitmargin = Convert.ToDouble(row.Cells[2].Value.ToString());
            string pricetiercode = row.Cells[3].Value.ToString();

            detailModal.setDetails(name, profitmargin, pricetiercode, pricetierId);

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadPriceTierList();
            }
        }

        private void pricetierGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            updatePriceTier();
        }
    }
}

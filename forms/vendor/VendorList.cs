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

namespace mjc_dev.forms.vendor
{
    public partial class VendorList : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkEdits = new HotkeyButton("Enter", "Edits", Keys.Enter);
        private HotkeyButton hkPreviousScreen = new HotkeyButton("Esc", "Previous Screen", Keys.Escape);
        private HotkeyButton hkArchivedVendors = new HotkeyButton("F8", "Archived Vendors", Keys.F8);

        private GridViewOrigin VendorListGrid = new GridViewOrigin();
        private DataGridView VGridRefer;
        private DashboardModel model = new DashboardModel();

        public VendorList() : base("Vendor List", "List of vendors")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[5] { hkAdds, hkDeletes, hkEdits, hkPreviousScreen, hkArchivedVendors };
            _initializeHKButtons(hkButtons);
            AddHotKeyEvents();

            InitVendorList();
        }
        private void AddHotKeyEvents()
        {
            hkAdds.GetButton().Click += (sender, e) =>
            {
                VendorDetail detailModal = new VendorDetail();
                if (detailModal.ShowDialog() == DialogResult.OK)
                {
                    LoadVendorList();
                }
            };
            hkDeletes.GetButton().Click += (sender, e) =>
            {
                int selectedVendorId = 0;
                if (VGridRefer.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in VGridRefer.SelectedRows)
                    {
                        selectedVendorId = (int)row.Cells[0].Value;
                    }
                }
                bool refreshData = model.DeleteVendor(selectedVendorId);
                if (refreshData)
                {
                    LoadVendorList();
                }
            };
            hkEdits.GetButton().Click += (sender, e) =>
            {
                updateVendor();
            };
        }

        private void InitVendorList()
        {
            VGridRefer = VendorListGrid.GetGrid();
            VGridRefer.Location = new Point(0, 95);
            VGridRefer.Width = this.Width;
            VGridRefer.Height = this.Height - 295;
            this.Controls.Add(VGridRefer);
            this.VGridRefer.CellDoubleClick += (sender, e) =>
            {
                updateVendor();
            };

            LoadVendorList();
        }

        private void LoadVendorList()
        {
            string filter = "";
            var refreshData = model.LoadVendorData(filter);
            Console.WriteLine(refreshData);
            if (refreshData)
            {
                VGridRefer.DataSource = model.VendorDataList;
                VGridRefer.Columns[0].HeaderText = "Vendor #";
                VGridRefer.Columns[0].Width = 300;
                VGridRefer.Columns[1].HeaderText = "Name";
                VGridRefer.Columns[1].Width = 300;
                VGridRefer.Columns[2].HeaderText = "City";
                VGridRefer.Columns[2].Width = 500;
                VGridRefer.Columns[3].HeaderText = "State";
                VGridRefer.Columns[3].Width = 200;
                VGridRefer.Columns[4].HeaderText = "Phone";
                VGridRefer.Columns[4].Width = 200;
            }
        }

        private void updateVendor()
        {
            VendorDetail detailModal = new VendorDetail();

            int rowIndex = VGridRefer.CurrentCell.RowIndex;

            DataGridViewRow row = VGridRefer.Rows[rowIndex];

            int vendorId = (int)row.Cells[0].Value;
            List<dynamic> vendorData = new List<dynamic>();
            vendorData = model.GetVendorData(vendorId);
            detailModal.setDetails(vendorData, vendorData[0].id);

            if (detailModal.ShowDialog() == DialogResult.OK)
            {
                LoadVendorList();
            }
        }
    }
}

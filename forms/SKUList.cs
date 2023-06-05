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

namespace mjc_dev.forms
{
    public partial class SKUList : GlobalLayout
    {

        private HotkeyButton hkAdds = new HotkeyButton("Ins", "Adds", Keys.Insert);
        private HotkeyButton hkDeletes = new HotkeyButton("Del", "Deletes", Keys.Delete);
        private HotkeyButton hkSelects = new HotkeyButton("Enter", "Selects", Keys.Enter);
        private HotkeyButton hkCrossRefLookup = new HotkeyButton("F2", "Cross Ref Lookup", Keys.F2);
        private HotkeyButton hkView = new HotkeyButton("F3", "View", Keys.F3);
        private HotkeyButton hkAdjustQty = new HotkeyButton("F4", "AdjustQty", Keys.F4);
        private HotkeyButton hkSKUHistory = new HotkeyButton("F5", "SKU History", Keys.F5);
        private HotkeyButton hkProfileHistory = new HotkeyButton("F6", "SKU History", Keys.F6);
        private HotkeyButton hkArchivedSKUs = new HotkeyButton("F8", "Archived SKUs", Keys.F8);

        public SKUList() : base("SKU List", "Select a held order to open")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[9] { hkAdds, hkDeletes, hkSelects, hkCrossRefLookup, hkView, hkAdjustQty, hkSKUHistory, hkProfileHistory, hkArchivedSKUs };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
        }
    }
}

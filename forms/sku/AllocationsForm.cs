using mjc_dev.common;
using mjc_dev.common.components;
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
    public partial class AllocationsForm : GlobalLayout
    {
        private HotkeyButton hkPrevScreen = new HotkeyButton("Esc", "Previous screen", Keys.Escape);
        private HotkeyButton OpenOrder = new HotkeyButton("Enter", "Open order", Keys.Enter);


        public AllocationsForm() : base("Allocations for SKU#", "Review held orders with sku quantity allocated")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkPrevScreen, OpenOrder };
            _initializeHKButtons(hkButtons);
        }
    }
}

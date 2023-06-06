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
    public partial class Reorder : GlobalLayout
    {

        private HotkeyButton hkClosesReport = new HotkeyButton("Esc", "Closes Report", Keys.Escape);
        private HotkeyButton hkPrint = new HotkeyButton("F9", "Print", Keys.F9);

        public Reorder() : base("Reorder - SKU List", "SKU that are below critical quantity and need replenished")
        {
            InitializeComponent();
            _initBasicSize();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkClosesReport, hkPrint };
            _initializeHKButtons(hkButtons);
            _addComingSoon();

            foreach (HotkeyButton button in hkButtons)
            {
                if (button != hkClosesReport)
                    button.GetButton().Click += new EventHandler(_hotkeyTest);
            }
            hkClosesReport.GetButton().Click += new EventHandler(_navigateToPrev);
        }
    }
}

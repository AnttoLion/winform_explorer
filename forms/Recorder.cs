﻿using mjc_dev.common.components;
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
    public partial class Recorder : GlobalLayout
    {

        private HotkeyButton hkClosesReport = new HotkeyButton("Esc", "ClosesReport", Keys.Escape);
        private HotkeyButton hkPrint = new HotkeyButton("F9", "Print", Keys.F9);

        public Recorder() : base("SKU List", "SKU that are below critical quantity and need replenished")
        {
            InitializeComponent();

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
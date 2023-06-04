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

namespace mjc_dev.forms
{
    public partial class Dashboard : GlobalLayout
    {
        private HotkeyButton hkCloseProgramButton = new HotkeyButton("Esc", "Close Program", Keys.Escape);
        private HotkeyButton hkOpenSelection = new HotkeyButton("Enter", "Open Selection", Keys.Enter);

        private NavigationButton OrderEntry = new NavigationButton("Order Entry", new OrderEntry());
        private NavigationButton Inventory = new NavigationButton("Inventory", new InventoryDashboard());
        private NavigationButton Receivables = new NavigationButton("Receivables", new ReceivableDashboard());
        private NavigationButton Users = new NavigationButton("Users", new Users());
        private NavigationButton SystemInformation = new NavigationButton("System Information", new SystemSettings());

        private PictureBox hkPicture;
        public Dashboard() : base("Dashboard", "Dashboard view")
        {
            InitializeComponent();

            HotkeyButton[] hkButtons = new HotkeyButton[2] { hkCloseProgramButton, hkOpenSelection };
            _initializeHKButtons(hkButtons);
            this.AddHKEvents();

            NavigationButton[] navButtons = new NavigationButton[5] { OrderEntry, Inventory, Receivables, Users, SystemInformation };
            _initiallizeNavButtons(navButtons);

            SetImage();
        }

        private void AddHKEvents()
        {
            hkCloseProgramButton.GetButton().Click += new EventHandler(_closeProgram);
            hkOpenSelection.GetButton().Enabled = false;
        }

        private void SetImage()
        {
            hkPicture = new PictureBox();
            hkPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            hkPicture.Image = global::mjc_dev.Properties.Resources.hotkeyview;
            hkPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            hkPicture.TabStop = false;
            hkPicture.Location = new System.Drawing.Point(this.Width - hkPicture.Image.Width - 30, 120);
            this.Controls.Add(hkPicture);
        }
    }
}

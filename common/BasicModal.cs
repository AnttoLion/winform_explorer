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

namespace mjc_dev.common
{
    public partial class BasicModal : Form
    {
        public BasicModal()
        {
            InitializeComponent();
            this._initLayout();
        }
        public BasicModal(string title) : base()
        {
            this._initLayout();
            this.Text = title;
        }
        private void _initLayout()
        {
            this.BackColor = System.Drawing.Color.FromArgb(38, 77, 118);
            this.KeyPreview = true;
            this.ShowIcon = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.KeyDown += (s, e) => { 
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                    return;
                }
            };
        }

        protected void ModalButton_HotKeyDown(ModalButton modalButton)
        {
            this.KeyDown += (s, e) => {
                if (e.KeyCode == modalButton.GetKeys())
                {
                    modalButton.GetButton().PerformClick();
                    this.Close();
                    return;
                }
            };
        }
    }
}

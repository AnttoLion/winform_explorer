using mjc_dev.common.components;
using mjc_dev.common;
using mjc_dev.model;
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
    public partial class SKUMemo : BasicModal
    {
        private HotkeyButton hkClose = new HotkeyButton("Esc", "Close", Keys.Escape);

        private FRichTextBox memoTextBox = new FRichTextBox();

        private int priceTierId;
        //private DashboardModel model = new DashboardModel();

        private int SKUId = 0;
        private string memo;

        public SKUMemo() : base()
        {
            InitializeComponent();
        }

        public SKUMemo(int skuId, string memo) : base()
        {
            InitializeComponent();
            _setModalStyle2();
            this.Size = new Size(420, 320);

            this.SKUId = skuId;
            this.memo = memo;

            InitButton();
            InitInputBox();
        }

        private void InitButton()
        {
            hkClose.SetPosition(new Point(20, 230));
            this.Controls.Add(hkClose.GetButton());
            this.Controls.Add(hkClose.GetLabel());
            hkClose.GetButton().Click += (s, e) => { this.Close(); };
        }

        private void InitInputBox()
        {
            memoTextBox.SetPosition(new Point(20, 20));
            memoTextBox.GetTextBox().Size = new Size(360, 190);
            memoTextBox.GetTextBox().Text = this.memo;
            this.Controls.Add(memoTextBox.GetTextBox());
        }

        public string getMemo()
        {
            return this.memoTextBox.GetTextBox().Text;
        }
    }
}

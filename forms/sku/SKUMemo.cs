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
    public partial class SKUMemo : Form
    {
        private HotkeyButton hkClose = new HotkeyButton("Esc", "Close", Keys.Escape);

        private FRichTextBox memoTextBox = new FRichTextBox();

        private int priceTierId;
        private DashboardModel model = new DashboardModel();

        private int SKUId = 0;
        private string memo;

        public SKUMemo() : base()
        {
            InitializeComponent();
        }

        public SKUMemo(int skuId, string memo) : base()
        {
            InitializeComponent();

            this.BackColor = System.Drawing.Color.FromArgb(35, 102, 169);
            this.KeyPreview = true;
            this.ShowIcon = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(420, 320);
            this.StartPosition = FormStartPosition.CenterScreen;


            this.SKUId = skuId;
            this.memo = memo;

            InitButton();
            InitInputBox();
            //LoadMemo();

            hkClose.GetButton().Click += (s, e) => {
                //updateMemo();
                this.Close();
            };

            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                {
                    hkClose.GetButton().PerformClick();
                }
            };
        }

        private void InitButton()
        {
            hkClose.SetPosition(new Point(20, 230));
            this.Controls.Add(hkClose.GetButton());
            this.Controls.Add(hkClose.GetLabel());
        }

        private void InitInputBox()
        {
            memoTextBox.SetPosition(new Point(20, 20));
            memoTextBox.GetTextBox().Size = new Size(360, 190);
            memoTextBox.GetTextBox().Text = this.memo;
            this.Controls.Add(memoTextBox.GetTextBox());
        }

        private void LoadMemo()
        {
            if(this.SKUId != 0)
            {
                List<dynamic> memoData = new List<dynamic>();
                memoData = model.GetSKUData(this.SKUId);

                if(memoData[0] != null)
                {
                    this.memoTextBox.GetTextBox().Text = memoData[0].memo.ToString();
                }
            }
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
        }
        private void updateMemo()
        {
            if(this.SKUId !=0)
            {
                model.UpdateSKUMemo(memoTextBox.GetTextBox().Text, this.SKUId);
            }
        }

        public string getMemo()
        {
            return this.memoTextBox.GetTextBox().Text;
        }
    }
}

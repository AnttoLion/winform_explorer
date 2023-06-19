using mjc_dev.common.components;
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

namespace mjc_dev.common
{
    public partial class SearchInput : BasicModal
    {
        private FInputBox SearchKeyInput = new FInputBox("Search");
        private string initialSearchKey;

        public SearchInput() : base("searchInput", false)
        {
            InitializeComponent();
            _setModalStyle2();

            this.Size = new Size(363, 120);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;

            InitInputBox();

            this.Load += (s, e) => AdjustQty_Load(s, e);
            this.KeyDown += (s, e) => { 
               if(e.KeyCode == Keys.Enter) { 
                    this.Close();
               };
               if (e.KeyCode == Keys.Escape)
               {
                    if(initialSearchKey != GetSearchKey()) SetSearchKey("");
                    this.Close();
               };
            };
        }
        private void AdjustQty_Load(object sender, EventArgs e)
        {
            SearchKeyInput.GetTextBox().Focus();
            initialSearchKey = this.GetSearchKey();
        }

        public void SetSearchKey(string sKey)
        {
            SearchKeyInput.GetTextBox().Text = sKey;
        }

        public string GetSearchKey()
        {
            return SearchKeyInput.GetTextBox().Text.Trim();
        }


        private void InitInputBox()
        {
            SearchKeyInput.GetTextBox().Location = new Point(20, 20);
            SearchKeyInput.GetTextBox().Width = 310;
            this.Controls.Add(SearchKeyInput.GetTextBox());
            SearchKeyInput.GetTextBox().Click += (s, e) =>
            {
                this.Close();
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_winforms
{
    public partial class BasicLayout : Form
    {
        public BasicLayout() : base()
        {
            InitializeComponent();
        }

        public BasicLayout(string title, string formDescription) : base()
        {
            InitializeComponent();
            form_title.Text = title;
            form_description.Text = formDescription;
        }
        private void BasicLayout_Load(object sender, EventArgs e)
        {
            company_name.Text = "Marietta Joint && Cluth";
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(200, 200);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            dateview.Text = DateTime.Today.ToString("MM/dd/yyyy");
            project_version.Text = "v 1.0.0";
            this.WindowState = FormWindowState.Maximized;

            label1.Visible = false;
            this.ActiveControl = label1;

            resetView();
        }
        private void BasicLayout_Resize(object sender, EventArgs e)
        {
            this.resetView();
        }

        private void resetView()
        {
            dateview.Top = 15;
            form_title.Left = (this.ClientSize.Width - form_title.Width) / 2;
            company_name.Location = new Point(this.Width - company_name.Width - 30, 15);
            project_version.Location = new Point(this.Width - project_version.Width - 30, 45);
            form_description.Left = (this.ClientSize.Width - form_description.Width) / 2;
        }
    }
}

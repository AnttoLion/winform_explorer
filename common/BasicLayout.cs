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
    public partial class BasicLayout : Form
    {
        public Form _prevNav;

        // protected font
        protected Font _fontPoint2 = new System.Drawing.Font("Segoe UI Semibold", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        protected Font _fontPoint4 = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        protected Font _fontPoint4_1 = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        protected Font _fontPoint4_2 = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        protected Color _textMainColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

        // protected header & footer
        protected Panel _header; // header panel
        protected Panel _footer; // footer panel

        // windows size
        private int _winHeight = 1920;
        private int _winWidth = 1080;

        // basic components
        private Label _dateView;
        private Label _formTitle;
        private Label _companyName;
        private Label _projectVersion;

        private Label _formDescription;

        public BasicLayout()
        {
            InitializeComponent();
            this._initLayout();
            this._initHeader("Form title");
            this._initFooter("Form Description");
        }
        public BasicLayout(string title, string formDescription) : base()
        {
            this._initLayout();
            this._initHeader(title);
            this._initFooter(formDescription);
        }
        protected void _initBasicSize() { 
            this.Size = new System.Drawing.Size(_winHeight, _winWidth);
        }
        private void _initLayout()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(77)))), ((int)(((byte)(118)))));
            this.KeyPreview = true;
            this.ShowIcon = false;
            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
            this._initBasicSize();
            this.Size = new System.Drawing.Size(_winHeight, _winWidth);
            this.WindowState = FormWindowState.Maximized;
        }
        private void _initHeader(string title)
        {
            //header panel
            this._header = new System.Windows.Forms.Panel();
            this._header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(46)))), ((int)(((byte)(78)))));
            this._header.Dock = System.Windows.Forms.DockStyle.Top;
            this._header.Height = 95;
            //this._header.TabIndex = 0;
            //this._header.TabStop = true;
            this.Controls.Add(this._header);

            //header/_dateview
            this._dateView = new System.Windows.Forms.Label();
            this._dateView.AutoSize = true;
            this._dateView.BackColor = System.Drawing.Color.Transparent;
            this._dateView.Font = this._fontPoint4;
            this._dateView.ForeColor = this._textMainColor;
            this._dateView.Location = new System.Drawing.Point(26, 19);
            this._dateView.Text = DateTime.Today.ToString("MM/dd/yyyy");
            this._header.Controls.Add(this._dateView);

            //header/_formTitle
            this._formTitle = new System.Windows.Forms.Label();
            this._formTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._formTitle.Width = 500;
            this._formTitle.Height = 50;
            this._formTitle.BackColor = System.Drawing.Color.Transparent;
            this._formTitle.Font = _fontPoint2;
            this._formTitle.ForeColor = this._textMainColor;
            this._formTitle.Text = title;
            this._formTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._formTitle.Location = new System.Drawing.Point((this.Width - this._formTitle.Width)/2, 22);
            this._header.Controls.Add(this._formTitle);

            //header/_companyName
            this._companyName = new System.Windows.Forms.Label();
            this._companyName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._companyName.AutoSize = true;
            this._companyName.BackColor = System.Drawing.Color.Transparent;
            this._companyName.Font = this._fontPoint4_1;
            this._companyName.ForeColor = this._textMainColor;
            this._companyName.Text = "Marietta Joint && Clutch";
            this._companyName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._header.Controls.Add(this._companyName);
            this._companyName.Location = new System.Drawing.Point(this.Width - this._companyName.Width - 30, 15);

            //header/_projectVersion
            this._projectVersion = new System.Windows.Forms.Label();
            this._projectVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._projectVersion.AutoSize = true;
            this._projectVersion.BackColor = System.Drawing.Color.Transparent;
            this._projectVersion.Font = this._fontPoint4_2;
            this._projectVersion.ForeColor = this._textMainColor;
            this._projectVersion.Text = "v1.0.0";
            this._header.Controls.Add(this._projectVersion);
            this._projectVersion.Location = new System.Drawing.Point(this.Width - this._projectVersion.Width - 30, 45);
        }
        private void _initFooter(string formDescription)
        {
            this._footer = new System.Windows.Forms.Panel();
            this._footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(46)))), ((int)(((byte)(78)))));
            this._footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footer.Height = 200;
            this.Controls.Add(this._footer);

            //header/_formDescription
            this._formDescription = new System.Windows.Forms.Label();
            this._formDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._formDescription.Width = 700;
            this._formDescription.Height = 50;
            this._formDescription.BackColor = System.Drawing.Color.Transparent;
            this._formDescription.Font = _fontPoint4_2;
            this._formDescription.ForeColor = this._textMainColor;
            this._formDescription.Text = formDescription;
            this._formDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._formDescription.Location = new System.Drawing.Point((this.Width - this._formDescription.Width) / 2, 10);
            this._footer.Controls.Add(this._formDescription);
        }

    }
}

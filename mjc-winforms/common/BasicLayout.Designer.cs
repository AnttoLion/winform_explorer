namespace mjc_winforms
{
    partial class BasicLayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.header_panel = new System.Windows.Forms.Panel();
            this.project_version = new System.Windows.Forms.Label();
            this.company_name = new System.Windows.Forms.Label();
            this.form_title = new System.Windows.Forms.Label();
            this.dateview = new System.Windows.Forms.Label();
            this.form_description = new System.Windows.Forms.Label();
            this.footer_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.header_panel.SuspendLayout();
            this.footer_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // header_panel
            // 
            this.header_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(46)))), ((int)(((byte)(78)))));
            this.header_panel.Controls.Add(this.label1);
            this.header_panel.Controls.Add(this.project_version);
            this.header_panel.Controls.Add(this.company_name);
            this.header_panel.Controls.Add(this.form_title);
            this.header_panel.Controls.Add(this.dateview);
            this.header_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.header_panel.Location = new System.Drawing.Point(0, 0);
            this.header_panel.Name = "header_panel";
            this.header_panel.Size = new System.Drawing.Size(944, 100);
            this.header_panel.TabIndex = 0;
            // 
            // project_version
            // 
            this.project_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.project_version.AutoSize = true;
            this.project_version.BackColor = System.Drawing.Color.Transparent;
            this.project_version.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.project_version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.project_version.Location = new System.Drawing.Point(842, 56);
            this.project_version.Name = "project_version";
            this.project_version.Size = new System.Drawing.Size(81, 30);
            this.project_version.TabIndex = 3;
            this.project_version.Text = "Version";
            // 
            // company_name
            // 
            this.company_name.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.company_name.AutoSize = true;
            this.company_name.Font = new System.Drawing.Font("Segoe UI Semibold", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.company_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.company_name.Location = new System.Drawing.Point(737, 19);
            this.company_name.Name = "company_name";
            this.company_name.Size = new System.Drawing.Size(198, 35);
            this.company_name.TabIndex = 2;
            this.company_name.Text = "Company Name";
            this.company_name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // form_title
            // 
            this.form_title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.form_title.AutoSize = true;
            this.form_title.Font = new System.Drawing.Font("Segoe UI Semibold", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.form_title.Location = new System.Drawing.Point(402, 25);
            this.form_title.Name = "form_title";
            this.form_title.Size = new System.Drawing.Size(155, 42);
            this.form_title.TabIndex = 1;
            this.form_title.Text = "form Title";
            this.form_title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateview
            // 
            this.dateview.AutoSize = true;
            this.dateview.Font = new System.Drawing.Font("Segoe UI Semibold", 18.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dateview.Location = new System.Drawing.Point(26, 19);
            this.dateview.Name = "dateview";
            this.dateview.Size = new System.Drawing.Size(117, 35);
            this.dateview.TabIndex = 0;
            this.dateview.Text = "dateview";
            // 
            // form_description
            // 
            this.form_description.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.form_description.AutoSize = true;
            this.form_description.Font = new System.Drawing.Font("Segoe UI", 18.75F);
            this.form_description.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.form_description.Location = new System.Drawing.Point(351, 17);
            this.form_description.Name = "form_description";
            this.form_description.Size = new System.Drawing.Size(275, 35);
            this.form_description.TabIndex = 4;
            this.form_description.Text = "description of form use";
            this.form_description.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // footer_panel
            // 
            this.footer_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(46)))), ((int)(((byte)(78)))));
            this.footer_panel.Controls.Add(this.form_description);
            this.footer_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footer_panel.Location = new System.Drawing.Point(0, 301);
            this.footer_panel.Name = "footer_panel";
            this.footer_panel.Size = new System.Drawing.Size(944, 200);
            this.footer_panel.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // BasicLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(77)))), ((int)(((byte)(118)))));
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.header_panel);
            this.Controls.Add(this.footer_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "BasicLayout";
            this.ShowIcon = false;
            this.Text = "Frame";
            this.Load += new System.EventHandler(this.BasicLayout_Load);
            this.Resize += new System.EventHandler(this.BasicLayout_Resize);
            this.header_panel.ResumeLayout(false);
            this.header_panel.PerformLayout();
            this.footer_panel.ResumeLayout(false);
            this.footer_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel header_panel;
        private System.Windows.Forms.Label dateview;
        private System.Windows.Forms.Label form_title;
        private System.Windows.Forms.Label company_name;
        private System.Windows.Forms.Label project_version;
        private System.Windows.Forms.Label form_description;
        private System.Windows.Forms.Panel footer_panel;
        private System.Windows.Forms.Label label1;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev
{
    public partial class Form2 : Form
    {
        private DataGridView dataGridView11 = new DataGridView();

        public Form2()
        {
            InitializeComponent();
            this.dataGridView11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView11.Location = new Point(0, 100);
            dataGridView11.Width = this.Width;
            dataGridView11.Height = this.Height - 295;
            dataGridView11.BackgroundColor = Color.FromArgb(215, 231, 246);
            dataGridView11.ReadOnly = true;


            dataGridView11.RowTemplate.Height = 50;
            dataGridView11.EnableHeadersVisualStyles = false;
            dataGridView11.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView11.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(253, 255, 255);
            dataGridView11.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(38, 77, 118);
            dataGridView11.ColumnHeadersDefaultCellStyle.Padding = new Padding(19);
            dataGridView11.BorderStyle = BorderStyle.None;
            
            dataGridView11.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView11.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView11.RowsDefaultCellStyle.Font = new Font("Segoe UI", 18F, FontStyle.Regular);
            dataGridView11.RowsDefaultCellStyle.ForeColor = Color.FromArgb(58, 65, 73);
            dataGridView11.RowsDefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            dataGridView11.RowsDefaultCellStyle.BackColor = Color.FromArgb(215, 231, 246);
            dataGridView11.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(231, 240, 249);
            dataGridView11.RowHeadersVisible = false;

            dataGridView11.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView11.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 227, 130);
            dataGridView11.DefaultCellStyle.SelectionForeColor = Color.FromArgb(58, 65, 73);

            dataGridView11.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);

            // Add some columns to the DataGridView
            dataGridView11.Columns.Add("Column 1", "Header 1");
            dataGridView11.Columns[0].Width = 200;
            dataGridView11.Columns.Add("Column 2", "Header 2");
            dataGridView11.Columns[1].Width = 200;
            dataGridView11.Columns.Add("Column 3", "Header 3");
            dataGridView11.Columns[2].Width = 200;

            // Add some rows to the DataGridView
            dataGridView11.Rows.Add("Row 1 Data 1", "Row 1 Data 2", "Row 1 Data 3");
            dataGridView11.Rows.Add("Row 2 Data 1", "Row 2 Data 2", "Row 2 Data 3");
            dataGridView11.Rows.Add("Row 3 Data 1", "Row 3 Data 2", "Row 3 Data 3");

            // Add the DataGridView to the form controls
            this.Controls.Add(dataGridView11);
        }
    }
}

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
    public partial class Form1 : Form
    {
        private DataGridView dataGridView11 = new DataGridView();
        public Form1()
        {
            TextBox textBox1 = new TextBox();

            textBox1.Location = new Point(50, 50);
            textBox1.Size = new Size(150, 20);
            textBox1.Font = new Font("Segeo UI", 20);

            this.Controls.Add(textBox1);
        }
    }
}

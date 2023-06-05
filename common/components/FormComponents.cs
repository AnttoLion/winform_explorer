using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common.components
{
    public class FInputBox
    {
        private TextBox textBox;
        private Label label;

        public FInputBox(string labeltext)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(150, 31);

            textBox = new TextBox();
            textBox.Size = new Size(300, 20);
            textBox.Font = new Font("Segoe UI Semibold", 15.75F);
        }

        public TextBox GetTextBox()
        {
            return textBox;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetPosition(Point location)
        {
            label.Location = location;
            textBox.Location = new Point(location.X + 200, location.Y);
        }
    }

    public class FDateTime
    {
        private DateTimePicker calendar;
        private Label label;

        public FDateTime(string labeltext)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(150, 31);

            calendar = new DateTimePicker();
            calendar.Size = new Size(300, 20);
            calendar.Font = new Font("Arial", 15.75F);
            calendar.Format = DateTimePickerFormat.Custom;
            calendar.CustomFormat = "MMMd, yyyy";
        }

        public DateTimePicker GetDateTimePicker()
        {
            return calendar;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetPosition(Point location)
        {
            label.Location = location;
            calendar.Location = new Point(location.X + 200, location.Y);
        }
    }

    public class FComboBox
    {
        private ComboBox comboBox;
        private Label label;

        public FComboBox(string labeltext)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(150, 31);

            comboBox = new ComboBox();
            comboBox.Size = new Size(300, 20);
            comboBox.Font = new Font("Segoe UI Semibold", 15.75F);
        }

        public ComboBox GetComboBox()
        {
            return comboBox;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetPosition(Point location)
        {
            label.Location = location;
            comboBox.Location = new Point(location.X + 200, location.Y);
        }
    }

    public class FCheckBox
    {
        private CheckBox checkBox;
        private Label label;
        public FCheckBox(string labeltext)
        {
            checkBox = new CheckBox();
            //checkBox.Location = new Point(50, 50);
            checkBox.AutoSize = true;
            checkBox.Font = new Font("Segoe UI Semibold", 15.75F);

            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.AutoSize = true;
            label.AutoSize = true;
        }

        public CheckBox GetCheckBox()
        {
            return checkBox;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetPosition(Point location)
        {
            checkBox.Location = new Point(location.X + 10, location.Y + 10);
            label.Location = new Point(location.X + 30, location.Y);
        }
    }

    public class FGroupLabel
    {
        private Label label;
        public FGroupLabel(string labeltext)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 16.75F);
            label.ForeColor = System.Drawing.Color.DarkGray;
            label.AutoSize = true;
            label.AutoSize = true;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetPosition(Point location)
        {
            label.Location = location;
        }
    }
}

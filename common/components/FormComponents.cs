using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common.components
{
    internal static class ComponentsConstants
    {
        public static Color FocusedColor = Color.FromArgb(255, 255, 204);
    }
    public class ModalButton
    {
        private Button button;
        private Label label;
        protected Keys hotKey;

        public ModalButton(string text, Keys hotKey)
        {
            this.hotKey = hotKey;

            button = new Button();
            button.Text = text;

            button.AutoSize = true;
            button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            button.BackColor = Color.FromArgb(223, 223, 223);
            button.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold);
            button.ForeColor = System.Drawing.Color.DimGray;
            button.Margin = new System.Windows.Forms.Padding(0);
            button.Size = new System.Drawing.Size(53, 32);
            button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.hotKey = hotKey;
        }

        public Button GetButton()
        {
            return button;
        }

        public Keys GetKeys()
        {
            return hotKey;
        }

        public void SetPosition(Point location)
        {
            button.Location = location;
            label.Location = new Point(location.X + button.Width + 8, location.Y);
        }
    }
    
    public class FInputBox
    {
        private TextBox textBox;
        private Label label;

        public FInputBox(string labeltext, int labelWidth = 200)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(labelWidth, 31);

            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            textBox = new TextBox();
            textBox.Size = new Size(300, 20);
            textBox.Font = new Font("Segoe UI Semibold", 15.75F);
            textBox.BackColor = Color.FromArgb(236, 242, 249);

            textBox.Enter += (s, e) =>
            {
                label.ForeColor = ComponentsConstants.FocusedColor;
            };
            textBox.LostFocus += (s, e) =>
            {
                label.ForeColor = System.Drawing.Color.WhiteSmoke;
            };
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
            textBox.Location = new Point(location.X + label.Width, location.Y);
        }
    }

    public class FDateTime
    {
        private DateTimePicker calendar;
        private Label label;

        public FDateTime(string labeltext, int labelWidth = 200)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(labelWidth, 31);

            calendar = new DateTimePicker();
            calendar.Size = new Size(300, 20);
            calendar.Font = new Font("Arial", 15.75F);
            calendar.Format = DateTimePickerFormat.Custom;
            calendar.CustomFormat = "MMMd, yyyy";
            calendar.BackColor = Color.Red;

            calendar.Enter += (s, e) =>
            {
                label.ForeColor = ComponentsConstants.FocusedColor;
            };
            calendar.LostFocus += (s, e) =>
            {
                label.ForeColor = System.Drawing.Color.WhiteSmoke;
            };
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
            calendar.Location = new Point(location.X + label.Width, location.Y);
        }
    }

    public class FComboBox
    {
        private ComboBox comboBox;
        private Label label;

        public FComboBox(string labeltext, int labelWidth = 200)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(labelWidth, 31);

            comboBox = new ComboBox();
            comboBox.Size = new Size(300, 20);
            comboBox.Font = new Font("Segoe UI Semibold", 15.75F);
            comboBox.BackColor = Color.FromArgb(236, 242, 249);
            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox.Enter += (s, e) =>
            {
                label.ForeColor = ComponentsConstants.FocusedColor;
            };
            comboBox.LostFocus += (s, e) =>
            {
                label.ForeColor = System.Drawing.Color.WhiteSmoke;
            };
            comboBox.KeyDown += ComboBox_KeyDown;
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboBox.DroppedDown = true;
            }
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
            comboBox.Location = new Point(location.X + label.Width, location.Y);
        }
    }

    public class FComboBoxItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public FComboBoxItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class FCheckBox
    {
        private CheckBox checkBox;
        public FCheckBox(string labeltext)
        {
            checkBox = new CheckBox();
            checkBox.AutoSize = true;
            checkBox.Text = labeltext;
            checkBox.Font = new Font("Segoe UI Semibold", 15.75F);
            checkBox.ForeColor = System.Drawing.Color.WhiteSmoke; 


            checkBox.Enter += (s, e) =>
            {
                checkBox.ForeColor = ComponentsConstants.FocusedColor;
            };
            checkBox.LostFocus += (s, e) =>
            {
                checkBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            };
        }

        public CheckBox GetCheckBox()
        {
            return checkBox;
        }


        public void SetPosition(Point location)
        {
            checkBox.Location = new Point(location.X, location.Y);
        }
    }

    public class FRichTextBox
    {
        private RichTextBox richText;
        public FRichTextBox()
        {
            richText = new RichTextBox();
            richText.AutoSize = true;
            richText.Font = new Font("Segoe UI Semibold", 15.75F);
            //richText.ForeColor = System.Drawing.Color.WhiteSmoke;
        }

        public RichTextBox GetTextBox()
        {
            return richText;
        }


        public void SetPosition(Point location)
        {
            richText.Location = new Point(location.X, location.Y);
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
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 16.75F);
            label.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
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

    public class FlabelConstant
    {
        private Label contant;
        private Label label;

        public FlabelConstant(string labeltext, int labelWidth = 200)
        {
            label = new Label();
            label.Text = labeltext;

            label.AutoSize = true;
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            label.ForeColor = System.Drawing.Color.WhiteSmoke;
            label.Size = new System.Drawing.Size(labelWidth, 31);

            contant = new Label();
            contant.Text = "N/A";

            contant.AutoSize = true;
            contant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            contant.BackColor = System.Drawing.Color.Transparent;
            contant.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            contant.ForeColor = System.Drawing.Color.WhiteSmoke;
            contant.Size = new System.Drawing.Size(200, 31);
        }

        public Label GetConstant()
        {
            return contant;
        }

        public Label GetLabel()
        {
            return label;
        }

        public void SetContext(string constText)
        {
            this.contant.Text = constText;
        }

        public void SetPosition(Point location)
        {
            label.Location = location;
            contant.Location = new Point(location.X + label.Width, location.Y);
        }
    }
}

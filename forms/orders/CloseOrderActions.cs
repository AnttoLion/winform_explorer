using mjc_dev.common.components;
using mjc_dev.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.forms.orders
{
    public partial class CloseOrderActions : BasicModal
    {
        private FInputBox PayPrintInvoice = new FInputBox("1)Pay && Print Invoice");
        private FInputBox PayNoPrint = new FInputBox("2)Pay && Don't Print");
        private FInputBox UpdateHeldOrder = new FInputBox("3)Update Held Order");
        private FInputBox HeldOrderPrintInvoice = new FInputBox("4)Hold Order && Print Invoice");
        private FInputBox UpdateQuote = new FInputBox("5)Update Quote");
        private FInputBox UpdateQuotePrint = new FInputBox("6)Update Quote && Print");
        private FInputBox CancelOrderQuote = new FInputBox("7)Cancel Order/Quote");
        private FInputBox ResumeProcessing = new FInputBox("8)Resume Processing");

        private int saveFlage = 0;
        public CloseOrderActions()
        {
            InitializeComponent();
            //_setModalStyle2();
            this.Size = new Size(400, 400);

            InitForms();

            this.KeyDown += CloseOrderActions_KeyDown;
        }

        private void InitForms()
        {
            int xPos = 30;
            int yPos = 20;
            int yDistance = 40;

            PayPrintInvoice.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(PayPrintInvoice.GetLabel());

            yPos += yDistance;
            PayNoPrint.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(PayNoPrint.GetLabel());

            yPos += yDistance;
            UpdateHeldOrder.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(UpdateHeldOrder.GetLabel());

            yPos += yDistance;
            HeldOrderPrintInvoice.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(HeldOrderPrintInvoice.GetLabel());

            yPos += yDistance;
            UpdateQuote.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(UpdateQuote.GetLabel());

            yPos += yDistance;
            UpdateQuotePrint.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(UpdateQuotePrint.GetLabel());

            yPos += yDistance;
            CancelOrderQuote.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(CancelOrderQuote.GetLabel());

            yPos += yDistance;
            ResumeProcessing.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(ResumeProcessing.GetLabel());
        }

        private void CloseOrderActions_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    this.saveFlage = 1;
                    this.Close();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    this.saveFlage = 2;
                    this.Close();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    this.saveFlage = 3;
                    this.Close();
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    this.saveFlage = 4;
                    this.Close();
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    this.saveFlage = 5;
                    this.Close();
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    this.saveFlage = 6;
                    this.Close();
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    this.saveFlage = 7;
                    this.Close();
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    this.saveFlage = 0;
                    this.Close();
                    break;
            }
        }

        public int GetSaveFlage()
        {
            return this.saveFlage;
        }
    }
}

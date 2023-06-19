using mjc_dev.common;
using mjc_dev.common.components;
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
    public partial class MiscManagement : BasicModal
    {
        private FInputBox VenderCosts = new FInputBox("A) Vendor Costs");
        private FInputBox CrossReferences = new FInputBox("B) Cross References");
        private FInputBox SubAssemblies = new FInputBox("C) Sub-assemblies");
        private FInputBox SKUCostQty = new FInputBox("D) SKU Cost/Qty");
        private FInputBox SerialLotNumbers = new FInputBox("E) Serial/Lot Numbers");
        private FInputBox QuantityDiscounts = new FInputBox("F) Quantity Discounts");
        private FInputBox SKUHistory = new FInputBox("G) SKU History");

        private HotkeyButton hkClose = new HotkeyButton("Esc", "Close", Keys.Escape);

        public MiscManagement()
        {
            InitializeComponent();
            //_setModalStyle2();
            this.Size = new Size(420, 420);

            InitForms();
        }

        private void InitForms()
        {
            int xPos = 100;
            int yPos = 20;
            int yDistance = 40;

            VenderCosts.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(VenderCosts.GetLabel());

            yPos += yDistance;
            CrossReferences.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(CrossReferences.GetLabel());

            yPos += yDistance;
            SubAssemblies.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(SubAssemblies.GetLabel());

            yPos += yDistance;
            SKUCostQty.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(SKUCostQty.GetLabel());

            yPos += yDistance;
            SerialLotNumbers.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(SerialLotNumbers.GetLabel());

            yPos += yDistance;
            QuantityDiscounts.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(QuantityDiscounts.GetLabel());

            yPos += yDistance;
            SKUHistory.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(SKUHistory.GetLabel());

            xPos += 40;
            yPos += yDistance + 10;
            hkClose.SetPosition(new Point(xPos, yPos));
            this.Controls.Add(hkClose.GetButton());
            this.Controls.Add(hkClose.GetLabel());

        }
    }
}

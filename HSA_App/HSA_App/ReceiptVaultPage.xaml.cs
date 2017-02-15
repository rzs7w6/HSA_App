using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class ReceiptVaultPage : ContentPage
    {
        public ReceiptVaultPage()
        {
            InitializeComponent();
        }


		public void handleManual(object sender, EventArgs e)
		{
			manualReceipt.Text = "Handling Manual Receipt";
		}

		public void handleOCR(object sender, EventArgs e)
		{
			captureReceipt.Text = "Doing OCR Shit";
		}
    }
}

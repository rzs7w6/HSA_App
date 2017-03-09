using HSA_App;
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
			//manualReceipt.Text = "Handling Manual Receipt";
		}

        public async void handleOCR(object sender, EventArgs e)
        {
            //captureReceipt.Text = "Doing OCR Shit";
            var cameraProvider = DependencyService.Get<ICameraProvider>();
            if (cameraProvider != null)
            {
                var pictureResult = await cameraProvider.TakePictureAsync();
                //ameraResult result = await pictureResult;
                string file = pictureResult.FilePath;
                manualReceipt.Text = file;
            

        //if (pictureResult != null)
        //{
        //    manualReceipt.Text = "Not null";
        //}
        //else
        //{
        //    manualReceipt.Text = "Null as f";
        //}
        //manualReceipt.Text += "WTF IS IT NULL OR NOT?";
    }
            else
            {
                //manualReceipt.Text += "WTF IS IT NULL OR NOT?";
            }
            //manualReceipt.Text += "WTF IS IT NULL OR NOT? JUST TELL ME";
        }
    }
}

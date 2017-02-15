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

        public async void takePicture(Object sender, EventArgs e)
        {
            var cameraProvider = DependencyService.Get<ICameraProvider>();
            var pictureResult = await cameraProvider.TakePictureAsync();

            if (pictureResult != null)
            {
                //Do stuff
            }
        }
    }
}

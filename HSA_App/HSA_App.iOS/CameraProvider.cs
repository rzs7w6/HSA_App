using HSA_App.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using UIKit;

[assembly: Dependency(typeof(CameraProvider))]
namespace HSA_App.iOS
{
    class CameraProvider : ICameraProvider
    {
        public Task<CameraResult> TakePictureAsync()
        {
            var tcs = new TaskCompletionSource<CameraResult>();

            // Use the camera helper to launch the picker     
            Camera.TakePicture(
                UIApplication.SharedApplication.KeyWindow.RootViewController,
                (imagePickerResult) => {
                // In the callback set the task result to the new picture using
                // tcs.TrySetResult(..)
            });

            return tcs.Task;
        }
    }
}

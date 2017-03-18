﻿using HSA_App.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using UIKit;

[assembly: Dependency(typeof(CameraProvider))]
namespace HSA_App.iOS
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using UIKit;
    using Foundation;

    using HSA_App;

    public class CameraProvider : ICameraProvider
    {
        public string filePath { get; set; }
        public Task<CameraResult> TakePictureAsync()
        {
            var tcs = new TaskCompletionSource<CameraResult>();

            Camera.TakePicture(UIApplication.SharedApplication.KeyWindow.RootViewController, (imagePickerResult) => {

                if (imagePickerResult == null)
                {
                    tcs.TrySetResult(null);
                    return;
                }

                var photo = imagePickerResult.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;

                // You can get photo meta data with using the following
                // var meta = obj.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;

                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string jpgFilename = Path.Combine(documentsDirectory, Guid.NewGuid() + ".jpg");
                NSData imgData = photo.AsJPEG();
                NSError err = null;

                if (imgData.Save(jpgFilename, false, out err))
                {
                    CameraResult result = new CameraResult();
                    result.Picture = ImageSource.FromStream(imgData.AsStream);
                    result.FilePath = jpgFilename;

                    tcs.TrySetResult(result);
                }
                else
                {
                    tcs.TrySetException(new Exception(err.LocalizedDescription));
                }
            });

            return tcs.Task;
        }
    }
}

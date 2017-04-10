using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.ComponentModel;
using System.Diagnostics;

using static System.Diagnostics.Debug;
using System.Runtime.CompilerServices;
using System.IO;

namespace HSA_App
{
    class ViewReceiptViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Receipt> InvoicesFromDB { get; } = new ObservableCollection<Receipt>();

        public string Message { get; set; } = "Hello World!";
        
        Command viewInvoicesCommand = null;
        public Command ViewInvoicesCommand =>
                    viewInvoicesCommand ?? (viewInvoicesCommand = new Command(async () => await ExecuteViewInvoicesCommandAsync()));

        async Task ExecuteViewInvoicesCommandAsync()
        {
            try
            {
                IsBusy = true;

                // 1. Add camera logic.
                await CrossMedia.Current.Initialize();

                MediaFile photo;
                byte[] source; //New
                if (CrossMedia.Current.IsCameraAvailable)
                {
                    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Receipts",
                        Name = "Receipt"
                    });
                }
                else
                {
                    photo = await CrossMedia.Current.PickPhotoAsync();
                }

                if (photo == null)
                {
                    PrintStatus("Photo was null :(");
                    return;
                }
                //New stuff
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        photo.GetStream().CopyTo(memoryStream);
                        photo.Dispose();
                        source = memoryStream.ToArray();
                        Debug.WriteLine(source);
                    }
                    var image = new Image
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(source))
                    };
                    InvoicesFromDB.Add(new Receipt
                    {
                        Total = 0,
                        Photo = image.Source.ToString(),
                        TimeStamp = DateTime.Now
                    });
                }
                //End new stuff

            }
            catch (Exception ex)
            {
                await (Application.Current?.MainPage?.DisplayAlert("Error", $"Something bad happened: {ex.StackTrace}", "OK") ??
                Task.FromResult(true));

                PrintStatus(string.Format("ERROR: {0}", ex.Message));

            }
            finally
            {
                IsBusy = false;
            }

        }


        public void PrintStatus(string helloWorld)
        {
            if (helloWorld == null)
                throw new ArgumentNullException(nameof(helloWorld));

            WriteLine(helloWorld);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                if (busy == value)
                    return;

                busy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Message));
            }
        }

    }
}

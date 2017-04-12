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
    class ReceiptsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Receipt> Invoices { get; } = new ObservableCollection<Receipt>();

        public string Message { get; set; } = "Hello World!";

        Command addInvoiceCommand = null;
        public Command AddInvoiceCommand =>
                    addInvoiceCommand ?? (addInvoiceCommand = new Command(async () => await ExecuteAddInvoiceCommandAsync()));

        async Task ExecuteAddInvoiceCommandAsync()
        {
            double total = 0.0;
            try
            {
                IsBusy = true;

                // 1. Add camera logic.
                await CrossMedia.Current.Initialize();

                MediaFile photo;
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
                else
                {
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        photo.GetStream().CopyTo(memoryStream);
                        bytes =  memoryStream.ToArray();
                    }

                    sources.Add(ImageSource.FromStream(() => new MemoryStream(bytes)));

                }
                

                // 2. Add  OCR logic.
                OcrResults text;

                var client = new VisionServiceClient("ebccaf8faed7407eb5b2108193d7b13a");

				using (var stream = photo.GetStream())
				{
					stream.Seek(0, System.IO.SeekOrigin.Begin);
					text = await client.RecognizeTextAsync(stream);
				}

                var numbers = from region in text.Regions
                              from line in region.Lines
                              from word in line.Words
                              where word?.Text?.Contains("$") ?? false
                              select word.Text.Replace("$", string.Empty);

				foreach (var region in text.Regions)
				{
					foreach (var line in region.Lines)
					{
						var word = string.Join(" ", line.Words.Select(w => w.Text));
						if (word.Contains("$"))
						{
							Debug.WriteLine(word);
						}

					}
				}


                double temp = 0.0;
                total = numbers?.Count() > 0 ?
                        numbers.Max(x => double.TryParse(x, out temp) ? temp : 0) :
                        0;



                PrintStatus($"Found total {total.ToString("C")} " +
                    $"and we had {text.Regions.Count()} regions");


                // 3. Add to data-bound collection.
                Invoices.Add(new Receipt
                {
                    Total = total,
                    Photo = photo.Path,
                    TimeStamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                await (Application.Current?.MainPage?.DisplayAlert("Error",$"Something bad happened: {ex.StackTrace}", "OK") ??
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

        //Dealing with viewing images please
        Command viewInvoiceCommand = null;
        public Command ViewInvoiceCommand =>
                    viewInvoiceCommand ?? (viewInvoiceCommand = new Command(async () => await ExecuteViewInvoiceCommandAsync()));
        List<ImageSource> sources = new List<ImageSource>();
        async Task ExecuteViewInvoiceCommandAsync()
        {
            var viewPage = new ViewReceiptsPage(sources);

            await App.Current.MainPage.Navigation.PushModalAsync(viewPage);
        }

    }
}

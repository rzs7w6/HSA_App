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

		private User user;

		public ReceiptsViewModel(User _user)
		{
			this.user = _user;
		}

		async Task ExecuteAddInvoiceCommandAsync()
		{
			double total = 0.0;
			try
			{
				IsBusy = true;
				ReceiptRest rec = new ReceiptRest();

				// 1. Add camera logic.
				await CrossMedia.Current.Initialize();

				MediaFile photo;

				//ReceiptRest rec = new ReceiptRest();
				if (CrossMedia.Current.IsCameraAvailable)
				{
					photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
					{
						Directory = "Receipts",
						Name = "Receipt",
						CompressionQuality = 25
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
						bytes = memoryStream.ToArray();
						Debug.WriteLine(bytes.Length);
					}
					////Create new user object
					//ReceiptRest rec = new ReceiptRest();
					rec.AccountNumber = user.AccountNumber;
					//rec.AccountNumber = 12345678910;
					//rec.Total = 10;
					//rec.Date = "4-18-2017";
					//string currentDateTime = DateTime.UtcNow.Date.ToString();
					//rec.Date = currentDateTime.Remove(10);

					rec.Image = bytes;

					//var sv = new WebService();
					//var es = sv.RegisterReceipt(rec);
				}


				// 2. Add  OCR logic.
				OcrResults text;
				double tmp;

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
							word = word.Trim('$', ' ');
							try
							{
								tmp = Convert.ToDouble(word);

								if (tmp > total)
								{
									total = tmp;
								}

							}
							catch (Exception ex)
							{
								Debug.WriteLine(ex);
							}
							Debug.WriteLine(word);
						}
					}

				}

				double maybe = 0.0;

				//If we couldn't find any dollar signs
				if(total == 0.0)
				{
					foreach (var region in text.Regions)
				{
					foreach (var line in region.Lines)
					{
						var word = string.Join(" ", line.Words.Select(w => w.Text));


							if (word.Contains(".") == true)
							{

								if (Double.TryParse(word, out maybe))
								{
									if (maybe > total)
									{
										total = maybe;
									}
								}
							}
							
					}

				}
				}

				//CHECK FOR WORD CASH TO BE FOUND ANYWHERE, WE DONT REIMBURSE CASH PURCHASES! 
				foreach (var region in text.Regions)
				{
					foreach (var line in region.Lines)
					{
						var word = string.Join(" ", line.Words.Select(w => w.Text));
						if (word.Contains("Cash ") || word.Contains("CASH ") || word.Contains("cash "))
						{
							Debug.WriteLine("CANNOT PROCESS CASH RECEIPTS");

							return;
						}
					}
				}



				/*double temp = 0.0;
                total = numbers?.Count() > 0 ?
                        numbers.Max(x => double.TryParse(x, out temp) ? temp : 0) :
                        0;*/

				PrintStatus($"Found total {total.ToString("C")} " +
					$"and we had {text.Regions.Count()} regions");
                Receipt receipt = new Receipt
                {
                    Total = total,
                    Photo = photo.Path,
                    TimeStamp = DateTime.Now
                };
                                    
                await App.Current.MainPage.Navigation.PushModalAsync(new UserCorrectsTotal(receipt, Invoices, user, rec));


                // 3. Add to data-bound collection.
                //Invoices.Add(receipt);


                //rec.Total = (float) total;
                //var sv = new WebService();
                //var es = sv.RegisterReceipt(rec);

    //            try
    //            {
    //                var sv = new WebService();

    //                //Withdrawl money from account
    //                Balance balance = await sv.GetBalance(user.AccountNumber);
    //                balance.AccountBalance -= (float)total;

    //                if (balance.AccountBalance < 0)
    //                {
    //                    Debug.WriteLine("You cannot complete this transaction due to insufficent funds");
    //                    return;
    //                }

    //                int b = await sv.UpdateBalance(balance);
    //                if (b == -1)
    //                {
    //                    Debug.WriteLine("Unable to update balance\n");
    //                    return;
    //                }

    //                //Add reciept to Database
    //                //rec.Total = (float)total;
    //                if (Invoices.Count != 0)
    //                {
    //                    rec.Total = (float)Invoices.ElementAt<Receipt>(Invoices.Count - 1).Total;
    //                }
    //                else rec.Total = (float) 6.66;
				//	rec.Date = DateTime.Now.ToString("yyyy-MM-dd");
				//	var es = await sv.RegisterReceipt(rec);

				//	//Add transaction to Databse
				//	Transaction trans = new Transaction();
				//	trans.AccountNumber = user.AccountNumber;
				//	trans.Type = "W";
				//	trans.Date = DateTime.Now.ToString("yyyy-MM-dd");
				//	trans.Amount = total;
				//	int result = await sv.DepositTransaction(trans);

				//	if (result == -1)
				//	{
				//		Debug.WriteLine("Unable to add transaction");
				//	}
				//}
				//catch (Exception ex)
				//{
				//	Debug.WriteLine(ex);
				//}
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

		//Dealing with viewing images please
		Command viewInvoiceCommand = null;
		public Command ViewInvoiceCommand =>
					viewInvoiceCommand ?? (viewInvoiceCommand = new Command(async () => await ExecuteViewInvoiceCommandAsync()));
		List<ImageSource> sources = new List<ImageSource>();
		//private User user;


		async Task ExecuteViewInvoiceCommandAsync()
		{
			//Retrieve from database
			//Start our webservice
			var sv = new WebService();

			//Getuser object back based on username
			List<ReceiptRest> receipt = await sv.GetReceipts(user.AccountNumber);

			if (receipt == null)
			{
				Debug.WriteLine("Receipt was null");
				return;
				//display.Text = "Invalid login information!";
			}
			//Clear sources
			sources.Clear();
			//Add to sources
			foreach (ReceiptRest index in receipt)
			{
				sources.Add(ImageSource.FromStream(() => new MemoryStream(index.Image)));
			}
			var viewPage = new ViewReceiptsPage(sources, receipt);

			await App.Current.MainPage.Navigation.PushModalAsync(viewPage);
		}

		Command RefreshCommandwait = null;
		public Command RefreshComm =>
				RefreshCommandwait ?? (RefreshCommandwait = new Command(() => RefreshCommand()));

		async Task RefreshCommand()
		{
			Debug.WriteLine("MADE IT\n");
			App.Current.MainPage = new NavigationPage(new NavigationLocal(user));
		}
	}
}
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
    public class ViewReceiptsPage : ContentPage
    { 
        private async Task ExecuteZoomCommandAsync(string param)
        {
            Debug.WriteLine(param);
        }

        TableRoot tableRoot = new TableRoot("Receipts");

        public ViewReceiptsPage(List<ImageSource> sources, List<ReceiptRest> receipts)
        {

			try
			{
				tableRoot.Add(new TableSection
			{
				new buttonCell { }
			});

				foreach (ImageSource source in sources)
				{
					tableRoot.Add(new TableSection(receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Date)
				{
					new ImageCell
					{
                        //ImageSource = source,
                        Text = "Total: " + receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Total.ToString(),
						TextColor = Color.FromHex("#0A3079"),
						CommandParameter = source,
						Command = new Command<ImageSource>(execute: (ImageSource image) =>
						{
							App.Current.MainPage.Navigation.PushModalAsync(new ImagePage(image, receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Total.ToString(), receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Date));
						})
					}
				});

				}
				Content = new TableView { Root = tableRoot };
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}

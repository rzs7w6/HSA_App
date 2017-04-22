using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
    
    public class ViewReceiptsPage : ContentPage
    {
        TableRoot tableRoot = new TableRoot("Receipts");

        public ViewReceiptsPage(List<ImageSource> sources, List<ReceiptRest> receipts)
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
                        ImageSource = source,
                        Text = "Total: " + receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Total.ToString(),
                        TextColor = Color.FromHex("#0A3079")
                    }
                });

            }
            Content = new TableView { Root = tableRoot };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}

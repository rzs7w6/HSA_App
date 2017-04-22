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
        User me = new User();
        StackLayout parent = null;
        TableRoot tableRoot = new TableRoot("Receipts");

        public ViewReceiptsPage(List<ImageSource> sources, List<ReceiptRest> receipts)
        {
            
            parent = new StackLayout();

            Button button = new Button
            {
                Text = "< Back",
                HeightRequest = 45,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,   
            };
            parent.Children.Add(button);
            button.Clicked += OnButtonClicked;
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
                //Image image = new Image
                //{
                //    Source = source
                //};
                
                //Label date = new Label
                //{
                //    Text = receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Date
                //};
                
                //Label total = new Label
                //{
                //    Text = receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Total.ToString()
                //};
                //parent.Children.Add(image);
                //parent.Children.Add(date);
                //parent.Children.Add(total);
            }

            //Content = new ScrollView { Content = parent };
            Content = new TableView { Root = tableRoot };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HSA_App
{
    
    public class ViewReceiptsPage : ContentPage
    {
        User me = new User();
        StackLayout parent = null;

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
                Image image = new Image
                {
                    Source = source
                };
                
                Label date = new Label
                {
                    Text = receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Date
                };
                
                Label total = new Label
                {
                    Text = receipts.ElementAt<ReceiptRest>(sources.IndexOf(source)).Total.ToString()
                };
                parent.Children.Add(image);
                parent.Children.Add(date);
                parent.Children.Add(total);
            }

            Content = new ScrollView { Content = parent };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
        }
    }
}

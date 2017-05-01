using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
    public class UserCorrectsTotal : ContentPage
    {
        Receipt receipt;
        Entry entry;

        public UserCorrectsTotal(ref Receipt receipt)
        {
            this.receipt = receipt;

            Button button = new Button();
            button.Text = "Return";
            button.VerticalOptions = LayoutOptions.Center;
            button.Clicked += OnButtonClicked;

            entry = new Entry();
            entry.Placeholder = "Total:";
            entry.Text = receipt.Total.ToString();

            Content = new StackLayout
            {

                Children = {
                    new Label { Text = "Enter Your Correct Total Below:" },
                    entry,
                    button
                }
            };
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            Double total;
            if (!Double.TryParse(entry.Text, out total))
            {
                await DisplayAlert("Error", "Invalid total. Try again.", "OK");
                return;
            }
            else
            {
                receipt.Total = total;
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }
    }
}

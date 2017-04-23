using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
    class buttonCell : ViewCell
    {
        public buttonCell()
        {
            Button button = new Button();

            button.Text = "Return";
            button.VerticalOptions = LayoutOptions.Center;

            button.Clicked += OnButtonClicked;

            var nameLabel = new Label();
            nameLabel.SetBinding(Label.TextProperty, "name");
            nameLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
            nameLabel.VerticalOptions = LayoutOptions.Center;

            var viewLayout = new StackLayout()
            {
                //Padding = new Thickness(10, 0, 10, 0),
                Orientation = StackOrientation.Horizontal,
                Children = { button }
            };

            View = viewLayout;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}

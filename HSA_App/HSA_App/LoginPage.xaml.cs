using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void handleLogin(object sender, EventArgs e)
        {
            //if (username.Equals("rzs7w6") && password.Equals("password"))
            //{
            //    App.Current.MainPage = new NavigationPage();
            //}
            display.Text = password.Text + " " + username.Text;
        }
    }
}

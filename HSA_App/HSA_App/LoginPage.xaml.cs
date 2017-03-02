using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
	public partial class LoginPage : ContentPage, RegistrationParser
	{
		public LoginPage()
		{
			InitializeComponent();
		}

        public int checkAccount(string actNum)
        {
            throw new NotImplementedException();
        }

        public int checkName(string actName)
        {
            throw new NotImplementedException();
        }

        public int checkPassword(string actPass)
        {
            throw new NotImplementedException();
        }

        public void handleLogin(object sender, EventArgs e)
		{
            
			if (username.Text.Equals("test") && password.Text.Equals("password"))
			{
				App.Current.MainPage = new NavigationPage();
			}
			else
			{
				display.Text = "Incorrect Username and/or Password";
				display.TextColor = Color.Red;
			}
		}

		public void handlePasswordHelp(object sender, EventArgs e)
		{
			display.Text = "You have requested help with your username/password";

		}
	}
}

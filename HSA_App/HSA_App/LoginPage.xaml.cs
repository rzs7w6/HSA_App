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

        public int checkPassword(string actPass)
		{
			bool upper = false;
			bool lower = false;
			bool number = false;
			bool special = false;

			foreach (char letter in actPass)
			{
				if (System.Char.IsDigit(letter))
				{
					number = true;
				}

				if (System.Char.IsLower(letter))
				{
					lower = true;
				}

				if (System.Char.IsUpper(letter))
				{
					upper = true;
				}

				if (System.Char.IsSymbol(letter) || System.Char.IsPunctuation(letter))
				{
					special = true;
				}
			}

			if (upper == false || lower == false || number == false || special == false)
			{
				return -1;
			}
			else
			{
				return 1;
			}

		}

        public void handleLogin(object sender, EventArgs e)
		{
			//TESTING PURPOSES!!
			if (username.Text.Equals("test") && password.Text.Equals("password"))
			{
				App.Current.MainPage = new NavigationPage();
			}

			//Good To go! The password contains all necassary components to be a password
			if (checkPassword(password.Text) == -1)
			{
				display.Text = "Incorrect Password!";
				display.TextColor = Color.Red;

				password.BackgroundColor = Color.Red;
			}
			else
			{
				display.Text = "";
				display.TextColor = Color.White;

				password.BackgroundColor = Color.White;
			}

			//ADD QUERY LOGIC HERE TO TEST PROPER USERNAME/PASSWORD COMBO!!!
		}


		public void handleRegister(object sender, EventArgs e)
		{
			App.Current.MainPage = new RegistrationPage();
		}


		public void handlePasswordHelp(object sender, EventArgs e)
		{
			display.Text = "You have requested help with your username/password";

		}
	}
}

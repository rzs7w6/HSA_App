using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;

using Xamarin.Forms;

namespace HSA_App
{
	public partial class LoginPage : ContentPage
	{
        //public NavigationLocal navigation = new NavigationLocal(user);

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

        public async void handleLogin(object sender, EventArgs e)
		{
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

				//Start our webservice
				var sv = new WebService();

				//Getuser object back based on username
				User user = await sv.GetUsers(username.Text, password.Text);

                if(user == null)
                {
                    display.Text = "Invalid login information!";
                }
                //Debug.WriteLine("just want to check user");
                /*
				//Create byte array from string
				byte[] EncryptedPass = Encoding.UTF8.GetBytes(user.HashedPassword);
				byte[] Salt = Encoding.UTF8.GetBytes(user.Salt);

				//Decrypt That hoe
				byte[] encrypted = Crypto.EncryptAes(password.Text, password.Text,Salt);

				//Unecrypted Password
				Debug.WriteLine(Encoding.UTF8.GetString(encrypted,0,encrypted.Length));
				Debug.WriteLine(user.HashedPassword);

				if (user != null)
				{
					App.Current.MainPage = new NavigationPage(new NavigationLocal(user));
				}
				else
				{
					display.Text = "Invalid login information!";
				}*/

                if (user.UserName.Equals(username.Text))
                {
                    App.Current.MainPage = new NavigationPage(new NavigationLocal(user));
                }
                else
                {
                    display.Text = "Invalid login information!";
                }

			}
        }


		public void handleRegister(object sender, EventArgs e)
		{
			App.Current.MainPage = new RegistrationPage();
		}


		public void handlePasswordHelp(object sender, EventArgs e)
		{
			display.Text = "You have requested help with your username/password";
            EmailHandler.sendForgotPasswordEmail("zane.spalding@gmail.com");
		}
	}
}
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HSA_App
{
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage()
		{
			InitializeComponent();
		}

		public void handleReg(object sender, EventArgs e)
		{
			int successFlag;
			successFlag = 1;

			if (successFlag == 0)
			{
				username.Text = "USERNAME ALREADY TAKEN";
				username.TextColor = Color.Red;
			}
			else
			{
				App.Current.MainPage = new LoginPage();
			}
		}
	}
}

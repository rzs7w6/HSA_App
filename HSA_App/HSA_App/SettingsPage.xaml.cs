using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
	public partial class SettingsPage : ContentPage
	{
		User me;

		public SettingsPage(User user)
		{
			InitializeComponent();
			me = user;
		}

		public void handleLogout(object sender, EventArgs e)
		{
			me = null;
			App.Current.MainPage = new LoginPage();
		}
		public async void handleUpdateAccount(object sender, EventArgs e)
		{
			var amount = deposit.Text;
			float dollar = 0;

			try
			{
				dollar = (float)Convert.ToDouble(amount);
				display.Text = "";

				var sv = new WebService();

				Balance balance = await sv.GetBalance(me.AccountNumber);
				balance.AccountBalance += dollar;

				int result = await sv.UpdateBalance(balance);
				if (result == 1)
				{
					display.TextColor = Color.Green;
					display.Text = "New Account balance is: " + balance.AccountBalance.ToString();
					App.Current.MainPage = new NavigationPage(new NavigationLocal(me));
				}
				else
				{
					display.TextColor = Color.Red;
					display.Text = "Somthing went wrong with the Web service Class -- Team Assemble! -- ";
				}

			}
			catch (Exception ex)
			{
				display.TextColor = Color.Red;
				display.Text = "That's not a dollar amount";
			}

		}
	}
}
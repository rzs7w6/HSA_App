using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
				if (dollar > 5000.00)
				{
					display.TextColor = Color.Red;
					display.Text = "Anual Limit of $5,000!";
				}
				else
				{
					display.Text = "";

					var sv = new WebService();

					Balance balance = await sv.GetBalance(me.AccountNumber);
					balance.AccountBalance += dollar;

					//CHECK TO MAKE SURE THEY HAVEN'T ALREADY DEPOSITED MAX AMOUNT BEFORE YOU POST TO UPDATE BALANCE
					double depoamount = await sv.GetDepoData(me.AccountNumber);

					if ((depoamount+dollar) > 5000.00)
					{
						display.TextColor = Color.Red;
						display.Text = "You have Deposited $" + depoamount.ToString() + ".00 in the last 365 days. Your limit is $5,000 anually";
						return;
					}
					else
					{
						Debug.WriteLine("You've Deposited: " + depoamount);
					}

					Transaction trans = new Transaction();
					trans.AccountNumber = me.AccountNumber;
					trans.Type = "D";
					trans.Amount = (double) dollar;

					int depo = await sv.DepositTransaction(trans);

					if (depo == -1)
					{
						Debug.WriteLine("DEPO DIDNT WORK\n");
						return;
					}

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

			}
			catch (Exception ex)
			{
				display.TextColor = Color.Red;
				display.Text = "That's not a dollar amount";
			}

		}
	}
}
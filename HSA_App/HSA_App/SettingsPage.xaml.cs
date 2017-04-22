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
			double dollar = 0;

			try
			{
				dollar = Convert.ToDouble(amount);
				display.Text = "";
			}
			catch(Exception ex)
			{
				display.TextColor = Color.Red;
				display.Text = "That's not a dollar amount";
			}

			try
			{
				var sv = new WebService();

				Balance balance = await sv.GetBalance(me.AccountNumber);

				double currentAmount = balance.AccountBalance;

				double newBalance = currentAmount + dollar;

			}
			catch(Exception ex)
			{
				display.Text = "Error Occured when trying to update your account balance";
			}

        }
    }
}
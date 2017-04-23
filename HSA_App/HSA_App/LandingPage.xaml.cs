using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace HSA_App
{
	public partial class LandingPage : ContentPage
	{
		User me;

		public LandingPage(User user)
		{
<<<<<<< HEAD
			//InitializeComponent();
			//accountBalance.Text = "$3000.00";
=======
			if (user == null)
				Debug.WriteLine("\n\n\n\n\nTHE USER IS NULL\n\n\n\n");
>>>>>>> master
			InitializeComponent();
			getBalance(user);
			me = user;
<<<<<<< HEAD
			accountBalance.Text = "$3000.00";
           	 	CouponListPage();
=======
			//CouponListPage();
			TransactionListPage();
		}

		private async void getBalance(User user)
		{
			var sv = new WebService();

			Balance balance = await sv.GetBalance(user.AccountNumber);

			accountBalance.Text = balance.AccountBalance.ToString();
>>>>>>> master

			if (balance == null)
			{
				accountBalance.Text = "$0.00";
			}
			else
			{
				accountBalance.Text = "$" + balance.AccountBalance.ToString();
			}
		}

		/*ObservableCollection<Coupon> coupons = new ObservableCollection<Coupon>();
        private void CouponListPage()
        {
            //defined in XAML to follow
            TransactionView.ItemsSource = coupons;
            coupons.Add(new Coupon { StoreName = "CVS", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Walmart", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Walgreens", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Drug Store #1", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Rite Aide", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "HyVee", CouponDetails = "$10" });
        }*/


		private async void TransactionListPage()
		{
			ObservableCollection<Transaction> trans = new ObservableCollection<Transaction>();

			//defined in XAML to follow
			TransactionView.ItemsSource = trans;

			var sv = new WebService();

			List<Transaction> transactionlist = await sv.GetTransactions(me.AccountNumber);
            if (transactionlist == null || transactionlist.Count == 0)
            {
                trans.Add(new Transaction { Type = "NO TRANSACTION HISTORY", Date = "Blue" });
                return;
            }
			int i = 0;
			int j = 0;
			string type = "";
			string date = "";
			foreach (Transaction t in transactionlist){
				Debug.WriteLine(t.Amount);

				if(t.Type == "D")
				{
					for (j = 0; j < 10; j++)
					{
						date += t.Date[j];
					}

					type = "DEPOSIT -- Amount: $" + t.Amount + " Date: " + date + "--";
					trans.Add(new Transaction { Type = type, Date = "Green" });
					date = "";
				}
				else
				{	
					for (j = 0; j < 10; j++)
					{
						date += t.Date[j];
					}

					type = "WITHDRAWL -- Amount: $" + t.Amount + " Date: " + date + "-- ";
					trans.Add(new Transaction { Type = type, Date = "Red" });
					date = "";
				}

				i++;
			}

			if (i == 0)
			{
				trans.Add(new Transaction { Type = "NO TRANSACTION HISTORY", Date = "Blue" });
			}
		}
	}
}
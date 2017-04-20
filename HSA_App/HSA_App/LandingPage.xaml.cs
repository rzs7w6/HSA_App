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
            if (user == null)
                Debug.WriteLine("\n\n\n\n\nTHE USER IS NULL\n\n\n\n");
			InitializeComponent();
			//me = user;
			//accountBalance.Text = "$3000.00";
            getBalance(user);
            
            CouponListPage();

		}

        private async void getBalance(User user)
        {
            var sv = new WebService();

            Balance balance = await sv.GetBalance(user.AccountNumber);

            accountBalance.Text = balance.AccountBalance.ToString();

            if(balance == null)
            {
                accountBalance.Text = "0";
            }
            else
            {
                accountBalance.Text = balance.AccountBalance.ToString();
            }
        }

        ObservableCollection<Coupon> coupons = new ObservableCollection<Coupon>();
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
        }
    }
}

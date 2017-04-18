using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HSA_App
{
	public partial class LandingPage : ContentPage
	{
		User me;

		public LandingPage(User user)
		{
			InitializeComponent();
			me = user;
			accountBalance.Text = "$3000.00";
            CouponListPage();

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

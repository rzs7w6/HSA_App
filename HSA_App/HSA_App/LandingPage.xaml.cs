using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HSA_App
{
	public partial class LandingPage : ContentPage
	{
		public LandingPage()
		{
			//InitializeComponent();
			//accountBalance.Text = "$3000.00";
			InitializeComponent();

			accountBalance.Text = "$3000.00";
           	 	CouponListPage();

		}
        ObservableCollection<Coupon> coupons = new ObservableCollection<Coupon>();
        private void CouponListPage()
        {
            //defined in XAML to follow
            TransactionView.ItemsSource = coupons;
            coupons.Add(new Coupon { StoreName = "cVs", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Bitchmart", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "WalGREEN", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Drug Store #1", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "Rite Aids", CouponDetails = "$10" });
            coupons.Add(new Coupon { StoreName = "HighMee", CouponDetails = "$10" });
        }
    }
}

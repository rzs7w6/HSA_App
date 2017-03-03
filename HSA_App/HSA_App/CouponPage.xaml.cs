using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class CouponPage : ContentPage
    {

        public CouponPage()
        {
            InitializeComponent();

        }


		public void handleCoupon(object sender, EventArgs e)
		{
			couponButton.Text = "Found coupons";
		}


    
	
	}

}

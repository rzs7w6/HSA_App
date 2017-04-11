using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HSA_App
{
    public class Navigation : TabbedPage
    {
        public Navigation()
        {
            this.Title = "UMB";
			this.Children.Add(new LandingPage());
            this.Children.Add(new RecieptPage());
            this.Children.Add(new CouponPage());
            this.Children.Add(new SettingsPage());
            this.Children.Add(new RecieptVaultPage());
        }
    }
}

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
        public Navigation(User user)
        {
            this.Title = "UMB";
			this.Children.Add(new LandingPage(user));
			this.Children.Add(new RecieptPage(user));
			this.Children.Add(new CouponPage(user));
			this.Children.Add(new SettingsPage(user));
			this.Children.Add(new RecieptVaultPage(user));
        }
    }
}

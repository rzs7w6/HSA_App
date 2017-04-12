using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HSA_App
{
    public class NavigationLocal : TabbedPage
    {
        public NavigationLocal()
        {
            this.Title = "UMB";
			this.Children.Add(new LandingPage());
            this.Children.Add(new RecieptPage());
            this.Children.Add(new CouponPage());
            this.Children.Add(new SettingsPage());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HSA_App
{
    public class NavigationPage : TabbedPage
    {
        public NavigationPage()
        {
            this.Title = "Tabbed Page";
            this.Children.Add(new ReceiptVaultPage());
            this.Children.Add(new CouponPage());
            this.Children.Add(new SettingsPage());
			this.Children.Add(new RegistrationPage());
        }
    }
}

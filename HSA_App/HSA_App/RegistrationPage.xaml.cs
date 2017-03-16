using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;

namespace HSA_App
{
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage()
		{
			InitializeComponent();
		}

		public void handleReg(object sender, EventArgs e)
		{
            Debug.WriteLine("\n\n\nGot into the function\n\n\n");
			int successFlag;
			successFlag = 1;
            /*
			if (successFlag == 0)
			{
				username.Text = "USERNAME ALREADY TAKEN";
				username.TextColor = Color.Red;
			}
			else
			{
				App.Current.MainPage = new LoginPage();
			}*/
            
            //regButton.Clicked += async (thissender, thise) =>
            //{

                User person = new User();
                person.FirstName = fname.Text;
                person.LastName = lname.Text;
                person.UserName = username.Text;
                person.AccountNumber = Convert.ToInt32(accountNum.Text);
                //person.Birthday = Convert.ToDateTime(dobLabel.Text);
                person.HashedPassword = password2.Text;
                Debug.WriteLine("\n\n\n\ngot here\n\n\n\n");
                var sv = new WebService();
                var es = sv.RegisterUser(person);
                App.Current.MainPage = new LandingPage();
            //Debug.WriteLine("\n\nfound " + es.Length + " users\n\n");
            //};
        }
	}
}

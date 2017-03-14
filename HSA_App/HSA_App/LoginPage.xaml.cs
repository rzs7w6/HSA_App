using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;


using Xamarin.Forms;

namespace HSA_App
{
	public partial class LoginPage : ContentPage
	{

        CoManager Conn = new HSA_App.CoManager();

        public LoginPage()
		{
			InitializeComponent();
        }

        public int checkAccount(string actNum)
        {
            throw new NotImplementedException();
        }

        public int checkName(string actName)
        {
            throw new NotImplementedException();
        }

        public int checkPassword(string actPass)
        {
            throw new NotImplementedException();
        }

        public async void handleLogin(object sender, EventArgs e)
        {



          

            if (username.Text.Equals("test") && password.Text.Equals("password"))
            {
                App.Current.MainPage = new NavigationPage();
            }
            else
            {
                display.Text = "Incorrect Username and/or Password";
                display.TextColor = Color.Red;


                var code = await Conn.GetRest(string.Format("/api/user/'" + username.Text + "'"));
                display.Text = code;
                //System.Diagnostics.Debug.WriteLine(code);

                var jo = JObject.Parse(code);
                var pass = jo["HashedPassword"];

                System.Diagnostics.Debug.WriteLine(pass);


                   

                //DBContext.AddUser(new User { ..., Password = savedPasswordHash });


                /*
                var code = await Conn.GetRest(string.Format("/api/user/{0}", 3));
                display.Text = code;
                System.Diagnostics.Debug.WriteLine(code);
                */
            }

        }

        public void handlePasswordHelp(object sender, EventArgs e)
		{
			display.Text = "You have requested help with your username/password";

		}
	}
}

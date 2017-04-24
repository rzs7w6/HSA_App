using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class ForgotEmailPage : ContentPage
    {
        private static Random random = new Random();

        public string RandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            string password = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            password += "!1Q";
            return password;
        }

        public ForgotEmailPage()
        {
            InitializeComponent();
        }

        private async void Submit(Object sender, EventArgs e)
        {
            string username = entry.Text;
            //Start our webservice
            var sv = new WebService();

            //Getuser object back based on username
            User user = await sv.GetUsers(username, "");
            if (user == null || user.UserName == null || user.Email == null || user.Email == "" || user.UserName == "")
            {
                await DisplayAlert("Error", "User does not exist. Try again.", "OK");
                return;
            }
            string newPassword = RandomPassword(8);

            if(!await UpdatePassword(newPassword))
            {
                await DisplayAlert("Error", "There was an error in updating your password. Try again.", "OK");
                return;
            }

            await EmailHandler.sendForgotPasswordEmail(user.Email, newPassword, username);
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async Task<bool> UpdatePassword(string newPassword)
        {
            
            return true;
        }
        

    }
}

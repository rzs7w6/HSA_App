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
            UpdatePassword(RandomPassword(8));
            await EmailHandler.sendForgotPasswordEmail(entry.Text);
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void UpdatePassword(string newPassword)
        {
            Debug.WriteLine(newPassword);
        }
    }
}

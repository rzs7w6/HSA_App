﻿using System;
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
            string newPassword = RandomPassword(8);

            await UpdatePassword(newPassword);

            await EmailHandler.sendForgotPasswordEmail(entry.Text, newPassword, username);
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async Task UpdatePassword(string newPassword)
        {
            
            return;
        }
        

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public void handleLogout(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
        public void handleUpdateAccount(object sender, EventArgs e)
        {
            //TODO handle update account information
        }
    }
}

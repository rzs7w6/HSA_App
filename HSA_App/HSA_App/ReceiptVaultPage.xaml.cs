﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class RecieptVaultPage : ContentPage
    {
        public RecieptVaultPage()
        {
            InitializeComponent();

            BindingContext = new ViewReceiptViewModel();

        }
    }
}

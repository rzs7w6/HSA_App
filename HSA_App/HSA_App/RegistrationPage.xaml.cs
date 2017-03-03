using System;
using System.Collections.Generic;

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
			display.Text = "Username available/not available. REGISTRATION IS ON THE WAY!!";
		}
	}
}

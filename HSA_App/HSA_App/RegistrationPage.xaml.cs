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
            dobPicker.SetValue (DatePicker.MaximumDateProperty, DateTime.Now);
		}


        /*Check Account Number

			1.) Ensure the string can be converted to an int
			2.) Ensure the string has the correct length to represent an account number

		*/
        public Int64 checkAccount(String actNum)
		{
			Int64 number;

			//Checks the Account for all numbers (Attempt to convert)
			try
			{
				number = Convert.ToInt64(actNum);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("There was NOT All numbers in your account number!");
				return -1;
			}


			//Check the length of the account to be 11
			try
			{
				if (actNum.Length != 11)
				{
					Debug.WriteLine("Account Number was NOT of length 11!!!\n");
					return -1;
				}

				return number;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				return -1;
			}
		}



		public int checkName(string actName)
		{

			//Check if the name is too long to be stored in our Database
			if (actName.Length > 45 || actName.Length <= 0)
			{
				return -1;
			}

			//Check for numbers in Name
			foreach (char letter in actName)
			{
				if (System.Char.IsDigit(letter))
				{
					return -1;
				}
			}

			return 1;
		}



		public int checkPassword(string actPass)
		{
			bool upper = false;
			bool lower = false;
			bool number = false;
			bool special = false;

			foreach (char letter in actPass)
			{
				if (System.Char.IsDigit(letter))
				{
					number = true;
				}

				if (System.Char.IsLower(letter))
				{
					lower = true;
				}

				if (System.Char.IsUpper(letter))
				{
					upper = true;
				}

				if (System.Char.IsSymbol(letter) || System.Char.IsPunctuation(letter))
				{
					special = true;
				}
			}

			if (upper == false || lower == false || number == false || special == false)
			{
				return -1;
			}
			else
			{
				return 1;
			}

		}

        public void handleBack(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

		public async void handleReg(object sender, EventArgs e)
		{

			//Check First Name
			if (checkName(fname.Text) == -1)
			{
				fname.BackgroundColor = Color.Red;
			}
			else
			{
				fname.BackgroundColor = Color.White;
			}

			//Check Last Name
			if (checkName(lname.Text) == -1)
			{
				lname.BackgroundColor = Color.Red;
			}
			else
			{
				lname.BackgroundColor = Color.White;
			}

			//Check Account Number
			if (checkAccount(accountNum.Text) == -1)
			{
				accountNum.BackgroundColor = Color.Red;
			}
			else
			{
				accountNum.BackgroundColor = Color.White;
			}

			//Check Password (1)
			if (checkPassword(password1.Text) == -1)
			{
				password1.BackgroundColor = Color.Red;
			}
			else
			{
				password1.BackgroundColor = Color.White;
				if (checkPassword(password2.Text) == -1)
				{
					password2.BackgroundColor = Color.Red;
				}
				else
				{
					password2.BackgroundColor = Color.White;
					if (password1.Text != password2.Text)
					{
						password1.BackgroundColor = Color.Red;
						password2.BackgroundColor = Color.Red;
					}
					else
					{
						password1.BackgroundColor = Color.White;
						password2.BackgroundColor = Color.White;
					}
				}
			}


			if (password1.BackgroundColor == Color.White &&
				password2.BackgroundColor == Color.White &&
				accountNum.BackgroundColor == Color.White &&
				lname.BackgroundColor == Color.White &&
				fname.BackgroundColor == Color.White)
			{
				//Create new user object
				User person = new User();
				person.FirstName = fname.Text;
				person.LastName = lname.Text;
				person.UserName = username.Text;
                person.AccountNumber = Convert.ToInt64(accountNum.Text);
                //Debug.WriteLine("\n\n\n\n\nThere was Nour account number!  " + person.AccountNumber);

                person.HashedPassword = password1.Text;

				int year = dobPicker.Date.Year;
                int day = dobPicker.Date.Day;
                int month = dobPicker.Date.Month;
                string birthday = year + "-" + day + "-" + month;

                person.Birthday = birthday;
				var sv = new WebService();
				var es = sv.RegisterUser(person);
                App.Current.MainPage = new NavigationPage(new NavigationLocal(es));

            }
        }
	}

}
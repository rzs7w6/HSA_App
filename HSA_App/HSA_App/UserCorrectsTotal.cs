using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSA_App
{
    public class UserCorrectsTotal : ContentPage
    {
        //Receipt receipt;
        Entry entry;

        public UserCorrectsTotal(Receipt receipt, ObservableCollection<Receipt> Invoices, User user, ReceiptRest rec)
        {
            //this.receipt = receipt;
            //Debug.WriteLine(ReferenceEquals(receipt, this.receipt));
            Button button = new Button();
            button.Text = "Confirm";
            button.VerticalOptions = LayoutOptions.Center;
            //button.Clicked += OnButtonClicked;
            button.Clicked += async (sender, ea) =>
            {
                Double total;
                if (!Double.TryParse(entry.Text, out total))
                {
                    await DisplayAlert("Error", "Invalid total. Try again.", "OK");
                    return;
                }
                else
                {
                    receipt.updateTotal(total);
                    Invoices.Add(receipt);
                    try
                    {
                        //ReceiptRest rec = new ReceiptRest();
                        var sv = new WebService();

                        //Withdrawl money from account
                        Balance balance = await sv.GetBalance(user.AccountNumber);
                        balance.AccountBalance -= (float)total;

                        if (balance.AccountBalance < 0)
                        {
                            Debug.WriteLine("You cannot complete this transaction due to insufficent funds");
                            return;
                        }

                        int b = await sv.UpdateBalance(balance);
                        if (b == -1)
                        {
                            Debug.WriteLine("Unable to update balance\n");
                            return;
                        }

                        //Add reciept to Database
                        //rec.Total = (float)total;
                        if (Invoices.Count != 0)
                        {
                            rec.Total = (float)Invoices.ElementAt<Receipt>(Invoices.Count - 1).Total;
                        }
                        else rec.Total = (float)6.66;
                        rec.Date = DateTime.Now.ToString("yyyy-MM-dd");
                        var es = await sv.RegisterReceipt(rec);

                        //Add transaction to Databse
                        Transaction trans = new Transaction();
                        trans.AccountNumber = user.AccountNumber;
                        trans.Type = "W";
                        trans.Date = DateTime.Now.ToString("yyyy-MM-dd");
                        trans.Amount = total;
                        int result = await sv.DepositTransaction(trans);

                        if (result == -1)
                        {
                            Debug.WriteLine("Unable to add transaction");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            };

            entry = new Entry();
            entry.Placeholder = "Total:";
            entry.Text = receipt.Total.ToString();

            Content = new StackLayout
            {

                Children = {
                    new Label { Text = "Enter Your Correct Total Below:" },
                    entry,
                    button
                }
            };
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            Double total;
            if (!Double.TryParse(entry.Text, out total))
            {
                await DisplayAlert("Error", "Invalid total. Try again.", "OK");
                return;
            }
            else
            {
                //receipt.Total = total;
                
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }
    }
}

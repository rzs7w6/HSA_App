using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class RecieptVaultPage : ContentPage
    {

		User me; 
        public RecieptVaultPage(User user)
        {
            InitializeComponent();
            BindingContext = new ViewReceiptViewModel();
			me = user;
        }
    }
}

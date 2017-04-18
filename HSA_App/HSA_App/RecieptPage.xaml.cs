using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class RecieptPage : ContentPage
    {
		User me;

        public RecieptPage(User user)
        {
            InitializeComponent();
			me = user;
            BindingContext = new ReceiptsViewModel();

        }
    }
}

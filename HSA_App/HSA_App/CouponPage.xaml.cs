
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{
    public partial class CouponPage : ContentPage
    {
		User me;

        public CouponPage(User user)
        {
            InitializeComponent();
            CouponListPage();
			me = user;
        }

        ObservableCollection<Coupon> coupons = new ObservableCollection<Coupon>();
        private void CouponListPage()
        {
            //defined in XAML to follow
            CouponView.ItemsSource = coupons;
            coupons.Add(new Coupon { StoreName = "cVs", CouponDetails = "Free Bitch Mittens" });
            coupons.Add(new Coupon { StoreName = "Bitchmart", CouponDetails = "Nad Creme" });
            coupons.Add(new Coupon { StoreName = "WalGREEN", CouponDetails = "Bud of Lyfe" });
            coupons.Add(new Coupon { StoreName = "Drug Store #1", CouponDetails = "Drugs" });
            coupons.Add(new Coupon { StoreName = "Rite Aids", CouponDetails = "G Easy Was Here" });
            coupons.Add(new Coupon { StoreName = "HighMee", CouponDetails = "Fresher Green In Every Aisle" });
        }   
        private void GetCoupons(object sender, EventArgs e)
        {
                /*  var rxcui = "198440";
                  var request = HttpWebRequest.Create(string.Format(@"http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", rxcui));
                  request.ContentType = "application/json";
                  request.Method = "GET";
                  String[] items;
                  int counter = 0;
                  using (HttpWebResponse response = request.BeginGetResponse)
                  {
                      using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                      {
                          String content = reader.ReadToEnd();
                          while(counter < content.Length)
                          {
                          }
                      }
                  }
                */

            }
    }
}
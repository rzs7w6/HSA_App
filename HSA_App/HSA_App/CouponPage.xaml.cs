﻿
using System;
using System.Collections.Generic;
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
        List<Coupon> couponList = new List<Coupon>();
        public string[] testData = { "CVS", "Free Shit", "Hyvee", "Not Free Shit" };
        
        public CouponPage()
        {
           InitializeComponent();
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
                  }*/
                for (int i = 0; i < testData.Length / 2; i++)
                {
                    Coupon coupon = new Coupon();
                    coupon.StoreName = testData[i];
                    coupon.CouponDetails = testData[i++];
                    couponList.Add(coupon);
                }
                

            }
    }
}
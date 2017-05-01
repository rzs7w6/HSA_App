using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HSA_App
{
    class EmailHandler
    {
        public static string username = "capstone_tbd";
        public static string password = "capstone1";

        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> sendForgotPasswordEmail(String recepient, String newPassword, String username)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            String body = "Your new password is: " + newPassword + ".";
            Dictionary<string, string> values = buildRequest(recepient, body);
            string requestUri = "http://ec2-54-68-37-246.us-west-2.compute.amazonaws.com:8081/sendEmail/?to=" + recepient + " &subject=UMB HSA Password Reset&body=" + body;
            var content = new FormUrlEncodedContent(values);
            var emptyContent = new StringContent("");

            var response = await client.PostAsync(requestUri, emptyContent);

            var responseString = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(responseString.ToString());

            return true;
        }

        public static Dictionary<string, string> buildRequest(String recepient, String body)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
               { "to", recepient },
               { "subject", "UMB HSA Password Reset" },
               { "body", body }
            };

            return values;
        }
    }
}

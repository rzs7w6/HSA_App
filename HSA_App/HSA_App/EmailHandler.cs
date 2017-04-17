using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HSA_App
{
    class EmailHandler
    {
        private static String userName = "rzs7w6";
        private static String password = "";
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> sendForgotPasswordEmail(String recepient)
        {
            String body = "Your new password is: WOOOOO";
            Dictionary<string, string> values = buildRequest(recepient, body);

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://api.sendgrid.com/api/mail.send.json", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(responseString.ToString());

            return true;
        }

        public static Dictionary<string, string> buildRequest(String recepient, String body)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
               { "api_user", userName },
               { "api_key", password },
               { "to", recepient },
               { "toname", "Customer" },
               { "subject", "UMB" },
               { "text", body },
               { "from", "info@domain.com" },
            };

            return values;
        }
    }
}

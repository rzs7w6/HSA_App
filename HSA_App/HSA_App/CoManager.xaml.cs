using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HSA_App
{

    public partial class CoManager : ContentPage
    {
        
        // http://services.faa.gov/docs/faq/


        public async Task<string> GetUser(int id)
        {

            string user = await GetRest(string.Format("/api/user/{0}", id));

            return user;
        }

        public async Task<string> GetRest(string code)
        {

            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var address = $"http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest/{code}";

          // var address = $"http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest/api/user/'DunDunDun'";

           // var address = $"http://10.0.2.2:57176/api/user/3";   //http://10.0.2.2:57176/{code}"; //?format=application/json";

            var response = await client.GetAsync(address);

            var userJson = response.Content.ReadAsStringAsync().Result;

            System.Diagnostics.Debug.WriteLine(userJson);

            //var rootobject = JsonConvert.DeserializeObject<Rootobject>(userJson);

            return userJson;

        }
    }


}


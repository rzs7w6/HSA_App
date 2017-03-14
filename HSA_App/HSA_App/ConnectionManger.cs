
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HSA_App.Connection
{
    class ConnectionManger
    {
        public LoginPage MainPage { get; internal set; }

        public async Task<Shared.Models.User> GetUser(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("localhost:57176/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.GetAsync(string.Format("/api/user/{0}", id));

                return JsonConvert.DeserializeObject<Shared.Models.User>(await result.Content.ReadAsStringAsync());
            }
        }

    }
}


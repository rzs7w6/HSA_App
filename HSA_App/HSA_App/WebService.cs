using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;

namespace HSA_App
{
    public class WebService
    {
        public WebService()
        {

        }
        
        public async Task<User> RegisterUser(User user)
        {
            Debug.WriteLine(user);
            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest/api/user");

            var json = JsonConvert.SerializeObject(user);

            try
            {
                StringContent content = new StringContent(json,UnicodeEncoding.UTF8, "application/json");
                //Debug.WriteLine("\n\n\nContent is: "+ content + "\n\n\n\n");
                var response = await client.PostAsync("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest/api/user", content);
                var UserJson = response.Content.ReadAsStringAsync().Result;
                var rootobject = JsonConvert.DeserializeObject<Rootobject>(UserJson);
                return rootobject.users;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        public async Task<User> GetUsers(String username, String password)
        {
            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest/api/user");
            
            var response = await client.GetAsync("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest/api/user/"+"\"" + username +"\"");

            var usersJson = response.Content.ReadAsStringAsync().Result;

            User returnPerson = JsonConvert.DeserializeObject<User>(usersJson);
            Debug.WriteLine(returnPerson.HashedPassword);
            if (returnPerson.HashedPassword.Equals(password))
                return returnPerson;
            else
                return null;


        }

    }
}

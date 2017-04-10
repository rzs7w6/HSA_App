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


            var data = "Cryptographic example";
            var pass = user.HashedPassword;
            
            //Crypto.
            var salt = Crypto.CreateSalt(16);
            Debug.WriteLine("Encrypting String " + data + ", with salt " + BitConverter.ToString(salt));
            var bytes = Crypto.EncryptAes(pass, pass, salt);

            user.HashedPassword = (BitConverter.ToString(bytes)).ToString();

            user.Salt = (BitConverter.ToString(salt)).ToString();



            Debug.WriteLine((BitConverter.ToString(salt)).ToString());

            var json = JsonConvert.SerializeObject(user);

            // Debug.WriteLine("hasssssshhhhh\n\n\n\n " + BitConverter.ToString(bytes));
            //var bytes2 = Crypto.
            Debug.WriteLine("Encrypted, Now Decrypting");
            //var str = Crypto.DecryptAes(bytes, pass, salt);
            //Debug.WriteLine("Decryted " + str);

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

            //var data = "Cryptographic example";
            var pass = password;

            //var salt = Crypto.CreateSalt(16);



            //var str = Crypto.DecryptAes(, returnPerson.HashedPassword, salt);
            //Debug.WriteLine("Decryted " + str);

            if (returnPerson.HashedPassword.Equals(password))
                return returnPerson;
            else
                return null;


        }

    }
}

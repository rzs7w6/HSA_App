﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HSA_App
{
    public class WebService
    {
        public WebService()
        {

        }

        public async Task<User> RegisterUser(User user)
        {
			//Create a new client object to access our resftull service
			var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/user");

            var pass = user.HashedPassword;
            
			//Get the Salt (Byte[])
            var salt = Crypto.CreateSalt(16);
            /*
			//Get the encrypted password
            var bytes = Crypto.EncryptAes(pass, pass, salt);

			//Store the string of "bytes" in our user object to be passed to the database
            user.HashedPassword = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
			*/
            user.Salt = Encoding.UTF8.GetString(salt, 0, salt.Length);
            
			//Convert object to Json to pass to restfull service
            var json = JsonConvert.SerializeObject(user);

            try
            {
				//POST CALL TO RESTFULL SERVICE
                StringContent content = new StringContent(json,UnicodeEncoding.UTF8, "application/json");
                var response = await client.PostAsync("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/user", content);
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

        public async Task<ReceiptRest> RegisterReceipt(ReceiptRest rec)
        {
            //Create a new client object to access our resftull service
            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/receipt");

            //Convert object to Json to pass to restfull service
            var json = JsonConvert.SerializeObject(rec);
            Debug.WriteLine("\n\n\nthe string has " +json.Length+" characters");
            try
            {
                //POST CALL TO RESTFULL SERVICE
                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var response = await client.PostAsync("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/receipt", content);

                Debug.WriteLine(response.Content);

                var RecJson = response.Content.ReadAsStringAsync().Result;
                var rootobject = JsonConvert.DeserializeObject<RootobjectRest>(RecJson);

                return rootobject.receipt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }



        public async Task <User> GetUsers(String username, String password)
        {
			var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/user/");

			var response = await client.GetAsync(client.BaseAddress +"\"" + username +"\"");

            var usersJson = response.Content.ReadAsStringAsync().Result;
		 	
			return JsonConvert.DeserializeObject<User>(usersJson);
        }

        public async Task<Balance> GetBalance(Int64 accountNumber)
        {
            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/balance/");

            var response = await client.GetAsync(client.BaseAddress + accountNumber.ToString());

            var usersJson = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Balance>(usersJson);
        }

        public async Task<List<ReceiptRest>> GetReceipts(Int64 accountNumber)
        {
            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/receipt/");
            try
            {
                var response = await client.GetAsync("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/receipt/" + accountNumber.ToString());
                var receiptJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ReceiptRest>>(receiptJson);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("\n\n\n\n\n\n"+ex);
            }
            return null;
        }

		public async Task<int> UpdateBalance(Balance balance)
		{

			var client = new System.Net.Http.HttpClient();

			client.BaseAddress = new Uri("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/balance");

			var json = JsonConvert.SerializeObject(balance);

			try
            {
                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
				var response = await client.PutAsync("http://ec2-54-69-2-41.us-west-2.compute.amazonaws.com/rest2/api/balance", content);

				Debug.WriteLine(response.Content);
			}
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
				return -1;
            }

			return 1;

		}

    }
}

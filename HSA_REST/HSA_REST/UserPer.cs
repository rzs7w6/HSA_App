using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HSA_REST.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HSA_REST
{
    public class UserPer
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public UserPer()
        {
            string myConnectionString;
            myConnectionString = "server=us-cdbr-azure-central-a.cloudapp.net; port=3306; database=umbcapstone; user=b6f1178a2ef7af; pwd=20593ecb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
               Console.WriteLine(ex.Message);
               Console.WriteLine("Connection failed");
            }
        }

        public long saveUser(User userToSave)
        {
            String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, Birthday, HashedPassword, UserName) VALUES ('" + userToSave.AccountNumber + "','" + userToSave.FirstName + "','" + userToSave.LastName + "','" + userToSave.Birthday.ToString("yyyy-MM-dd HH:mm:ss") + "','" + userToSave.HashedPassword + "','" + userToSave.UserName + "')";
            //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES ('2343245','Bryan','Boswell','jk4h3kj3h5k4j3h5k', 'Dododo')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
        }

    }
}
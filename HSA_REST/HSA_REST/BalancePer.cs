using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HSA_REST.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace HSA_REST
{
    public class BalancePer
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public BalancePer()
        {
            string myConnectionString;
            myConnectionString = "host=54.69.2.41; port=3306; database=umbcapstone; user=root; pwd=Capstone1!";
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

        public Balance getBalance(Int64 username)
        {
            Balance b = new Balance();
            MySqlDataReader MySqlReader = null;
            // Int64 recint = Convert.ToInt64(username);
            string sqlString = "SELECT * FROM balance WHERE AccountNumber = " + username;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            try
            {
                MySqlReader = cmd.ExecuteReader();
            }
            catch (SqlException oError)
            {
                //bFailed = true;
                Console.WriteLine(oError.Message);
            }



            while (MySqlReader.Read())
            {

                b.AccountNumber = MySqlReader.GetInt64(0);
                b.AccountBalance = MySqlReader.GetFloat(1);
            }

            return b;
        }
    }
}
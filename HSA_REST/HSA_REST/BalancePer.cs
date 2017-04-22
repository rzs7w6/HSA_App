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

        public void updateBalance(Int64 username, double amount)
        {
 
            string sqlString = "UPDATE balance SET AccountBalance = 'amount' WHERE AccountNumber = " + username;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();

        }

        public long setBalance(Balance userToSave)
        {

           // string sqlString = "UPDATE balance SET AccountBalance = " + userToSave.AccountBalance + " WHERE AccountNumber = " + userToSave.AccountNumber;
            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            //cmd.ExecuteNonQuery();

            string sqlString = "UPDATE balance SET AccountBalance = " + userToSave.AccountBalance + " WHERE AccountNumber = " + userToSave.AccountNumber;
            //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES (23432450098,'Bryan','Boswell','2-3-1456', 'jk4h3kj3h5k4j3h5k', 'Dododo', '2kjk3j4kj34kj', 'email@email.com')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;

        }

        public long saveBalance(Balance userToSave)
        {
            //if (!isUserDuplicate(userToSave))
            //{
                //conn.Open();
                String sqlString = "INSERT INTO balance(AccountNumber, AccountBalance) VALUES ('" + userToSave.AccountNumber + "','" + userToSave.AccountBalance + "')";
                //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES (23432450098,'Bryan','Boswell','2-3-1456', 'jk4h3kj3h5k4j3h5k', 'Dododo', '2kjk3j4kj34kj', 'email@email.com')";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                return id;
           // }
            //return -1;
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
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
    public class TransactionPer
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public TransactionPer()
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
        /*
        public void updateTransaction(Int64 username, double amount)
        {

            string sqlString = "UPDATE transaction_history SET AccountBalance = 'amount' WHERE AccountNumber = " + username;
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
        */
        public long saveTransaction(Transaction transToSave)
        {
            //if (!isUserDuplicate(userToSave))
            //{
            //conn.Open();

            //DateTime.Now.ToString("yyy-MM-dd");
            String sqlString = "INSERT INTO transaction_history(AccountNumber, Type, Amount, Date) VALUES ('" + transToSave.AccountNumber + "','" + transToSave.Type + "','" + transToSave.Amount + "','" + DateTime.Now.ToString("yyy-MM-dd") + "')";
            //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES (23432450098,'Bryan','Boswell','2-3-1456', 'jk4h3kj3h5k4j3h5k', 'Dododo', '2kjk3j4kj34kj', 'email@email.com')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
            // }
            //return -1;
        }


        public List<Transaction> getAllTrans(Int64 username)
        {
            List<Transaction> list = new List<Transaction>();

            Console.WriteLine("\n\n\n\n" + username);
            MySqlDataReader MySqlReader = null;
            // Int64 recint = Convert.ToInt64(username);
            string sqlString = "SELECT * FROM transaction_history WHERE AccountNumber = " + username;
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
                Transaction u = new Transaction();
                u.AccountNumber = MySqlReader.GetInt64(0);
                u.Type = MySqlReader.GetString(1);
                u.Amount = MySqlReader.GetDouble(2);
                u.Date = MySqlReader.GetString(3);


                //u.Image = null;
                //u.Image = MySqlReader.GetBytes(3,5, buff,8,6);
                list.Add(u);
            }

            return list;
        }


        public Double getTransaction(Int64 username)
        {
            //Transaction b = new Transaction();
            MySqlDataReader MySqlReader = null;

            String old = DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd");
            //old = old.Date;

            String now = DateTime.Now.Date.ToString("yyyy-MM-dd");

            // Int64 recint = Convert.ToInt64(username);+
            string sqlString = "SELECT SUM(Amount) FROM transaction_history WHERE Date BETWEEN " + "'" +  old + "'"  + " and " + "'" + now + "'" + " and AccountNumber = " + username + " and Type  = 'D'";
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

            Double returnVal = -1;

            while (MySqlReader.Read())
            {

                returnVal = MySqlReader.GetDouble(0);

            }
            Console.WriteLine(returnVal);
            return returnVal;
        }
        
    }
}
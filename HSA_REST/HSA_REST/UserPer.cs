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
    public class UserPer
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public UserPer()
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

        public User getUser(string userName)
        {
            User u = new User();
            MySql.Data.MySqlClient.MySqlDataReader MySqlReader = null;
            string sqlString = "SELECT * FROM user WHERE UserName = " + userName;
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

            if (MySqlReader.Read())
            {
                u.AccountNumber = MySqlReader.GetInt64(0);
                u.FirstName = MySqlReader.GetString(1);
                u.LastName = MySqlReader.GetString(2);
                u.Birthday = MySqlReader.GetString(3);
                u.HashedPassword = MySqlReader.GetString(4);
                u.UserName = MySqlReader.GetString(5);
                u.Salt = MySqlReader.GetString(6);
                u.Email = MySqlReader.GetString(7);
                return u;
            }

            return null;
        }

        public bool isUserDuplicate(User user)
        {
            //MySql.Data.MySqlClient.MySqlDataReader MySqlReader2 = null;

            String sqlString = "SELECT * FROM user WHERE AccountNumber =" + user.AccountNumber + " OR UserName =" + "'" + user.UserName + "'";
            //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES ('2343245','Bryan','Boswell','jk4h3kj3h5k4j3h5k', 'Dododo')";
            MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            //conn.Open();



            if (cmd2.ExecuteReader().HasRows)
            {
                conn.Close();
                return true;
                //MySqlReader2.Close();
            }
            else
            {
                conn.Close();
                return false;
            }

        }

        public long saveUser(User userToSave)
        {
            if (!isUserDuplicate(userToSave))
            {
                conn.Open();
                String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, Birthday, HashedPassword, UserName, Salt, Email) VALUES ('" + userToSave.AccountNumber + "','" + userToSave.FirstName + "','" + userToSave.LastName + "','" + userToSave.Birthday + "','" + userToSave.HashedPassword + "','" + userToSave.UserName + "','" + userToSave.Salt + "','" + userToSave.Email + "')";
                //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES (23432450098,'Bryan','Boswell','2-3-1456', 'jk4h3kj3h5k4j3h5k', 'Dododo', '2kjk3j4kj34kj', 'email@email.com')";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                return id;
            }
            return -1;
        }

    }
}
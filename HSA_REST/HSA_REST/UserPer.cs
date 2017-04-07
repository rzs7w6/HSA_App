﻿using System;
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
                String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, Birthday, HashedPassword, UserName) VALUES ('" + userToSave.AccountNumber + "','" + userToSave.FirstName + "','" + userToSave.LastName + "','" + userToSave.Birthday + "','" + userToSave.HashedPassword + "','" + userToSave.UserName + "')";
                //String sqlString = "INSERT INTO user(AccountNumber, FirstName, LastName, HashedPassword, UserName) VALUES ('2343245','Bryan','Boswell','jk4h3kj3h5k4j3h5k', 'Dododo')";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                return id;
            }
            return -1;
        }

    }
}
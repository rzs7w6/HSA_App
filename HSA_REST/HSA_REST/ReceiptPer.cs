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
    public class ReceiptPer
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public ReceiptPer()
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

        public Receipt getReceipt(string recName)
        {
            Receipt u = new Receipt();
            MySql.Data.MySqlClient.MySqlDataReader MySqlReader = null;
            string sqlString = "SELECT * FROM receipt WHERE UserName = " + recName;
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
                var buff = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                u.AccountNumber = MySqlReader.GetInt64(0);
                u.Total = MySqlReader.GetFloat(1);
                u.Date = MySqlReader.GetString(2);
                u.Image = null;
                //u.Image = MySqlReader.GetBytes(3,5, buff,8,6);
                return u;
            }
            
            return null;
        }

        public long saveReceipt(Receipt receiptToSave)
        {

            //conn.Open();
            var buff = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            //String sqlString = "INSERT INTO receipt(AccountNumber, Total, Date, Image) VALUES ('" + receiptToSave.AccountNumber + "','" + receiptToSave.Total + "','" + receiptToSave.Date + "','" + buff + "')";
            String sqlString = "INSERT INTO receipt(AccountNumber, Total, Date, Image) VALUES ('" + 992343245 + "','" + 45.6 + "','" + "3-34-1999" + "','" +  buff + "')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;

        }

    }
}


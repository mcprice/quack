using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Quack.Models
{
    public class ReportModel
    {
        internal int Feed(string location, string feedTime, string numberFed, string feedType, string gramsFed)
        {
            DateTime timeFed = DateTime.Parse(feedTime);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;";

            string queryString = "INSERT INTO Feed (FeedTime, FeedType, FeedLocation, FeedGroupSize, FeedGrams, UserID ) VALUES " +
                "(@feedTime, @feedType, @location, @numberFed, @gramsFed, @userID)";
            int rowsEffected = 0;

            using (conn)
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@feedTime", timeFed);
                command.Parameters.AddWithValue("@numberFed", numberFed);
                command.Parameters.AddWithValue("@feedType", feedType);
                command.Parameters.AddWithValue("@gramsFed", gramsFed);
                command.Parameters.AddWithValue("@userID", Convert.ToInt32(HttpContext.Current.Session["UserID"]));

                conn.Open();
                try
                {
                    rowsEffected = command.ExecuteNonQuery();
                }
                finally
                {
                    // Always call Close when done reading.
                    conn.Close();
                }
            }

            return rowsEffected;
        }

        internal DataTable GetAllFeedings()
        {
            DataTable t = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;";

            string queryString = "SELECT * FROM dbo.Feed"; // Obviously not ideal to just blindly select * in production...

            using (conn)
            {
                SqlCommand command = new SqlCommand(queryString, conn);

                conn.Open();

                try
                {
                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(t);

                }
                finally
                {
                    // Always call Close when done reading.
                    conn.Close();
                }
            }

            return t;
        }
    }
}
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
        /// <summary>
        /// Store a duck feeding in the database.
        /// </summary>
        /// <param name="location">Latitude and Longitude coordinates of where the feeding occurred</param>
        /// <param name="feedTime">The time the feeding happened</param>
        /// <param name="numberFed">The number of ducks fed</param>
        /// <param name="feedType">The type of food fed to the ducks</param>
        /// <param name="gramsFed">The amount in grams fed</param>
        /// <returns>the number of rows effected by the query</returns>
        internal int Feed(string location, string feedTime, string numberFed, string feedType, string gramsFed)
        {
            DateTime timeFed = DateTime.Parse(feedTime);
            SqlConnection conn = new SqlConnection();
#if DEBUG
            conn.ConnectionString = Constants.debugSqlConnection;
#else
            conn.ConnectionString = Constants.prodSqlConnection;
#endif

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

        /// <summary>
        /// Retrieve all feedings from the database.
        /// </summary>
        /// <returns>A datatable object containing the sql results</returns>
        internal DataTable GetAllFeedings()
        {
            DataTable t = new DataTable();

            SqlConnection conn = new SqlConnection();

#if DEBUG
            conn.ConnectionString = Constants.debugSqlConnection;
#else
            conn.ConnectionString = Constants.prodSqlConnection;
#endif


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
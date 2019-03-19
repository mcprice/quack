using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Quack.Models
{
    public class UserModel
    {
        public int Register(string email, string password, string name) {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;";

            String queryString = "INSERT INTO Users (EmailAddress, Name, Password ) output INSERTED.UserID VALUES (@email, @name, @password)";
            int insertedID = 0;

            using (conn)
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@name", name);

                byte[] data = Encoding.ASCII.GetBytes(password);
                data = new SHA256Managed().ComputeHash(data);
                string hash = Encoding.ASCII.GetString(data);

                command.Parameters.AddWithValue("@password", hash);

                conn.Open();
                try
                {
                    insertedID = (int)command.ExecuteScalar();
                }
                finally
                {
                    // Always call Close when done reading.
                    conn.Close();
                }
            }

            return insertedID;
        }

        public int DeleteUser(int? userID)
        {
            // Early exit if no userid is passed in.
            if (userID == null)
            {
                return 0;
            }

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;";

            string queryString = "DELETE FROM dbo.Users WHERE UserID = @userID";
            int rowsEffected = 0;

            using (conn)
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@userID", userID);

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

        internal bool FindDuplicateEmail(string email)
        {
            bool emailExists = false;

            SqlConnection conn = new SqlConnection
            {
                ConnectionString =
            "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;"
            };

            string queryString = "SELECT count(UserID) FROM dbo.Users WHERE EmailAddress = @email";

            using (conn)
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@email", email);

                conn.Open();

                try
                {
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        emailExists = true;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    conn.Close();
                }
            }

            return emailExists;
        }

        internal bool Login(string email, string pass)
        {
            bool sessionCreated = false;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;";

            string queryString = "SELECT TOP 1 UserID, EmailAddress, Name FROM dbo.Users WHERE EmailAddress = @email AND Password = @password";

            using (conn)
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@email", email);

                byte[] data = Encoding.ASCII.GetBytes(pass);
                data = new SHA256Managed().ComputeHash(data);
                string hash = Encoding.ASCII.GetString(data);

                command.Parameters.AddWithValue("@password", hash);

                conn.Open();
                
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var userEmail = String.Format("{0}", reader["EmailAddress"]);
                        var userName = String.Format("{0}", reader["Name"]);
                        var userID = String.Format("{0}", reader["UserID"]);

                        HttpContext.Current.Session["UserID"] = userID;
                        HttpContext.Current.Session["UserName"] = userName;
                        HttpContext.Current.Session["UserEmail"] = userEmail;

                        sessionCreated = true;
                    }

                }
                finally
                {
                    // Always call Close when done reading.
                    conn.Close();
                }
            }

            return sessionCreated;
        }
    }
}
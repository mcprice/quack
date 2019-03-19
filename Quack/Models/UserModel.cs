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
        /// <summary>
        /// Register a new quack account and store in db. hashes password.
        /// </summary>
        /// <param name="email">Users' email address</param>
        /// <param name="password">Users' password</param>
        /// <param name="name">The users' name</param>
        /// <returns>The ID of the inserted user</returns>
        public int Register(string email, string password, string name) {

            SqlConnection conn = new SqlConnection();
#if DEBUG
            conn.ConnectionString = Constants.debugSqlConnection;
#else
            conn.ConnectionString = Constants.prodSqlConnection;
#endif

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

        /// <summary>
        /// Deletes a user by ID
        /// </summary>
        /// <param name="userID">The ID to delete</param>
        /// <returns>The number of rows effected</returns>
        public int DeleteUser(int? userID)
        {
            // Early exit if no userid is passed in.
            if (userID == null)
            {
                return 0;
            }

            SqlConnection conn = new SqlConnection();
#if DEBUG
            conn.ConnectionString = Constants.debugSqlConnection;
#else
            conn.ConnectionString = Constants.prodSqlConnection;
#endif

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

        /// <summary>
        /// Detect whether the users chosen email is a duplicate
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <returns>True or false. True if duplicate is found.</returns>
        internal bool FindDuplicateEmail(string email)
        {
            bool emailExists = false;
            SqlConnection conn = new SqlConnection();
#if DEBUG
            conn.ConnectionString = Constants.debugSqlConnection;
#else
            conn.ConnectionString = Constants.prodSqlConnection;
#endif

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

        /// <summary>
        /// Login to quack. Validate user credentials and create session.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="pass">User password</param>
        /// <returns>True or false. True of user is logged in</returns>
        internal bool Login(string email, string pass)
        {
            bool sessionCreated = false;

            SqlConnection conn = new SqlConnection();
#if DEBUG
            conn.ConnectionString = Constants.debugSqlConnection;
#else
            conn.ConnectionString = Constants.prodSqlConnection;
#endif

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
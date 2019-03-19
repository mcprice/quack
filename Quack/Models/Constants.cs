using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quack.Models
{
    public static class Constants
    {
        /// <summary>
        /// SQL connection string for local dev box.
        /// </summary>
        public static string debugSqlConnection = "Data Source=MATT-PC\\SQLEXPRESS;" +
            "Initial Catalog=QuackDb;" +
            "User id=matt;" +
            "Password=rslt4499;";

        /// <summary>
        /// SQL connection string for production environment.
        /// </summary>
        public static string prodSqlConnection = "Data Source=sql7005.site4now.net;" +
            "Initial Catalog=DB_A46A18_quack;" +
            "User id=DB_A46A18_quack_admin;" +
            "Password=rslt4499;";
    }
}
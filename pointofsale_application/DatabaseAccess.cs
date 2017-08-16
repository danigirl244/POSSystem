using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pointofsale_application
{
    class DatabaseAccess
    {
        //https://www.codeproject.com/Articles/4416/Beginners-guide-to-accessing-SQL-Server-through-C



        public SqlConnection AccessDB()
        {

            SqlConnection myConnection = new SqlConnection("Data Source=.\\SQLSERVER;" + "user id=admin;" +
                                   "password=adminpassword;"+ "server=possystem.cyjrzrk7ktbi.us-west-1.rds.amazonaws.com,1433;"+
                                   "database=POSSystem;" + "timeout=2;");
            try
            {
                myConnection.Open();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Cannot connect to database.");
            }
            return myConnection;
        }
    }
}

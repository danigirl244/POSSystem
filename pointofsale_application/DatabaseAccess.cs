using System;
using System.Data.SqlClient;
using System.Windows;

namespace pointofsale_application
{
    public class DatabaseAccess
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
                MessageBox.Show(ex.Message.ToString(), "Cannot connect to database");
            }
            return myConnection;
        }
    }
}

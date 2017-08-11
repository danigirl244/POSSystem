using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pointofsale_application
{
    class DatabaseAccess
    {
        //https://www.codeproject.com/Articles/4416/Beginners-guide-to-accessing-SQL-Server-through-C

        SqlConnection myConnection = new SqlConnection("user id=admin;" +
                               "password=adminpassword;server=possystem.cyjrzrk7ktbi.us-west-1.rds.amazonaws.com,1433;" +
                               "Trusted_Connection=yes;" +
                               "database=POSSystem;" +
                               "connection timeout=30");

        public void ConnectDB()
        {
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool AccessDB(string command)
        {
            ConnectDB();

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(command,
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    if (myReader.HasRows == true)
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }
        public void CloseDB()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

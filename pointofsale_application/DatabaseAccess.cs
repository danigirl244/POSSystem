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
        SqlConnection connection;
        SqlCommand command;

        public DatabaseAccess()
        {
            connection = new SqlConnection()
            {
                ConnectionString = @"Data Source=.     \SQLEXPRESS;AttachDbFilename=|DataDirectory|DatabaseName.mdf;Integrated Security=True;User Instance=True"
            };
            command = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.Text
            };
        }

    }
}

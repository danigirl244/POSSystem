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
            connection = new SqlConnection();

            command = new SqlCommand()
            {
                Connection = connection,
                CommandType = CommandType.Text
            };
        }

    }
}

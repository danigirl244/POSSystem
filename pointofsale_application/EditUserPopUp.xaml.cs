using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for EditUserPopUp.xaml
    /// </summary>
    public partial class EditUserPopUp : Window
    {

        DatabaseAccess dbt = new DatabaseAccess();

        public EditUserPopUp(string name)
        {
            InitializeComponent();
            getEmpInfo(name);
            
        }

        public void getEmpInfo(string name)
        {
            string permissions = "";
            bool isActive = true;

            SqlCommand users = new SqlCommand("SELECT Permissions, isActive FROM Users Where EmployeeName = @param1", dbt.AccessDB());
            users.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = name;

            SqlDataReader rd;
            rd = users.ExecuteReader();
            while (rd.Read())
            {
                permissions = rd.GetString(rd.GetOrdinal("Permissions"));
                isActive = rd.GetBoolean(rd.GetOrdinal("isActive"));
                //usersName.Add(rd.GetString(rd.GetOrdinal("EmployeeName")).Replace(" ", String.Empty));
            }

            UserName.Text = name;
            AStatus.Text = isActive.ToString();
            PStatus.Text = permissions;
        }

        public void UpdateEmpRank(int empID, string empPermissions)
        {
            //updates user rank according to the predetermined value ex) basic, admin

            SqlCommand updateEmpRank = new SqlCommand("UPDATE Users SET Permissions = @param2 WHERE UserID = @param1;", dbt.AccessDB());

            updateEmpRank.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empID;
            updateEmpRank.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = empPermissions;
            updateEmpRank.CommandType = CommandType.Text;

            try
            {
                updateEmpRank.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");

            }

        }

        public void UpdateEmpAct(int empID, int activity)
        {
            //updates user's isActive according to the predetermined value

            SqlCommand updateEmpAct = new SqlCommand("UPDATE Users SET isActive = @param2 WHERE UserID = @param1;", dbt.AccessDB());

            updateEmpAct.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empID;
            updateEmpAct.Parameters.Add("@param2", SqlDbType.Bit).Value = activity;
            updateEmpAct.CommandType = CommandType.Text;

            try
            {
                updateEmpAct.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");

            }

        }
    }
}

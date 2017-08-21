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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        DatabaseAccess dbt = new DatabaseAccess();

        public AddUser()
        {
            InitializeComponent();
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to permanently delete ", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you like to save these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                SortEmpData();
            }
        }

        public void SortEmpData()
        {
            string name = Name.Text;
            string empPermissions = "";
            int isActive = 1;
            if(Convert.ToBoolean(AdminRB.IsChecked))
            {
                empPermissions = "admin";
            } else
            {
                empPermissions = "basic";
            }

            AddEmp(name, empPermissions, isActive);
        }

        public void showID(string name)
        {
            int empID = 0;

            SqlCommand showID = new SqlCommand("Select UserID From Users Where EmployeeName = @param1", dbt.AccessDB());
            showID.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = name;
            SqlDataReader rd;
            rd = showID.ExecuteReader();
            while(rd.Read())
            {
               empID = rd.GetInt32(rd.GetOrdinal("UserID"));
            }

            MessageBox.Show("Login ID: " + empID.ToString());
        }

        public void AddEmp(string empName, string empPermissions, int isActive)
        {
            //creates a new employee with predetermined values

            SqlCommand addEmp = new SqlCommand("INSERT INTO Users (EmployeeName, Permissions, isActive) VALUES (@param1, @param2, @param3);", dbt.AccessDB());

            addEmp.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empName;
            addEmp.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = empPermissions;
            addEmp.Parameters.Add("@param3", SqlDbType.Bit).Value = isActive;
            addEmp.CommandType = CommandType.Text;

            try
            {
                addEmp.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");

            }

            showID(empName);

        }
    }
}

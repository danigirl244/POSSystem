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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {

        DatabaseAccess dbt = new DatabaseAccess();
        public EditUser()
        {
            InitializeComponent();
        }

        
private void ActiveDeactive_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {

            }
           

        }

        private void Promote_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        private void Promote2_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
            else
            {
                
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void AddEmp(string empName, string empPermissions, string isActive)
        {
            //creates a new employee with predetermined values

            SqlCommand addEmp = new SqlCommand("INSERT INTO Users (EmployeeName, Permissions, isActive) VALUES (" + empName + ", " + empPermissions + ", " + isActive + ");", dbt.AccessDB());                
            
        }

        public void UpdateEmpRank(int empID, string empPermissions)
        {
            //updates user rank according to the predetermined value ex) basic, admin

            SqlCommand updateEmpRank = new SqlCommand("UPDATE Users SET EmployeeRank = " + empPermissions + " WHERE UserID = " + empID + ";", dbt.AccessDB());

        }

        public void UpdateEmpAct(int empID, string activity)
        {
            //updates user's isActive according to the predetermined value

            SqlCommand updateEmpAct = new SqlCommand("UPDATE Users SET isActive = " + activity + " WHERE UserID = " + empID + ";", dbt.AccessDB());

        }

    }
}

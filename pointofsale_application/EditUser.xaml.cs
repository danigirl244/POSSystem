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

        public void addEmp(string empName, string empPermissions, string isActive)
        {           
            //creates a new employee with predetermined values
            //dbt.AccessDB("INSERT INTO dbo.Users (EmployeeName, Permissions, isActive) VALUES (" + empName + ", " + empPermissions + ", " + isActive + ");");
                
            
        }

        public void updateEmpRank(int empID, string empPermissions)
        {
            //updates user rank according to the predetermined value ex) basic, admin
            //dbt.AccessDB("UPDATE dbo.Users SET EmployeeRank = " + empPermissions + " WHERE UserID = " + empID + ";");

        }

        public void updateEmpAct(int empID, string activity)
        {
            //updates user's isActive according to the predetermined value
           // dbt.AccessDB("UPDATE dbo.Users SET isActive = " + activity + " WHERE UserID = " + empID + ";");

        }

    }
}

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

        //create separate onClick method that calls these methods with the parameters
        public void addEmp(string empName, string empRank, string isActive)
        {
            //ensuring values aren't null
            //Dont know if necessary
            if (empName != null && empRank != null && isActive != null)
            {
                //connect to database
                //might need to add dbo. before Users
                //creates a new employee with predetermined values
                //syntax might be incorrect for command
                SqlCommand insertUser = new SqlCommand("INSERT INTO Users (UserID, EmployeeName, EmployeeRank, isActive) VALUES (" + empName + ", " + empRank + ", " + isActive + ");");
                insertUser.CommandType = CommandType.Text;
                //might be able to make this a global class variable so it doesn't have to keep being called like this
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = insertUser;
            }
        }

        public void updateEmpRank(int empID, string rank)
        {
            //connect to database
            //updates user rank according to the predetermined value ex) basic, admin
            //syntax might be incorrect for command
            SqlCommand updateUserRank = new SqlCommand("UPDATE Users SET EmployeeRank = " + rank + " WHERE UserID = " + empID + ";");
            updateUserRank.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = updateUserRank;

        }

        public void updateEmpAct(int empID, string activity)
        {
            //connect to database
            //updates user's isActive according to the predetermined value
            //syntax might be incorrect for command
            SqlCommand updateUserAct = new SqlCommand("UPDATE Users SET isActive = " + activity + " WHERE UserID = " + empID + ";");
            updateUserAct.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = updateUserAct;
        }

    }
}

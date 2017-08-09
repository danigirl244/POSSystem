using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Navigation;

namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        
        public Login()
        {
            InitializeComponent();
        }
        DatabaseAccess db = new DatabaseAccess();

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmpID.Text.Length == 0 || textBoxEmpID.Text.Length != 5)
            {
                //errormessage.Text = "Enter employee id.";
                //textBoxEmpID.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmpID.Text, @"^[0-9]+$") || textBoxEmpID.Text.Length != 5)
            {
                //errormessage.Text = "Enter a valid employee id.";
                //textBoxEmpID.Select(0, textBoxEmpID.Text.Length);
                //textBoxEmpID.Focus();
            }
            else
            {
                SqlCommand idlookup = new SqlCommand();//Needs sql query
                idlookup.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = idlookup;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    if (Validation(input, db))
                    {
                        Status(input, db);
                        //Save Employee Info into employee object
                    }
                    else
                    {
                        //Error Invalid Employee Id
                    }
                }
                else
                {
                    //Incorrect Input Message
                }
            }
        }
       
        public bool Validation(int input, object db)
        {
            bool valid = false;
            SqlCommand activelookup = new SqlCommand();//Needs sql query
            activelookup.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = activelookup;
            DataSet data = new DataSet();
            adapter.Fill(data);
            if (data.ToString().Equals(""))
            {
                valid = true;
            }
            return valid;
        }

        public string Status(int input, object db)
        {
            String status;
            SqlCommand permlookup = new SqlCommand();//Needs sql query
            permlookup.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = permlookup;
            DataSet data = new DataSet();
            adapter.Fill(data);
            if (data.ToString().Equals(""))
            {
                status = "admin";
            }
            else
            {
                status = "basic";
            }
            return status;
        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            HomePage homepage = new HomePage();
            this.Close();
            homepage.Show();

        }
    }
}

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
        Menu menu = new Menu();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmpID.Text.Length == 0 || textBoxEmpID.Text.Length != 6)
            {
                errormessage.Content = "Enter employee id.";
                textBoxEmpID.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmpID.Text, @"^[0-9]+$") || textBoxEmpID.Text.Length != 6)
            {
                errormessage.Content = "Enter a valid employee id.";
                textBoxEmpID.Select(0, textBoxEmpID.Text.Length);
                textBoxEmpID.Focus();
            }
            else
            {
                string empID = textBoxEmpID.Text;

                SqlConnection con = new SqlConnection("<Specific info required from Database team>");
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Employees where EmpID='" + empID + "'", con);//Not sure if table name or column names are correct
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string username = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();//This can be change/removed depending on the data being stored
                    Close();
                }
                else
                {
                    errormessage.Content = "Invalid Identification.  Enter an existing employee id.";
                }
                con.Close();
            }
        }
    }
}

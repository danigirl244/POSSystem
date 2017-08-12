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
        StringBuilder sb = new StringBuilder("", 5);
        public Login()
        {
            InitializeComponent();
        }
        DatabaseAccess db = new DatabaseAccess();
       

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            int input = 0;
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
               if (db.AccessDB("select * from Users where UserID = " + textBoxEmpID.Text))
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
<<<<<<< HEAD

            HomePage hm = new HomePage();
            this.Close();
            hm.Show();

=======
            HomePage homepage = new HomePage();
            homepage.Show();
            this.Close();
>>>>>>> 909ae9f508b9c6645fdb79b3e36038b3cb235641
        }
       
        public bool Validation(int input, object db)
        {
            bool valid = false;
            SqlCommand activelookup = new SqlCommand("Select isActive from Users where UserID = '" + input + "'");//Needs sql query checking the isActive field in the Users table
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
            SqlCommand permlookup = new SqlCommand("Select [Permissions] from Users where UserID = '" + input + "'");//Needs sql query to check the permissions a user has (Permissions)
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



        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            sb.Clear();
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num0_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('0');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num1_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('1');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num2_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('2');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num3_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('3');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num4_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('4');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num5_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('5');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num6_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('6');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num7_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('7');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num8_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('8');
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num9_Click(object sender, RoutedEventArgs e)
        {
            sb.Append('9');
            textBoxEmpID.Text = sb.ToString();
        }
    }
}

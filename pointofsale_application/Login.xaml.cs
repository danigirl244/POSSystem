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
            HomePage homepage = new HomePage();
            AdminPage adminpage = new AdminPage();

            if (textBoxEmpID.Text.Length == 0 || textBoxEmpID.Text.Length != 5)
            {
                errormessage.Text = "Invalid Input: Enter employee id.";
                textBoxEmpID.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmpID.Text, @"^[0-9]+$") || textBoxEmpID.Text.Length != 5)
            {
                errormessage.Text = "Invalid Input: Enter a valid employee id.";
                textBoxEmpID.Select(0, textBoxEmpID.Text.Length);
                textBoxEmpID.Focus();
            }
            else
            {
               if (db.AccessDB("select * from Users where UserID = " + textBoxEmpID.Text))
                {

                    if (Validation(sb.ToString(), db))
                    {
                        if (Status(sb.ToString(), db).Equals("basic"))
                        {
                            homepage.Show();
                            this.Close();
                        }
                        if (Status(sb.ToString(), db).Equals("admin"))
                        {
                            adminpage.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        errormessage.Text = "Invalid Input: Enter a valid employee id.";
                    }
                }
                else
                {
                    errormessage.Text = "Invalid Input: Enter a valid employee id.";
                }
            }


            HomePage hm = new HomePage();
            this.Close();
            hm.Show();


            HomePage homepage = new HomePage();
            homepage.Show();
            this.Close();

        }
       
        public bool Validation(string sb, object db)
        {
            bool valid = false;
<<<<<<< HEAD
<<<<<<< HEAD
            SqlCommand activelookup = new SqlCommand("Select isActive from Users where UserID = '" + input + "'");//Needs sql query checking the isActive field in the Users table
=======
            SqlCommand activelookup = new SqlCommand("Select isActive from Users where UserID = '" + sb + "'");
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
            SqlCommand activelookup = new SqlCommand("Select isActive from Users where UserID = '" + sb + "'");
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
            activelookup.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = activelookup;
            DataSet data = new DataSet();
            adapter.Fill(data);
            if (data.ToString().Equals("1"))
            {
                valid = true;
            }
            return valid;
        }

        public string Status(string sb, object db)
        {
            String status;
<<<<<<< HEAD
<<<<<<< HEAD
            SqlCommand permlookup = new SqlCommand("Select [Permissions] from Users where UserID = '" + input + "'");//Needs sql query to check the permissions a user has (Permissions)
=======
            SqlCommand permlookup = new SqlCommand("Select [Permissions] from Users where UserID = '" + sb + "'");
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
            SqlCommand permlookup = new SqlCommand("Select [Permissions] from Users where UserID = '" + sb + "'");
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
            permlookup.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = permlookup;
            DataSet data = new DataSet();
            adapter.Fill(data);
            if (data.ToString().Equals("admin"))
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
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('0');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('0');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num1_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('1');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('1');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num2_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('2');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('2');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num3_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('3');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('3');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num4_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('4');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('4');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num5_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('5');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('5');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num6_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('6');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('6');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num7_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('7');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('7');
            textBoxEmpID.Text = sb.ToString();
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
            sb.Append('7');
            textBoxEmpID.Text = sb.ToString();
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num8_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('8');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('8');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }

        private void Num9_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            if (sb.ToString().Length < 5)
            {
                sb.Append('9');
                textBoxEmpID.Text = sb.ToString();
            }
=======
            sb.Append('9');
            textBoxEmpID.Text = sb.ToString();
<<<<<<< HEAD
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
=======
>>>>>>> 53e3d7a15eb7659c280087af72b3e0d7d0f87295
        }
    }
}

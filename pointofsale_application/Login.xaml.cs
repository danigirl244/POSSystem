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
        string perm;
        StringBuilder sb = new StringBuilder("", 5);
        public Login()
        {
            InitializeComponent();
        }
        DatabaseAccess db = new DatabaseAccess();

        public static class staticVars
        {
            public static string cashierName { get; set; }

        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
           
            if(textBoxEmpID.Text.Length == 5)
            {
                if(Regex.IsMatch(textBoxEmpID.Text, @"^[0-9]+$"))
                {
                    SqlCommand login = new SqlCommand("SELECT COUNT(*) FROM Users WHERE UserID = " + textBoxEmpID.Text, db.AccessDB());

                    int UserExist = 0;

                    try
                    {
                        UserExist = (int)login.ExecuteScalar();
                    }
                    catch
                    {
                        errormessage.Text = "An error occured accessing the database.";
                        return;
                    }

                    if (UserExist >= 1)
                    {
                        SqlCommand userAdmin = new SqlCommand("SELECT Permissions FROM Users WHERE UserID = " + textBoxEmpID.Text, db.AccessDB());
                        string permission = userAdmin.ExecuteScalar().ToString();
                        perm = permission;

                        SqlCommand userName = new SqlCommand("SELECT EmployeeName FROM Users WHERE UserID = " + textBoxEmpID.Text, db.AccessDB());
                        staticVars.cashierName = userName.ExecuteScalar().ToString();
                        //cashierName = name;

                        HomePage homepage = new HomePage(perm);
                        AdminPage adminpage = new AdminPage(perm);
                        if (permission == "admin")
                        {
                            adminpage.Show();
                            this.Close();
                        }
                        else
                        {
                            homepage.Show();
                            this.Close();
                        }

                    }
                        else
                    {
                        errormessage.Text = "That user does not exist. Try Again.";
                    }
                }
            }
            else
            {
                errormessage.Text = "Invalid entry. Try Again.";
            }

        }
       

    


        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            sb.Clear();
            textBoxEmpID.Text = sb.ToString();
        }

        private void Num0_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('0');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num1_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('1');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num2_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('2');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num3_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('3');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num4_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('4');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num5_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('5');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num6_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('6');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num7_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('7');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num8_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('8');
                textBoxEmpID.Text = sb.ToString();
            }
        }

        private void Num9_Click(object sender, RoutedEventArgs e)
        {
            if (sb.ToString().Length < 5)
            {
                sb.Append('9');
                textBoxEmpID.Text = sb.ToString();
            }
        }
    }
}

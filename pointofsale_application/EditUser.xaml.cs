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
        List<string> usersName = new List<string>();
        public EditUser()
        {
            InitializeComponent();
            getEmpInfo();
        }

        public void getEmpInfo()
        {
            SqlCommand users = new SqlCommand("SELECT EmployeeName FROM Users", dbt.AccessDB());

            SqlDataReader rd;
            rd = users.ExecuteReader();
            while (rd.Read())
            {
                usersName.Add(rd.GetString(rd.GetOrdinal("EmployeeName")).Replace(" ", String.Empty));
            }

            fillEmpColumn(usersName);
        }

        public void fillEmpColumn(List<string> Emp)
        {

            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button newBtn = new Button();
                    if (Emp.Count > count)
                    {
                        newBtn.Content = Emp[count];
                        newBtn.Name = Emp[count];
                        newBtn.Click += (s, e) => { btn_Click(newBtn.Name.ToString()); };
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        public void btn_Click(string name)
        {
            EditUserPopUp eUser = new EditUserPopUp(name);
            eUser.Show();
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser newUser = new AddUser();
            newUser.Show();
        }
    }
}

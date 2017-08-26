using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {

        DatabaseAccess db = new DatabaseAccess();
        List<string> usersName = new List<string>();
        public EditUser()
        {
            InitializeComponent();
            GetEmpInfo();
        }

        public void GetEmpInfo()
        {
            SqlCommand users = new SqlCommand("SELECT EmployeeName, UserID FROM Users", db.AccessDB());

            SqlDataReader rd;
            rd = users.ExecuteReader();
            while (rd.Read())
            {
                usersName.Add(rd.GetString(rd.GetOrdinal("EmployeeName")) + "," + rd.GetInt32(rd.GetOrdinal("UserID")));
            }

            FillEmpColumn(usersName);
        }

        public void FillEmpColumn(List<string> Emp)
        {

            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button newBtn = new Button();
                    if (Emp.Count > count)
                    {
                        string[] empInfo = Emp[count].Split(',');

                        newBtn.Content = empInfo[0];
                        newBtn.Name = empInfo[0];
                        newBtn.Tag = empInfo[1];
                        newBtn.Click += (s, e) => { Btn_Click(newBtn.Name.ToString(), newBtn.Tag.ToString()); };
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        public void Btn_Click(string userName, string userID)
        {
            EditUserPopUp eUser = new EditUserPopUp(userName, userID);
            eUser.ShowDialog();
        }

        
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser newUser = new AddUser();
            newUser.ShowDialog();
        }
    }
}

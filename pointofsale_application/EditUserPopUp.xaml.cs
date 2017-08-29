using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for EditUserPopUp.xaml
    /// </summary>
    public partial class EditUserPopUp : Window
    {

        DatabaseAccess db = new DatabaseAccess();

        public EditUserPopUp(string name, string userID)
        {
            InitializeComponent();
            GetEmpInfo(name, userID);
            DisplayInfo();
        }

        public EditUserPopUp()
        {

        }

        public void DisplayInfo()
        {
            if (PStatus.Text == "Basic")
            {
                PStatus.Text = "Admin";
                PromoteButton.Content = "Demote";
            }
            else
            {
                PStatus.Text = "Basic";
                PromoteButton.Content = "Promote";
            }
            if (AStatus.Text == "True")
            {
                AStatus.Text = "False";
                ActivateButton.Content = "Activate";
            }
            else
            {
                AStatus.Text = "True";
                ActivateButton.Content = "Deactivate";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard these changes? ", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you like to save these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                if(AStatus.Text == "True")
                {
                    UpdateEmpAct(UserName.Tag.ToString(), 1);
                } else
                {
                    UpdateEmpAct(UserName.Tag.ToString(), 0);
                }

                UpdateEmpName(UserName.Tag.ToString(), UserName.Text);
                UpdateEmpRank(UserName.Tag.ToString(), PStatus.Text);
            }
        }

        public void GetEmpInfo(string name, string userID)
        {
            string permissions = "";
            bool isActive = true;

            SqlCommand users = new SqlCommand("SELECT Permissions, isActive FROM Users Where UserID = @param1", db.AccessDB());
            users.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = userID;

            SqlDataReader rd;
            rd = users.ExecuteReader();
            while (rd.Read())
            {
                permissions = rd.GetString(rd.GetOrdinal("Permissions"));
                isActive = rd.GetBoolean(rd.GetOrdinal("isActive"));
            }

            UserName.Text = name;
            AStatus.Text = isActive.ToString();
            PStatus.Text = permissions;
            UserName.Tag = userID;
        }

        private void On_ClickAct(object sender, RoutedEventArgs e)
        {

            if(AStatus.Text == "True")
            {
                AStatus.Text = "False";
                ActivateButton.Content = "Activate";
            } else
            {
                AStatus.Text = "True";
                ActivateButton.Content = "Deactivate";
            }

        }

        private void On_ClickPerm(object sender, RoutedEventArgs e)
        {
            if(PStatus.Text == "Basic")
            {
                PStatus.Text = "Admin";
                PromoteButton.Content = "Demote";
            } else
            {
                PStatus.Text = "Basic";
                PromoteButton.Content = "Promote";
            }

        }

        public void UpdateEmpRank(string empID, string empPermissions)
        {
            //updates user rank according to the predetermined value ex) basic, admin

            SqlCommand updateEmpRank = new SqlCommand("UPDATE Users SET Permissions = @param2 WHERE UserID = @param1;", db.AccessDB());

            updateEmpRank.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empID;
            updateEmpRank.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = empPermissions;
            updateEmpRank.CommandType = CommandType.Text;

            try
            {
                updateEmpRank.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");

            }

        }

        public void UpdateEmpName(string empID, string empName)
        {
            SqlCommand updateEmpName = new SqlCommand("UPDATE Users Set EmployeeName = @param2 WHERE UserID = @param1", db.AccessDB());

            updateEmpName.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empID;
            updateEmpName.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = UserName.Text;

            try
            {
                updateEmpName.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
            }

        }

        public void UpdateEmpAct(string empID, int activity)
        {
            //updates user's isActive according to the predetermined value

            SqlCommand updateEmpAct = new SqlCommand("UPDATE Users SET isActive = @param2 WHERE UserID = @param1;", db.AccessDB());

            updateEmpAct.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empID;
            updateEmpAct.Parameters.Add("@param2", SqlDbType.Bit).Value = activity;
            updateEmpAct.CommandType = CommandType.Text;

            try
            {
                updateEmpAct.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");

            }

        }
    }
}

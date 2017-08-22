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

        DatabaseAccess dbt = new DatabaseAccess();

        public EditUserPopUp(string name)
        {
            InitializeComponent();
            GetEmpInfo(name);
            
        }

        public EditUserPopUp()
        {

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

                UpdateEmpRank(UserName.Tag.ToString(), PStatus.Text);
            }
        }

        public void GetEmpInfo(string name)
        {
            string permissions = "";
            int userID = 0;
            bool isActive = true;

            SqlCommand users = new SqlCommand("SELECT UserID, Permissions, isActive FROM Users Where EmployeeName = @param1", dbt.AccessDB());
            users.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = name;

            SqlDataReader rd;
            rd = users.ExecuteReader();
            while (rd.Read())
            {
                permissions = rd.GetString(rd.GetOrdinal("Permissions"));
                isActive = rd.GetBoolean(rd.GetOrdinal("isActive"));
                userID = rd.GetInt32(rd.GetOrdinal("UserID"));
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
            } else
            {
                AStatus.Text = "True";
            }

        }

        private void On_ClickPerm(object sender, RoutedEventArgs e)
        {
            if(PStatus.Text == "basic")
            {
                PStatus.Text = "admin";
            } else
            {
                PStatus.Text = "basic";
            }

        }

        public void UpdateEmpRank(string empID, string empPermissions)
        {
            //updates user rank according to the predetermined value ex) basic, admin

            SqlCommand updateEmpRank = new SqlCommand("UPDATE Users SET Permissions = @param2 WHERE UserID = @param1;", dbt.AccessDB());

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

        public void UpdateEmpAct(string empID, int activity)
        {
            //updates user's isActive according to the predetermined value

            SqlCommand updateEmpAct = new SqlCommand("UPDATE Users SET isActive = @param2 WHERE UserID = @param1;", dbt.AccessDB());

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

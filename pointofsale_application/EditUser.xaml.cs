﻿using System;
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

        public void AddEmp(string empName, string empPermissions, int isActive)
        {
            //creates a new employee with predetermined values

            SqlCommand addEmp = new SqlCommand("INSERT INTO Users (EmployeeName, Permissions, isActive) VALUES (@param1, @param2, @param3);", dbt.AccessDB());

            addEmp.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = empName;
            addEmp.Parameters.Add("@param2", SqlDbType.VarChar, 255).Value = empPermissions;
            addEmp.Parameters.Add("@param3", SqlDbType.Bit).Value = isActive;
            addEmp.CommandType = CommandType.Text;

            try
            {
                addEmp.ExecuteNonQuery();
            } catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");

            }

        }

        public void UpdateEmpRank(int empID, string empPermissions)
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

        public void UpdateEmpAct(int empID, int activity)
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

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser newUser = new AddUser();
            newUser.Show();
        }
    }
}

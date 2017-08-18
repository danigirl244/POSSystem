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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        Item product;
        DatabaseAccess dbt = new DatabaseAccess();

        public EditProduct(Item product)
        {  
            InitializeComponent();
            this.product = product;
            pullInfo();
        }

        public EditProduct()
        {
        }

        public void pullInfo()
        {
            int qty = 0;
            string desc = "";

            SqlCommand categories = new SqlCommand("SELECT QtyOnHand, [Desc] FROM Inventory WHERE SKU = @param1", dbt.AccessDB());
            categories.Parameters.Add("@param1", SqlDbType.Int).Value = product.SKU;

            SqlDataReader rd;
            rd = categories.ExecuteReader();
            while (rd.Read())
            {
                qty = rd.GetInt32(rd.GetOrdinal("QtyOnHand"));
                desc = rd.GetString(rd.GetOrdinal("Desc"));
            }

            initializeText(qty, desc);
        }

        public void initializeText(int qty, string desc)
        {
            prodID.Text = product.SKU.ToString();
            prodName.Text = product.Name.ToString();
            prodPrice.Text = product.Price.ToString();
            prodQuant.Text = qty.ToString();
            prodDesc.Text = desc;
            cat.Text = product.Category.ToString();

            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to permanently delete ", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                DeleteItem(product.SKU);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you like to save these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                EditItem(prodID.Text, prodQuant.Text, prodPrice.Text, prodName.Text, prodDesc.Text, cat.Text);
            }
        }

        public void DeleteItem(int itemID)
        {
            //delete from inventory table according to the itemId of the button pushed
            SqlCommand deleteItem = new SqlCommand("DELETE FROM Inventory WHERE SKU = @param1;", dbt.AccessDB());

            deleteItem.Parameters.Add("@param1", SqlDbType.Int).Value = itemID;
            deleteItem.CommandType = CommandType.Text;

            try
            {
                deleteItem.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
            }
        }

        public void EditItem(string itemID, string qty, string price, string name, string desc, string category)
        {
            //Using an update command according to the itemID to update editted fields
            SqlCommand editItem = new SqlCommand("UPDATE Inventory SET QtyOnHand = @param1, Price = @param2, Name = @param3, [Desc] = @param4, Category = @param5 WHERE SKU = @param6;", dbt.AccessDB());

            editItem.Parameters.Add("@param1", SqlDbType.Int).Value = qty;
            editItem.Parameters.Add("@param2", SqlDbType.Money).Value = price;
            editItem.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = name;
            editItem.Parameters.Add("@param4", SqlDbType.Text).Value = desc;
            editItem.Parameters.Add("@param5", SqlDbType.VarChar, 255).Value = category;
            editItem.Parameters.Add("@param6", SqlDbType.Int).Value = itemID;
            editItem.CommandType = CommandType.Text;

            try
            {
                editItem.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
            }

        }
    }
}
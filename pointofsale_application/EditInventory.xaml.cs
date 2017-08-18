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
    /// Interaction logic for EditInventory.xaml
    /// </summary>
    public partial class EditInventory : Window
    {


        DatabaseAccess dbt = new DatabaseAccess();

        public EditInventory(List<Item> Inventory)
        {
            InitializeComponent();
            fillItemColumn(Inventory);
            
        }

        public EditInventory()
        {
        }

        public void fillItemColumn(List<Item> category)
        {

            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button newBtn = new Button();
                    if (category.Count > count)
                    {
                        newBtn.Content = category[count].Name.ToString();
                        newBtn.Name = "Button" + count.ToString();
                        newBtn.Click += new RoutedEventHandler(btn_Click);
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //method that brings up the edit window according to the designated items
            EditProduct editProd = new EditProduct();
            editProd.Show();
        }
         private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to permanently delete this product?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you like to save these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        public void CreateItem(int qty, double price, string name, string desc, string category)
        {
            //Insert new item into inventory table according to the information entered
            //SqlCommand createItem = new SqlCommand("INSERT INTO Inventory (QtyOnHand, Price, Name, [Desc], Category, NumPurchased) VALUES (" + qty + ", " + price + ", " + name + ", " + desc + ", " + category + ", 0);", dbt.AccessDB());
            SqlCommand createItem = new SqlCommand("INSERT INTO Inventory (QtyOnHand, Price, Name, [Desc], Category, NumPurchased) VALUES (@param1, @param2, @param3, @param4, @param5, 700);", dbt.AccessDB());

            createItem.Parameters.Add("@param1", SqlDbType.Int).Value = qty;
            createItem.Parameters.Add("@param2", SqlDbType.Money).Value = price;
            createItem.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = name;
            createItem.Parameters.Add("@param4", SqlDbType.Text).Value = desc;
            createItem.Parameters.Add("@param5", SqlDbType.VarChar, 255).Value = category;
            createItem.CommandType = CommandType.Text;
            //createItem.ExecuteNonQuery();
            
            try
            {
                createItem.ExecuteNonQuery();
            } catch(SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
            }

        }

        public void EditItem(int itemID, int qty, double price, string name, string desc, string category)
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
            } catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
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
            }catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addprod = new AddProduct();
            addprod.Show();
        }
    }
}

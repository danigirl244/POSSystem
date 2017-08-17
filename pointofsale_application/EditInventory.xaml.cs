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

        public EditInventory()
        {
            InitializeComponent();
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
            SqlCommand createItem = new SqlCommand("INSERT INTO Inventory (QtyOnHand, Price, Name, [Desc], Category, NumPurchased) VALUES (" + qty + ", " + price + ", " + name + ", " + desc + ", " + category + ", 0);", dbt.AccessDB());
            
        }

        public void EditItem(int itemID, int qty, double price, string name, string desc, string category)
        {
            //Using an update command according to the itemID to update editted fields
            SqlCommand editItem = new SqlCommand("UPDATE Inventory SET QtyOnHand = " + qty + ", Price = " + price + ", Name = " + name + ", [Desc] = " + desc + ", Category = " + category + " WHERE SKU = " + itemID + ";", dbt.AccessDB());

        }

        public void DeleteItem(int itemID)
        {
            //delete from inventory table according to the itemId of the button pushed
            SqlCommand deleteItem = new SqlCommand("DELETE FROM Inventory WHERE SKU = " + itemID + ";", dbt.AccessDB());
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
    }
}

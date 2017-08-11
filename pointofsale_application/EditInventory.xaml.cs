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
        public EditInventory()
        {
            InitializeComponent();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to permanently delete this product?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Would you like to save these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        //create separate onClick method that calls these methods with the parameters

        public void createItem(int qty, double price, string name, string desc, string category)
        {
            //ensuring that these values arent null
            //Don't know if it's necessary
            //connect to database
            //Insert new item into inventory table according to the information entered
            //might need dbo. before inventory
            //syntax might be incorrect for command
            SqlCommand insertItem = new SqlCommand("INSERT INTO Inventory (QtyOnHand, Price, Name, [Desc], Category, NumPurchased) VALUES (" + qty + ", " + price + ", " + name + ", " + desc + ", " + category + ", 0");
            insertItem.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = insertItem;
            //Might not need adapter if not pulling information from the database
            //DataSet dataSet = new DataSet();
            //adapter.Fill(dataSet);
            

        }

        //Is there a button to edit an item?
        public void editItem(int itemID, int qty, double price, string name, string desc, string category)
        {
            //connect to database
            //Using an update command according to the itemID to update editted fields
            SqlCommand editItemCom = new SqlCommand("UPDATE Inventory SET QTYOnHand = " + qty + ", Price = " + price + ", Name = " + name + ", [Desc] = " + desc + ", Category = " + category + " WHERE SKU = " + itemID + ";");
            editItemCom.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = editItemCom;
        }

        public void deleteItem(int itemID)
        {
            //connect to database
            //delete from inventory table according to the itemId of the button pushed
            //syntax might be incorrect for command
            SqlCommand deleteItem = new SqlCommand("DELETE FROM Inventory WHERE SKU = " + itemID + ";");
            deleteItem.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = deleteItem;

        }
    }
}

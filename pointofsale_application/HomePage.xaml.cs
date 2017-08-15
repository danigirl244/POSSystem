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


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        DatabaseAccess access = new DatabaseAccess();
        private static double taxPercentage = .0685;
        private double subtotal;
        public double SubTotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }
        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }
        private double taxTotal;
        public double TaxTotal
        {
            get { return taxTotal; }
            set { taxTotal = value; }
        }

        List<Item> itemList = new List<Item>();
        List<Item> cartList = new List<Item>();
        public HomePage()
        {
            InitializeComponent();
            
        }

        public void InitializeItemList()
        {
            string fillItemList = "Select * From Inventory";
            SqlCommand fill = new SqlCommand(fillItemList);
            SqlDataReader dr;
            dr = fill.ExecuteReader();
            while (dr.Read())
            {
                itemList.Add(new Item()
                {
                    SKU = dr.GetInt32(dr.GetOrdinal("SKU")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    Price = dr.GetDouble(dr.GetOrdinal("Price")),
                    Category = dr.GetString(dr.GetOrdinal("[Desc]")),
                    NumPurchased = dr.GetInt32(dr.GetOrdinal("QtyOnHand"))
                });
            }

        }

        public void addItem()
        {
            //cartList.add(item)  'item' will be the oject of items.
            for (int i = 0; i < itemList.Count; i++)
            {
                /* 
                  if (((Item)itemList[i]).Name.Contains('ButtonStringValue'){
                     cartList.Add(itemList[i]);
                 }
                 */

            }
        }

        //Delete Item From 'Cart'
        public void removeItem()
        {
            //cartList.Remove() Removes item from list.
            for(int i = 0; i < cartList.Count; i++)
            {
                /*
                if (((Item)cartList[i]).Name.Contains('ButtonStringValue'){
                    cartList.Remove(i);
                }
                */
            }

        }
        
        public void sort(int sortType)
        {
            /*
            case 1
                Sort by Highest Purchased Items
            case 2
                Sort by category "Beer"
            case 3
                Sort by category "Wine"
            case 4
                Sort by category "Vodka"
            case 5
                Sort by category "Gin"
            case 6
                Sort by category "Whisky"
            case 7
                Sort by category "Tequilla"
             */
        }

        public void displayItems(int displayNum)
        {
            /*
            case 1
                Get all items in the list that have the highest NumPurchased
                Create buttons for each of those items
            case 2
                Get all items in the list that have a category of "Beer"
                Create buttons for each of those items
            case 3
                Get all items in the list that have a category of "Wine"
                Create buttons for each of those items
            case 4
                Get all items in the list that have a category of "Vodka"
                Create buttons for each of those items
            case 5
                Get all items in the list that have a category of "Gin"
                Create buttons for each of those items
            case 6
                Get all items in the list that have a category of "Whisky"
                Create buttons for each of those items
            case 7
                Get all items in the list that have a category of "Tequilla"
                Create buttons for each of those items
             */
        }


        //subtotal
        public double printSubTotal()
        {
            //Refreshes the subtotal each time an item is added to the 'cart'
            subtotal = 0;
            for (int i = 0; i < cartList.Count; i++)
            {
                subtotal += cartList[i].Price;
            }

            return subtotal;

        }

        //Final Price with Tax
        public double printTotal()
        {

            taxTotal = subtotal * taxPercentage;
            total = subtotal + taxTotal;

            return total;
        }


        public void CreateReceipt()
        {
            Receipt receipt = new Receipt(SubTotal, TaxTotal, Total, cartList, "CashierName", new DateTime(2017, 8, 14));
        }
    }
}

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
        List<Item> Inventory = new List<Item>();
        List<Item> BeerItems = new List<Item>();
        List<Item> VodkaItems = new List<Item>();
        List<Item> TequilaItems = new List<Item>();
        List<Item> WhiskeyItems = new List<Item>();
        List<Item> BourbonItems = new List<Item>();
        List<Item> WineItems = new List<Item>();

        public HomePage()
        {
            InitializeComponent();
            fillCategoryColumn();
            InitializeItemList();
            fillItemColumn();
        }

        public void InitializeItemList()
        {
            SqlCommand categories = new SqlCommand("SELECT SKU, Name, Price, Category, NumPurchased FROM Inventory ORDER by NumPurchased DESC", access.AccessDB());
            SqlDataReader rd;
            rd = categories.ExecuteReader();
            while (rd.Read())
            {
                Inventory.Add(new Item()
                {
                    SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                    Name = rd.GetString(rd.GetOrdinal("Name")),
                    Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                    Category = rd.GetString(rd.GetOrdinal("Category")),
                    NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                });
                for (int i = 0; i < Inventory.Count; i++)
                {
                    if (Inventory[i].Category.ToString() == "Beer")
                    {
                        BeerItems.Add(new Item()
                        {
                            SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                            Name = rd.GetString(rd.GetOrdinal("Name")),
                            Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                            Category = rd.GetString(rd.GetOrdinal("Category")),
                            NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                        });
                    }
                    else if (Inventory[i].Category.ToString() == "Vodka")
                    {
                        VodkaItems.Add(new Item()
                        {
                            SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                            Name = rd.GetString(rd.GetOrdinal("Name")),
                            Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                            Category = rd.GetString(rd.GetOrdinal("Category")),
                            NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                        });
                    }
                    else if (Inventory[i].Category.ToString() == "Tequila")
                    {
                        TequilaItems.Add(new Item()
                        {
                            SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                            Name = rd.GetString(rd.GetOrdinal("Name")),
                            Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                            Category = rd.GetString(rd.GetOrdinal("Category")),
                            NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                        });
                    }
                    else if (Inventory[i].Category.ToString() == "Bourbon")
                    {
                        BourbonItems.Add(new Item()
                        {
                            SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                            Name = rd.GetString(rd.GetOrdinal("Name")),
                            Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                            Category = rd.GetString(rd.GetOrdinal("Category")),
                            NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                        });
                    }
                    else if (Inventory[i].Category.ToString() == "Whiskey")
                    {
                        WhiskeyItems.Add(new Item()
                        {
                            SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                            Name = rd.GetString(rd.GetOrdinal("Name")),
                            Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                            Category = rd.GetString(rd.GetOrdinal("Category")),
                            NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                        });
                    }
                    else if (Inventory[i].Category.ToString() == "Wine")
                    {
                        WineItems.Add(new Item()
                        {
                            SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                            Name = rd.GetString(rd.GetOrdinal("Name")),
                            Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                            Category = rd.GetString(rd.GetOrdinal("Category")),
                            NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                        });
                    }
                    
                }
                
            }
            MessageBox.Show(WineItems[0].SKU.ToString());

        }
        public void fillCategoryColumn()
        {
            string[] cats = { "Best Sellers", "Beer", "Vodka", "Tequila", "Whiskey", "Bourbon", "Wine" };
            DatabaseAccess db = new DatabaseAccess();
            db.AccessDB();
            for (int i = 0; i < cats.Length/*mostpopularlength*/; i++)
            {
                Button newBtn = new Button();
                newBtn.Content = cats[i];
                newBtn.Name = "Button" + i;
                newBtn.Click += new RoutedEventHandler(btn_Click);
                CategoryColumn.Children.Add(newBtn); 
                
            }
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            fillItemColumn();
        }

        public void fillItemColumn()
        {
            int count = 0;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Button newBtn = new Button();
                    if (Inventory.Count > count)
                    {
                        newBtn.Content = Inventory[count].Name.ToString();
                        newBtn.Name = "Button" + count.ToString();
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
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

        private void CashoutButton_Click(object sender, RoutedEventArgs e)
        {
            CashOut cash = new CashOut();
            cash.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }
    }
}

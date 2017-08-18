using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        DatabaseAccess access = new DatabaseAccess();
        private static double taxPercentage = .0685;
        private string permission;
        public string Permission
        {
            get { return permission; }
            set { permission = value; }
        }
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
        List<Item> BestSellers = new List<Item>();
        List<Item> BeerItems = new List<Item>();
        List<Item> VodkaItems = new List<Item>();
        List<Item> TequilaItems = new List<Item>();
        List<Item> WhiskeyItems = new List<Item>();
        List<Item> BourbonItems = new List<Item>();
        List<Item> WineItems = new List<Item>();

        public AdminPage(string p)
        {
            Permission = p;
            InitializeComponent();
            fillCategoryColumn();
            InitializeItemList();
            InitializeBestSellersList();
            InitializeBeerList();
            InitializeVodkaList();
            InitializeTequilaList();
            InitializeWhiskeyList();
            InitializeBourbonList();
            InitializeWineList();

            DateTimeTransactionField.Text = DateTime.Now.ToString();

            fillItemColumn(BestSellers);

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
                    Name = rd.GetString(rd.GetOrdinal("Name")).Replace(" ", String.Empty),
                    Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                    Category = rd.GetString(rd.GetOrdinal("Category")),
                    NumPurchased = rd.GetInt32(rd.GetOrdinal("NumPurchased"))
                });
            }
        }

        public void InitializeBestSellersList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                BestSellers.Add(new Item()
                {
                    SKU = Inventory[i].SKU,
                    Name = Inventory[i].Name,
                    Price = Inventory[i].Price,
                    Category = Inventory[i].Category,
                    NumPurchased = Inventory[i].NumPurchased
                });
            }
        }

        public void InitializeBeerList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == "Beer")
                {
                    BeerItems.Add(new Item()
                    {
                        SKU = Inventory[i].SKU,
                        Name = Inventory[i].Name,
                        Price = Inventory[i].Price,
                        Category = Inventory[i].Category,
                        NumPurchased = Inventory[i].NumPurchased
                    });
                }
            }
        }

        public void InitializeVodkaList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == "Vodka")
                {
                    VodkaItems.Add(new Item()
                    {
                        SKU = Inventory[i].SKU,
                        Name = Inventory[i].Name.Replace(" ", String.Empty),
                        Price = Inventory[i].Price,
                        Category = Inventory[i].Category,
                        NumPurchased = Inventory[i].NumPurchased
                    });
                }
            }
        }

        public void InitializeTequilaList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == "Tequila")
                {
                    TequilaItems.Add(new Item()
                    {
                        SKU = Inventory[i].SKU,
                        Name = Inventory[i].Name,
                        Price = Inventory[i].Price,
                        Category = Inventory[i].Category,
                        NumPurchased = Inventory[i].NumPurchased
                    });
                }
            }
        }

        public void InitializeBourbonList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == "Bourbon")
                {
                    BourbonItems.Add(new Item()
                    {
                        SKU = Inventory[i].SKU,
                        Name = Inventory[i].Name,
                        Price = Inventory[i].Price,
                        Category = Inventory[i].Category,
                        NumPurchased = Inventory[i].NumPurchased
                    });
                }
            }
        }

        public void InitializeWhiskeyList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == "Whiskey")
                {
                    WhiskeyItems.Add(new Item()
                    {
                        SKU = Inventory[i].SKU,
                        Name = Inventory[i].Name,
                        Price = Inventory[i].Price,
                        Category = Inventory[i].Category,
                        NumPurchased = Inventory[i].NumPurchased
                    });
                }
            }
        }

        public void InitializeWineList()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == "Wine")
                {


                    WineItems.Add(new Item()
                    {
                        SKU = Inventory[i].SKU,
                        Name = Inventory[i].Name,
                        Price = Inventory[i].Price,
                        Category = Inventory[i].Category,
                        NumPurchased = Inventory[i].NumPurchased
                    });
                }
            }
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
                if (newBtn.Name == "Button0")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click0);
                }
                else if (newBtn.Name == "Button1")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click1);
                }
                else if (newBtn.Name == "Button2")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click2);
                }
                else if (newBtn.Name == "Button3")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click3);
                }
                else if (newBtn.Name == "Button4")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click4);
                }
                else if (newBtn.Name == "Button5")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click5);
                }
                else if (newBtn.Name == "Button6")
                {
                    newBtn.Click += new RoutedEventHandler(btn_Click6);
                }
                CategoryColumn.Children.Add(newBtn);

            }
        }
        private void btn_Click0(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            fillItemColumn(BestSellers);
        }
        private void btn_Click1(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            fillItemColumn(BeerItems);
        }
        private void btn_Click2(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            fillItemColumn(VodkaItems);
        }
        private void btn_Click3(object sender, RoutedEventArgs e)
        {
            fillItemColumn(TequilaItems);
        }
        private void btn_Click4(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            fillItemColumn(WhiskeyItems);
        }
        private void btn_Click5(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            fillItemColumn(BourbonItems);
        }
        private void btn_Click6(object sender, RoutedEventArgs e)
        {

            ItemGrid.Children.Clear();
            fillItemColumn(WineItems);
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
                        newBtn.Name = category[count].Name.ToString().Replace(" ", String.Empty);
                        newBtn.Click += (s, e) => { addItem(newBtn.Name); };
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        public void addItem(String s)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if ((Inventory[i]).Name.Contains(s))
                {
                    cartList.Add(Inventory[i]);
                }
            }

            SubtotalTransactionField.Text = "$ " + printSubTotal().ToString();
            TaxTransactionField.Text = "$ " + printTax().ToString();
            TotalTransactionField.Text = "$ " + printTotal().ToString();

        }

        //Delete Item From 'Cart'
        public void removeItem()
        {
            //cartList.Remove() Removes item from list.
            for (int i = 0; i < cartList.Count; i++)
            {
                /*
                if (((Item)cartList[i]).Name.Contains('ButtonStringValue'){
                    cartList.Remove(i);
                }
                */
            }

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

            return Math.Round(subtotal, 2);

        }

        public double printTax()
        {
            taxTotal = subtotal * taxPercentage;

            return Math.Round(taxTotal, 2);
        }

        //Final Price with Tax
        public double printTotal()
        {


            total = subtotal + taxTotal;

            return Math.Round(total, 2);
        }


        public void CreateReceipt()
        {
            Receipt receipt = new Receipt(SubTotal, TaxTotal, Total, cartList, "CashierName", new DateTime(2017, 8, 14));
        }

        private void CashoutButton_Click(object sender, RoutedEventArgs e)
        {
            CashOut cash = new CashOut(SubTotal, TaxTotal, Total, Permission, cartList);
            cash.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            EditUser user = new EditUser();
            user.Show();
       
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {

            Reports report = new Reports();
            report.Show();

        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {

            EditInventory editInventory = new EditInventory();
            editInventory.Show();

        }






    }
}

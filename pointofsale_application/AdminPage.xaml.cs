using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        EditUser user = new EditUser();
        Reports report = new Reports();
        EditInventory editInventory;


        DatabaseAccess db = new DatabaseAccess();
        private static double taxPercentage = .0685;
        private string permission;
        public string Permission
        {
            get { return permission; }
            set { permission = value; }
        }
        private double subtotal;
        public int numItems;
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

        private double txID;
        public double TxID
        {
            get { return txID; }
            set { txID = value; }
        }

        List<Item> itemList = new List<Item>();
        List<Item> cartList = new List<Item>();
        List<Button> cartButtonList = new List<Button>();
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
            FillCategoryColumn();
            InitializeItemList();
            InitializeBestSellersList();

            InitializeDrinkList("Beer", BeerItems);
            InitializeDrinkList("Vodka", VodkaItems);
            InitializeDrinkList("Tequila", TequilaItems);
            InitializeDrinkList("Bourbon", BourbonItems);
            InitializeDrinkList("Whiskey", WhiskeyItems);

            InitializeDrinkList("Wine", WineItems);
            DateTimeTransactionField.Text = DateTime.Now.ToString();
       
            ChangeCashierName(Login.StaticVars.CashierName);
            UpdateDateTime();
            ChangeOrderNum();

            editInventory = new EditInventory(Inventory);

            FillItemColumn(BestSellers);

        }

        public void InitializeItemList()
        {
            SqlCommand categories = new SqlCommand("SELECT SKU, Name, Price, Category, NumPurchased FROM Inventory ORDER by NumPurchased DESC", db.AccessDB());
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

       

        public void InitializeDrinkList(string type, List<Item> list)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Category == type)
                {

                    list.Add(new Item()
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

        public void FillCategoryColumn()
        {
            string[] cats = { "Best Sellers", "Beer", "Vodka", "Tequila", "Whiskey", "Bourbon", "Wine" };
            DatabaseAccess db = new DatabaseAccess();
            db.AccessDB();
            for (int i = 0; i < cats.Length/*mostpopularlength*/; i++)
            {
                Button newBtn = new Button();
                newBtn.Content = cats[i];
                newBtn.Name = "Button" + i;
                switch (i)
                {
                    case 0:
                        newBtn.Click += new RoutedEventHandler(Btn_Click0);
                        break;
                    case 1:
                        newBtn.Click += new RoutedEventHandler(Btn_Click1);
                        break;
                    case 2:
                        newBtn.Click += new RoutedEventHandler(Btn_Click2);
                        break;
                    case 3:
                        newBtn.Click += new RoutedEventHandler(Btn_Click3);
                        break;
                    case 4:
                        newBtn.Click += new RoutedEventHandler(Btn_Click4);
                        break;
                    case 5:
                        newBtn.Click += new RoutedEventHandler(Btn_Click5);
                        break;
                    case 6:
                        newBtn.Click += new RoutedEventHandler(Btn_Click6);
                        break;


                }
                CategoryColumn.Children.Add(newBtn);
            }
        }
        private void Btn_Click0(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            FillItemColumn(BestSellers);
        }
        private void Btn_Click1(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            FillItemColumn(BeerItems);
        }
        private void Btn_Click2(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            FillItemColumn(VodkaItems);
        }
        private void Btn_Click3(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            FillItemColumn(TequilaItems);
        }
        private void Btn_Click4(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            FillItemColumn(WhiskeyItems);
        }
        private void Btn_Click5(object sender, RoutedEventArgs e)
        {
            ItemGrid.Children.Clear();
            FillItemColumn(BourbonItems);
        }
        private void Btn_Click6(object sender, RoutedEventArgs e)
        {

            ItemGrid.Children.Clear();
            FillItemColumn(WineItems);
        }
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            TransactionBlock.Children.RemoveAt(0);
        }

        public void FillItemColumn(List<Item> category)
        {

            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button newBtn = new Button();
                    if (category.Count > count)
                    {
                        newBtn.Content = category[count].Name.ToString();
                        newBtn.Name = category[count].Name.ToString().Replace(" ", String.Empty);
                        newBtn.Click += (s, e) => { AddItem(newBtn.Name); };
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        public void AddItem(String str)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if ((Inventory[i]).Name.Contains(str))
                {
                    cartList.Add(Inventory[i]);
                    Button cartItem = new Button();
                    cartItem.Content = str;
                    cartItem.Name = str;

                    cartItem.Click += (s, e) => { RemoveItem(str); };
                    TransactionBlock.Children.Add(cartItem);
                    cartButtonList.Add(cartItem);
                    numItems++;
                }

            }

            SubtotalTransactionField.Text = "$ " + String.Format("{0:0.00}", PrintSubTotal());
            TaxTransactionField.Text = "$ " + String.Format("{0:0.00}", PrintTax());
            TotalTransactionField.Text = "$ " + String.Format("{0:0.00}", PrintTotal());


        }

        //Delete Item From 'Cart'
        public void RemoveItem(string s)
        {
            //cartList.Remove() Removes item from list.
            for (int i = 0; i < cartList.Count; i++)
            {
                
                if ((cartList[i]).Name.Contains(s)){
                    cartList.RemoveAt(i);
                    TransactionBlock.Children.RemoveAt(i);
                    cartButtonList.RemoveAt(i);
                    break;
                }
                
            }
            SubtotalTransactionField.Text = "$ " + String.Format("{0:0.00}", PrintSubTotal());
            TaxTransactionField.Text = "$ " + String.Format("{0:0.00}", PrintTax());
            TotalTransactionField.Text = "$ " + String.Format("{0:0.00}", PrintTotal());

        }

        //subtotal
        public double PrintSubTotal()
        {
            //Refreshes the subtotal each time an item is added to the 'cart'
            subtotal = 0;
            for (int i = 0; i < cartList.Count; i++)
            {
                subtotal += cartList[i].Price;
            }

            subtotal = Math.Round(subtotal, 2);
            return Math.Round(subtotal, 2);

        }

        public double PrintTax()
        {
            TaxTotal = subtotal * taxPercentage;
            TaxTotal = Math.Round(taxTotal, 2);
            return Math.Round(taxTotal, 2);
        }

        //Final Price with Tax
        public double PrintTotal()
        {
            Total = subtotal + taxTotal;
            Total = Math.Round(total, 2);
            return Math.Round(total, 2);
        }


        public void CreateReceipt()
        {
            Receipt receipt = new Receipt(SubTotal, TaxTotal, Total, cartList, "CashierName", new DateTime(2017, 8, 14));
        }

        private void CashoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (cartList.Count != 0)
            {
                CashOut cash = new CashOut(SubTotal, TaxTotal, Total, Permission, cartList, Inventory);
                cash.Show();
                App.Current.MainWindow = cash;
                this.Close();
            }
            else
            {
                MessageBox.Show("Cannot Cash Out When Cart Is Empty", "Error");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                Login login = new Login();
                login.Show();
                App.Current.MainWindow = login;
                this.Close();
            }
        }
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            user.Show();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            report.Show();
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {

            editInventory.Show();

        }



        private void ChangeOrderNum()
        {
            try
            {
                SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(TxID) FROM Tx", db.AccessDB());
                TxID = (int)retrieveOrderNum.ExecuteScalar() + 1;
                OrderNumberBlock.Text = TxID.ToString();
            }
            catch (Exception e)
            {
                TxID = 1;
                OrderNumberBlock.Text = txID.ToString();
            }
        }

        private void ChangeCashierName(string cashierName)
        {
            CashierTransactionField.Text = cashierName.ToString();
        }

        private void UpdateDateTime()
        {
            DateTimeTransactionField.Text = DateTime.Now.ToString();
        }


    }
}

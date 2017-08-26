using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for CashOut.xaml
    /// </summary>
    public partial class CashOut : Window
    {
        DatabaseAccess db = new DatabaseAccess();

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
        private double txID;
        public double TxID
        {
            get { return txID; }
            set { txID = value; }
        }

        List<Item> cart = new List<Item>();
        List<Button> cartButton = new List<Button>();
        List<Item> Inventory = new List<Item>();
        List<string> duplicates = new List<string>();
        List<string> receiptData = new List<string>();

        public CashOut(double sub, double taxTotal, double total, string permissionString, List<Item> cartList, List<Item> Inventory)
        {
            InitializeComponent();
            UpdateDateTime();
            ChangeOrderNum();
            ChangeCashierName(Login.StaticVars.CashierName);

            SubTotal = sub;
            TaxTotal = taxTotal;
            Total = total;
            permission = permissionString;
            cart = cartList;
            this.Inventory = Inventory;

            for (int i = 0; i < cart.Count; i++)
            {
                Button cartI = new Button();
                cartI.Content = cart[i].Name + "  $ " + String.Format("{0:0.00}", cart[i].Price);
                cartI.Name = "Button" + i;
                cartButton.Add(cartI);
                TransactionBlock.Children.Add(cartButton[i]);

            }

            SubTotal = Math.Round(SubTotal, 2);
            SubtotalTransactionField.Text = "$ " + String.Format("{0:0.00}", SubTotal);
            TaxTotal = Math.Round(TaxTotal, 2);
            TaxTransactionField.Text = "$ " + String.Format("{0:0.00}", TaxTotal);
            Total = Math.Round(Total, 2);
            TotalTransactionField.Text = "$ " + String.Format("{0:0.00}", Total);

            RemainingBalance.Text = String.Format("{0:0.00}", Total);
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            //Will go through the cart list and for each item in there, it will decrease the quantity of that specific item by 1 in the database.
            for (int i = 0; i < cart.Count; i++)
            {
                int qty = 0;

                SqlCommand categories = new SqlCommand("SELECT QtyOnHand FROM Inventory WHERE SKU = @param1", db.AccessDB());
                categories.Parameters.Add("@param1", SqlDbType.Int).Value = cart[i].SKU;

                SqlDataReader rd;
                rd = categories.ExecuteReader();
                while (rd.Read())
                {
                    qty = rd.GetInt32(rd.GetOrdinal("QtyOnHand"));
                }

                cart[i].NumPurchased += 1;
                SqlCommand createItem = new SqlCommand("UPDATE Inventory SET QtyOnHand = @param2, NumPurchased = @param3 WHERE SKU = @param1", db.AccessDB());
                createItem.Parameters.Add("@param1", SqlDbType.Int).Value = cart[i].SKU;
                createItem.Parameters.Add("@param2", SqlDbType.Int).Value = qty - 1;
                createItem.Parameters.Add("@param3", SqlDbType.Int).Value = cart[i].NumPurchased;

                try
                {
                    createItem.ExecuteNonQuery();
                }
                catch (SqlException y)
                {
                    MessageBox.Show(y.Message.ToString(), "Error Message");
                }
            }
            MessageBoxResult popUp = MessageBox.Show("Transaction Record" + Environment.NewLine + " Change Due: " + ChangeDue.Text, "Check Out");

            //Compares the Cart to Inventory to find duplicates and adds the qty to the duplicates array

            for (int i = 0; i < Inventory.Count; i++)
            {
                int dupCount = 0;

                for (int j = 0; j < cart.Count; j++)
                {
                    if (Inventory[i].SKU == cart[j].SKU && i < cart.Count)
                    {
                        dupCount++;
                        if (duplicates.Contains(Inventory[i].Name + "," + (dupCount - 1)) == true)
                        {
                            duplicates.Remove(Inventory[i].Name + "," + (dupCount - 1));
                            duplicates.Add(Inventory[i].Name + "," + dupCount);
                        }
                        else
                        {
                            duplicates.Add(Inventory[i].Name + "," + dupCount);
                        }
                    }
                }
            }

            SubmitTx();
            SaveTx();

            Reports.tillCount -= Total;
            SqlCommand ttlUpdate = new SqlCommand("UPDATE TillCount SET Till = Till " + "+" + Total, db.AccessDB());
            try
            {
                ttlUpdate.ExecuteNonQuery();
            }
            catch (SqlException y)
            {
                MessageBox.Show(y.Message.ToString(), "Error Message");
            }

            if (permission.Equals("admin"))
            {
                AdminPage adminpage = new AdminPage(permission);
                adminpage.Show();
                App.Current.MainWindow = adminpage;
                this.Close();
            }
            else if (permission.Equals("basic"))
            {
                HomePage homepage = new HomePage(permission);
                homepage.Show();
                App.Current.MainWindow = homepage;
                this.Close();
            }
        }

        public void SaveTx()
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            for (int x = 0; x < duplicates.Count; x++)
            {
                string[] test = duplicates[x].Split(',');

                for (int y = 0; y < Inventory.Count; y++)
                {
                    if (test[0] == Inventory[y].Name)
                    {
                        receiptData.Add(duplicates[x] + ",Item Price: $" + Inventory[y].Price + ",Subtotal: $" + subtotal + ",Total: $" + total);
                    }
                }
            }

            System.IO.File.WriteAllLines(path + @"\Receipt.txt", receiptData);

        }


        public void SubmitTx(/*double TxID, string SKU, string price, string qty, string date, string UserID, double Subtotal, double Total, string Tender*/)
        {
            for (int i = 0; i < duplicates.Count; i++)
            {
                //Insert transaction data into the Order Information table according to the information entered

                string[] test = duplicates[i].Split(',');
                int SKU = 0;
                double price = 0;

                for (int x = 0; x < Inventory.Count; x++)
                {
                    if (Inventory[x].Name == test[0])
                    {
                        SKU = Inventory[x].SKU;
                        price = Inventory[x].Price;
                    }
                }


                SqlCommand submittx = new SqlCommand("INSERT INTO Tx (TxID, SKU, Price, Qty, DateTime, UserID, Subtotal, Total, Tender) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9);", db.AccessDB());

                submittx.Parameters.Add("@param1", SqlDbType.Int).Value = TxID;
                submittx.Parameters.Add("@param2", SqlDbType.Int).Value = SKU;
                submittx.Parameters.Add("@param3", SqlDbType.Money).Value = price;
                submittx.Parameters.Add("@param4", SqlDbType.Int).Value = test[1];
                submittx.Parameters.Add("@param5", SqlDbType.DateTime).Value = DateTime.Now;
                submittx.Parameters.Add("@param6", SqlDbType.Int).Value = Login.StaticVars.CashierID;
                submittx.Parameters.Add("@param7", SqlDbType.Money).Value = SubTotal;
                submittx.Parameters.Add("@param8", SqlDbType.Money).Value = Total;
                submittx.Parameters.Add("@param9", SqlDbType.VarChar, 255).Value = "cash";
                submittx.CommandType = CommandType.Text;

                try
                {
                    submittx.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message.ToString(), "Error Message");
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel the transaction?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (permission.Equals("admin"))
                {
                    AdminPage adminpage = new AdminPage(permission);
                    adminpage.Show();
                    App.Current.MainWindow = adminpage;
                    this.Close();
                }
                else if (permission.Equals("basic"))
                {
                    HomePage homepage = new HomePage(permission);
                    homepage.Show();
                    App.Current.MainWindow = homepage;
                    this.Close();
                }
            }
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
                MessageBox.Show(e.Message.ToString(), "Error message");
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


        public void PrintChange()
        {
            double change = Math.Round(double.Parse(RemainingBalance.Text) * -1, 2);
            ChangeDue.Text = "$ " + String.Format("{0:0.00}", change);
        }

        private void HundredButton_Click(object sender, RoutedEventArgs e)
        {
            double hundred = double.Parse(RemainingBalance.Text) - 100.00;
            RemainingBalance.Text = String.Format("{0:0.00}", hundred);
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void FiftyButton_Click(object sender, RoutedEventArgs e)
        {
            double fifty = double.Parse(RemainingBalance.Text) - 50.00;
            RemainingBalance.Text = String.Format("{0:0.00}", fifty);
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void Twenty_fiveButton_Click(object sender, RoutedEventArgs e)
        {
            double twentyfive = double.Parse(RemainingBalance.Text) - 25.00;
            RemainingBalance.Text = String.Format("{0:0.00}", twentyfive);
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void TwentyButton_Click(object sender, RoutedEventArgs e)
        {
            double twenty = double.Parse(RemainingBalance.Text) - 20.00;
            RemainingBalance.Text = String.Format("{0:0.00}", twenty);
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void TenButton_Click(object sender, RoutedEventArgs e)
        {
            double ten = double.Parse(RemainingBalance.Text) - 10.00;
            RemainingBalance.Text = String.Format("{0:0.00}", ten);
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            double five = double.Parse(RemainingBalance.Text) - 5.00;
            RemainingBalance.Text = String.Format("{0:0.00}", five);
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void AddAmountButton_Click(object sender, RoutedEventArgs e)
        {
            double custPay;
            bool num = double.TryParse(InputBlock.Text, out custPay);

            if (num && double.Parse(InputBlock.Text) >= 0)
            {
                double currentBalance = double.Parse(RemainingBalance.Text) - double.Parse(InputBlock.Text);
                RemainingBalance.Text = String.Format("{0:0.00}", currentBalance);
                if (double.Parse(RemainingBalance.Text) <= 0)
                {
                    PrintChange();
                    CheckOutButton.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Invalid Payment!");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputBlock.Text = " ";
        }

        private void PeriodButton_Click(object sender, RoutedEventArgs e)
        {
            InputBlock.Text += ".";
        }

        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            double zero = 0;
            InputBlock.Text += zero.ToString();
        }

        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            double seven = 7;
            InputBlock.Text += seven.ToString();
        }

        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            double nine = 9;
            InputBlock.Text += nine.ToString();
        }

        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            double eight = 8;
            InputBlock.Text += eight.ToString();
        }

        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            double four = 4;
            InputBlock.Text += four.ToString();
        }

        private void FiveButton1_Click(object sender, RoutedEventArgs e)
        {
            double five = 5;
            InputBlock.Text += five.ToString();
        }

        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            double six = 6;
            InputBlock.Text += six.ToString();
        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            double one = 1;
            InputBlock.Text += one.ToString();
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            double two = 2;
            InputBlock.Text += two.ToString();
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            double three = 3;
            InputBlock.Text += three.ToString();
        }
    }
}



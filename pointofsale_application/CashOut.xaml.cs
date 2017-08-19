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
    /// Interaction logic for CashOut.xaml
    /// </summary>
    public partial class CashOut : Window
    {
        DatabaseAccess access = new DatabaseAccess();

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
        private double TxID;
        public double tranID
        {
            get { return TxID; }
            set { TxID = value; }
        }

        List<Item> cart = new List<Item>();
        List<Button> cartButton = new List<Button>();
        public CashOut(double sub, double t, double tot, string p, List<Item> l)
        {
            InitializeComponent();
            UpdateDateTime();
            ChangeOrderNum();

            SubTotal = sub;
            TaxTotal = t;
            Total = tot;
            permission = p;
            cart = l;
            
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

            RemainingBalance.Text = Total.ToString();

        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            //Will go through the cart list and for each item in there, it will decrease the quantity of that specific item by 1 in the database.
            for (int i = 0; i < cart.Count; i++)
            {
                int qty = 0;

                SqlCommand categories = new SqlCommand("SELECT QtyOnHand FROM Inventory WHERE SKU = @param1", access.AccessDB());
                categories.Parameters.Add("@param1", SqlDbType.Int).Value = cart[i].SKU;

                SqlDataReader rd;
                rd = categories.ExecuteReader();
                while (rd.Read())
                {
                    qty = rd.GetInt32(rd.GetOrdinal("QtyOnHand"));
                }

                cart[i].NumPurchased += 1;
                SqlCommand createItem = new SqlCommand("UPDATE Inventory SET QtyOnHand = @param2, NumPurchased = @param3 WHERE SKU = @param1", access.AccessDB());
                createItem.Parameters.Add("@param1", SqlDbType.Int).Value = cart[i].SKU;
                createItem.Parameters.Add("@param2", SqlDbType.Int).Value = qty-1;
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
            MessageBoxResult popUp = MessageBox.Show("Transaction Record" + Environment.NewLine + " Change Due: " +  "$" + ChangeDue.Text, "Check Out");
            Reports.tillCount -= Total;
            SqlCommand ttlUpdate = new SqlCommand("UPDATE TillCount SET Till = Till " + "-" + Total, access.AccessDB());
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
                this.Close();
            }
            else if (permission.Equals("basic"))
            {
                HomePage homepage = new HomePage(permission);
                homepage.Show();
                this.Close();
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
                    this.Close();
                }
                else if (permission.Equals("basic"))
                {
                    HomePage homepage = new HomePage(permission);
                    homepage.Show();
                    this.Close();
                }
            }
        }


        private void ChangeOrderNum()
        {
            try
            {
                SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(TxID) FROM Tx", access.AccessDB());
                tranID = (int)retrieveOrderNum.ExecuteScalar() + 1;
                OrderNumberBlock.Text = tranID.ToString();
            }
            catch (Exception e) 
            {
                tranID = 1;
                OrderNumberBlock.Text = TxID.ToString();
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
            ChangeDue.Text = System.Convert.ToString(change);
        }

        private void HundredButton_Click(object sender, RoutedEventArgs e)
        {
            double hundred = double.Parse(RemainingBalance.Text) - 100.00;
            RemainingBalance.Text = hundred.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void FiftyButton_Click(object sender, RoutedEventArgs e)
        {
            double fifty = double.Parse(RemainingBalance.Text) - 50.00;
            RemainingBalance.Text = fifty.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void Twenty_fiveButton_Click(object sender, RoutedEventArgs e)
        {
            double twentyfive = double.Parse(RemainingBalance.Text) - 25.00;
            RemainingBalance.Text = twentyfive.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void TwentyButton_Click(object sender, RoutedEventArgs e)
        {
            double twenty = double.Parse(RemainingBalance.Text) - 20.00;
            RemainingBalance.Text = twenty.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void TenButton_Click(object sender, RoutedEventArgs e)
        {
            double ten = double.Parse(RemainingBalance.Text) - 10.00;
            RemainingBalance.Text = ten.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                PrintChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            double five = double.Parse(RemainingBalance.Text) - 5.00;
            RemainingBalance.Text = five.ToString();
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

            if (num)
            {
                double currentBalance = double.Parse(RemainingBalance.Text) - double.Parse(InputBlock.Text);
                RemainingBalance.Text = currentBalance.ToString();
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

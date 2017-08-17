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

namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for CashOut.xaml
    /// </summary>
    public partial class CashOut : Window
    {
        public CashOut()
        {
            InitializeComponent();
           
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult popUp = MessageBox.Show("Transaction Record" + Environment.NewLine + " Change Due:" + ChangeDue.Text, "Check Out");
            this.Close();


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Cancelled = MessageBox.Show("Transaction Cancelled");
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        public void printChange()
        {
            double change = double.Parse(RemainingBalance.Text) * -1;
            ChangeDue.Text = System.Convert.ToString(change);
        }

        private void HundredButton_Click(object sender, RoutedEventArgs e)
        {
            double hundred = double.Parse(RemainingBalance.Text) - 100.00;
            RemainingBalance.Text = hundred.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                printChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void FiftyButton_Click(object sender, RoutedEventArgs e)
        {
            double fifty = double.Parse(RemainingBalance.Text) - 50.00;
            RemainingBalance.Text = fifty.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                printChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void Twenty_fiveButton_Click(object sender, RoutedEventArgs e)
        {
            double twentyfive = double.Parse(RemainingBalance.Text) - 25.00;
            RemainingBalance.Text = twentyfive.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                printChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void TwentyButton_Click(object sender, RoutedEventArgs e)
        {
            double twenty = double.Parse(RemainingBalance.Text) - 20.00;
            RemainingBalance.Text = twenty.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                printChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void TenButton_Click(object sender, RoutedEventArgs e)
        {
            double ten = double.Parse(RemainingBalance.Text) - 10.00;
            RemainingBalance.Text = ten.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                printChange();
                CheckOutButton.IsEnabled = true;
            }
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            double five = double.Parse(RemainingBalance.Text) - 5.00;
            RemainingBalance.Text = five.ToString();
            if (double.Parse(RemainingBalance.Text) <= 0)
            {
                printChange();
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
                    printChange();
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

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
            MessageBoxResult popUp = MessageBox.Show("Transaction Record" + Environment.NewLine +  " Change Due:", "Check Out");
            HomePage window1 = new HomePage();
            window1.Show();
            this.Close();
            



        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Cancelled = MessageBox.Show("Transaction Cancelled");
            HomePage window1 = new HomePage();
            window1.Show();
            this.Close();
        }
    }
}

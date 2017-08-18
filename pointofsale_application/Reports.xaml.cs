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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        DatabaseAccess access = new DatabaseAccess();
        public Reports()
        {
            InitializeComponent();
            UpdateDateTime();
            ChangeOrderNum();
        }

        // private void Button_Click(object sender, RoutedEventArgs e)
        // {

        // }

        private void ChangeOrderNum()
        {
            SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(TxID) FROM Tx", access.AccessDB());
            int orderNum = (int)retrieveOrderNum.ExecuteScalar() + 1;

            OrderNumberBlock.Text = orderNum.ToString();
        }

        private void ChangeCashierName(String cashierName)
        {
            CashierTransactionField.Text = cashierName;
        }

        private void UpdateDateTime()
        {
            DateTimeTransactionField.Text = DateTime.Now.ToString();
        }
    }
}

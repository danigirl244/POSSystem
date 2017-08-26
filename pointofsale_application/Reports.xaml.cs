using System;
using System.Data.SqlClient;
using System.Windows;


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    /// 
    
    public partial class Reports : Window
    {
        public static double tillCount = 5000.00;
        DatabaseAccess db = new DatabaseAccess();
        public Reports()
        {
            InitializeComponent();
            UpdateDateTime();
            ChangeOrderNum();
            PrintTill();
        }


        private void ChangeOrderNum()
        {
            SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(TxID) FROM Tx", db.AccessDB());
            int orderNum = (int)retrieveOrderNum.ExecuteScalar() + 1;

            OrderNumberBlock.Text = orderNum.ToString();
        }


        private void UpdateDateTime()
        {
            DateTimeTransactionField.Text = DateTime.Now.ToString();
        }

        public double PrintTill()
        {
            try
            {
                SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(Till) FROM TillCount", db.AccessDB());
                double newTill = Convert.ToDouble((decimal)retrieveOrderNum.ExecuteScalar());
                tillCount = newTill;
            }
            catch
            {
                tillCount = 5000;
            }
            Till_Count.Text = "$"  + tillCount.ToString();
            return tillCount;
        }
    }
}

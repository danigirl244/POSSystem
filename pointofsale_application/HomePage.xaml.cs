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

        List<Object> itemList = new List<Object>();
        List<Object> cartList = new List<Object>();
        public HomePage()
        {
            InitializeComponent();
            fillCategoryColumn();
            //InitializeItemList();
            
        }

        public void fillCategoryColumn()
        {
            for (int i = 0; i < 5/*mostpopularlength*/; i++)
            {
                System.Windows.Controls.Button newBtn = new Button();
                newBtn.Content = i.ToString();
                newBtn.Name = "Button" + i;
                categoryColumn.Children.Add(newBtn);
            }
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

        //subtotal
        public double printSubTotal()
        {

            foreach (object item in cartList)
            {
                //subtotal = subtotal + item.price; 'item.price' is a placeholder for whatever the price section is called
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

        private void CashoutButton_Click(object sender, RoutedEventArgs e)
        {
            CashOut cash = new CashOut();
            cash.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to log out?","confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }

        }
    }
}

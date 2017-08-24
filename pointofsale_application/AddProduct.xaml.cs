using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for AddProductPopUp.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        DatabaseAccess db = new DatabaseAccess();

        public AddProduct()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                this.Close();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            double x;
            if (double.TryParse(text, out x) && x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool IsNameAllowed(string text)
        {
            return Regex.IsMatch(text, "^[\\p{L}]+$");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (!IsTextAllowed(prodPrice.Text))
            {
                MessageBox.Show("Invalid price. Try again.", "Error");
            }
            else if (!IsTextAllowed(prodQuant.Text))
            {
                MessageBox.Show("Invalid quantity. Try again.", "Error");
            }
            else if (!IsNameAllowed(prodName.Text))
            {
                MessageBox.Show("Invalid Name. Use only letters.", "Error");
            }
            else
            {
                if (MessageBox.Show("Would you like to save these changes?", "Wait!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                    CreateItem(prodQuant.Text, prodPrice.Text, prodName.Text, prodDesc.Text, cat.Text);
                }
            }
        }

        public void CreateItem(string qty, string price, string name, string desc, string category)
        {
            //Insert new item into inventory table according to the information entered
            SqlCommand createItem = new SqlCommand("INSERT INTO Inventory (QtyOnHand, Price, Name, [Desc], Category, NumPurchased) VALUES (@param1, @param2, @param3, @param4, @param5, 0);", db.AccessDB());

            createItem.Parameters.Add("@param1", SqlDbType.Int).Value = qty;
            createItem.Parameters.Add("@param2", SqlDbType.Money).Value = price;
            createItem.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = name;
            createItem.Parameters.Add("@param4", SqlDbType.Text).Value = desc;
            createItem.Parameters.Add("@param5", SqlDbType.VarChar, 255).Value = category;
            createItem.CommandType = CommandType.Text;
            //createItem.ExecuteNonQuery();

            try
            {
                createItem.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error Message");
            }

        }
    }
}


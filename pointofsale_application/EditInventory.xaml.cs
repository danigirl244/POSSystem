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
    /// Interaction logic for EditInventory.xaml
    /// </summary>
    public partial class EditInventory : Window
    {


        DatabaseAccess dbt = new DatabaseAccess();

        public EditInventory(List<Item> Inventory)
        {
            InitializeComponent();
            fillItemColumn(Inventory);
            
        }

        public EditInventory()
        {
        }

        public void fillItemColumn(List<Item> Inventory)
        {

            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button newBtn = new Button();
                    if (Inventory.Count > count)
                    {
                        newBtn.Content = Inventory[count].Name.ToString();
                        newBtn.Name = Inventory[count].Name.ToString().Replace(" ", String.Empty);
                        newBtn.Click += (s, e) => { btn_Click(newBtn.Name, Inventory); }; ;
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        private void btn_Click(String s, List<Item> Inventory)
        {
            //method that brings up the edit window according to the designated items

            for(int x = 0; x < Inventory.Count; x++)
            {
                if(Inventory[x].Name.ToString().Replace(" ", String.Empty) == s)
                {
                    MessageBox.Show(Inventory[x].Name.ToString());
                    EditProduct editProd = new EditProduct(Inventory[x]);
                    editProd.Show();
                    break;
                }
            }
            
            
        }
        
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addprod = new AddProduct();
            addprod.Show();
        }
    }
}

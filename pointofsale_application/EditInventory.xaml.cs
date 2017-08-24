using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for EditInventory.xaml
    /// </summary>
    public partial class EditInventory : Window
    {


        DatabaseAccess db = new DatabaseAccess();

        public EditInventory(List<Item> Inventory)
        {
            InitializeComponent();
            FillItemColumn(Inventory);
            
        }


        public void FillItemColumn(List<Item> Inventory)
        {

            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button newBtn = new Button();
                    if (Inventory.Count > count)
                    {
                        newBtn.Content = Inventory[count].Name.ToString();
                        newBtn.Name = Inventory[count].Name.ToString().Replace(" ", String.Empty);
                        newBtn.Click += (s, e) => { Btn_Click(newBtn.Name, Inventory); }; ;
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        ItemGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }
        }

        private void Btn_Click(String s, List<Item> Inventory)
        {
            //method that brings up the edit window according to the designated items

            for(int i = 0; i < Inventory.Count; i++)
            {
                if(Inventory[i].Name.ToString().Replace(" ", String.Empty) == s)
                {
                    //MessageBox.Show(Inventory[x].Name.ToString());
                    EditProduct editProd = new EditProduct(Inventory[i]);
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

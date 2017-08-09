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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        
        public EditUser()
        {
            InitializeComponent();
        }

        
private void ActiveDeactive_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {

            }
           

        }

        private void Promote_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
        }

        private void Promote2_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to do this?", "confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

            }
            else
            {
                
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

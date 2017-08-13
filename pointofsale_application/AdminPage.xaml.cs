﻿using System;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
            fillCategoryColumn();
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


    }
}

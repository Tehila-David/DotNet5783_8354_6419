﻿using BO;
using PL.Order;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for OrderConfirmationWindow.xaml
    /// </summary>
    public partial class OrderConfirmationWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;

        public static readonly DependencyProperty CartDependency =
                DependencyProperty.Register(nameof(FinalCart),
                                        typeof(BO.Cart),
                                        typeof(CartWindow));
        public BO.Cart FinalCart
        {
            get => (BO.Cart)GetValue(CartDependency);
            private set => SetValue(CartDependency, value);
        }

        public OrderConfirmationWindow(BO.Cart myCart)
        {
            FinalCart = myCart;
            InitializeComponent();   
        }


        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)//ConfirmOrder Button
        {
            try
            {
                bl.Cart.CartConfirmation(FinalCart);
                FinalCart = null;
                Close();
                MessageBox.Show("Thanks for shopping with us. Your order has been successfully placed.");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

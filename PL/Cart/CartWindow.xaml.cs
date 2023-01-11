using BO;
using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get()!;

        public static readonly DependencyProperty CartItemsDependency =
       DependencyProperty.Register("CartItems",
                               typeof(ObservableCollection<OrderItem>),
                               typeof(CartWindow));
        public ObservableCollection<OrderItem> CartItems
        {
            get => (ObservableCollection<OrderItem>)GetValue(CartItemsDependency);
            private set => SetValue(CartItemsDependency, value);
        }



        public static readonly DependencyProperty CartDependency =
                DependencyProperty.Register(nameof(Cart),
                                        typeof(BO.Cart),
                                        typeof(CartWindow));
        public BO.Cart Cart
        {
            get => (BO.Cart)GetValue(CartDependency);
            private set => SetValue(CartDependency, value);
        }



        public CartWindow(BO.Cart myCart)
        {
            Cart = myCart;
            var item = bl.Cart.cartItems(Cart);
            CartItems = item == null ? new() : new(item);
            InitializeComponent();
            DataContext = CartItems;
        }

        public int id { get; set; }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.UpdateProductAmount(Cart, id, 0);
                
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public int newAmount { get; set; }
        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                bl.Cart.UpdateProductAmount(Cart, id, newAmount);
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void EmptyCart_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in CartItems) //removing all items from cart by settong their amount to zero
            {
                bl.Cart.UpdateProductAmount(Cart, item.ProductID, 0);
            }
        }
        private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BackToCatalog_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}

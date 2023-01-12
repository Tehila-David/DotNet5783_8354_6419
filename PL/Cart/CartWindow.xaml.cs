using BlApi;
using BO;
using DO;
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
                               typeof(ObservableCollection<BO.OrderItem>),
                               typeof(CartWindow));
        public ObservableCollection<BO.OrderItem> CartItems
        {
            get => (ObservableCollection<BO.OrderItem>)GetValue(CartItemsDependency);
            private set => SetValue(CartItemsDependency, value);
        }


       // public ObservableCollection<OrderItem> CartItems = new ObservableCollection<OrderItem>();




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
            var items = bl.Cart.cartItems(myCart); //returning list of cart items 
            CartItems = items == null ? new() : new(items);
            InitializeComponent();
            DataContext = CartItems;
         }

      //  public int ProductID { get; set; }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
                bl.Cart.UpdateProductAmount(Cart, orderItem.ProductID, 0);
                
                
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
       // public int Amount { get; set; }
        

        private void EmptyCart_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in CartItems) //removing all items from cart by settong their amount to zero
            {
                bl.Cart.UpdateProductAmount(Cart, item.ProductID, 0);
            }
        }
        
        private void BackToCatalog_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CatalogWindow(Cart).Show();
        }

        private void OrderItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
                bl.Cart.UpdateProductAmount(Cart, orderItem.ProductID, (orderItem.Amount) + 1);
                var items = bl.Cart.cartItems(Cart); //returning list of cart items 
                CartItems = items == null ? new() : new(items);
                DataContext = CartItems;
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
        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = (BO.OrderItem)((sender as Button)!.DataContext!);
                bl.Cart.UpdateProductAmount(Cart, orderItem.ProductID, (orderItem.Amount) - 1);
                var items = bl.Cart.cartItems(Cart); //returning list of cart items 
                CartItems = items == null ? new() : new(items);
                DataContext = CartItems;
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

        private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new OrderConfirmationWindow(Cart).Show();
        }


    }
}

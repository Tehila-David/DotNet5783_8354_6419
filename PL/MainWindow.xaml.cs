
using PL.Order;
using System.Windows;
using System;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private void MainWindow_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

        public MainWindow()
        {
           InitializeComponent();
            Back.Visibility = Visibility.Collapsed;//hide butten of Orderslist
            Orders_List.Visibility = Visibility.Hidden;//hide butten of Orderslist
            Products_List.Visibility = Visibility.Hidden;//hide butten of Productslist
        }
        BlApi.IBl? bl = BlApi.Factory.Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            Manager.Visibility = Visibility.Hidden;//hide butten of Manager
            New_Order.Visibility = Visibility.Hidden;//hide butten of New_Order
            Order_Tracking.Visibility = Visibility.Hidden;//hide butten of  Order_Tracking
            Orders_List.Visibility = Visibility.Visible;//Show butten of Orderslist
            Products_List.Visibility = Visibility.Visible;///Show butten of Productslist
            Back.Visibility = Visibility.Visible;//Show butten of Back
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart myCart = new BO.Cart()
            {
                Items = new()
            };
            new CatalogWindow(myCart).Show();
        }

        private void OrderTracking_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrackingWindow().Show();
        }

        private void ProductsList_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
           
        }

        private void OrdersList_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().Show();
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.Visibility=Visibility.Visible;//show butten of manager
            New_Order.Visibility = Visibility.Visible;//show butten of New_Order
            Order_Tracking.Visibility = Visibility.Visible;//Show butten of  Order_Tracking
            Orders_List.Visibility = Visibility.Hidden;//hide butten of Orderslist
            Products_List.Visibility = Visibility.Hidden;///hide butten of Productslist
        }

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            SimulatorWindow window = new SimulatorWindow();
            window.Show();
           
        }
    }
}

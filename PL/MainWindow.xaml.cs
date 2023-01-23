
using PL.Order;
using System.Windows;
using System;
using System.Media;
using System.Windows.Controls;
using System.Windows.Media;
using System.Numerics;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player = new MediaPlayer();
      
        
        private void PlaySound(string filePath)
        {
            player.Open(new Uri(filePath));
            PLAY.Source = player.Source;
            player.Play();
        }
       
        public MainWindow()
        {
           InitializeComponent();
            
            Orders_List.Visibility = Visibility.Hidden;//hide butten of Orderslist
            Products_List.Visibility = Visibility.Hidden;//hide butten of Productslist
            New_Order.Visibility = Visibility.Hidden;//hide butten of New_Order
            Order_Tracking.Visibility = Visibility.Hidden;//hide butten of  Order_Tracking
            PlaySound(@"C:\Users\tehil\source\repos\tehila859\DotNet5783_8354_6419\PL\Images+Music\dreams.mp3");

        }
        BlApi.IBl? bl = BlApi.Factory.Get();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            
            Orders_List.Visibility = Visibility.Visible;//Show butten of Orderslist
            Products_List.Visibility = Visibility.Visible;//Show butten of Productslist
            New_Order.Visibility = Visibility.Hidden;//hide butten of New_Order
            Order_Tracking.Visibility = Visibility.Hidden;//hide butten of  Order_Tracking

           

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

      

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            SimulatorWindow window = new SimulatorWindow();
            window.Show();
           
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            New_Order.Visibility = Visibility.Visible;//Show butten of New_Order
            Order_Tracking.Visibility = Visibility.Visible;//Show butten of  Order_Tracking
            Orders_List.Visibility = Visibility.Hidden;//hide butten of Orderslist
            Products_List.Visibility = Visibility.Hidden;//hide butten of Productslist
        }

        private void StopMusic_Click(object sender, RoutedEventArgs e)
        {

          player.Stop();


        }

        private void StartMusic_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
        }
    }
}

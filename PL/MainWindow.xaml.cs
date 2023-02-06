
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
      
        
        private void PlaySound(string filePath)//BackGround Music
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
        
     
        private void Manager_Click(object sender, RoutedEventArgs e)//Manager Button
        {
            
            Orders_List.Visibility = Visibility.Visible;//Show butten of Orderslist
            Products_List.Visibility = Visibility.Visible;//Show butten of Productslist
            New_Order.Visibility = Visibility.Hidden;//hide butten of New_Order
            Order_Tracking.Visibility = Visibility.Hidden;//hide butten of  Order_Tracking
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)//New Order Button 
        {
            BO.Cart myCart = new BO.Cart()
            {
                Items = new()
            };
            new CatalogWindow(myCart).Show();
        }

        private void OrderTracking_Click(object sender, RoutedEventArgs e)//Order Tracking Button
        {
            new OrderTrackingWindow().Show();
        }

        private void ProductsList_Click(object sender, RoutedEventArgs e)//Products List Button
        {
            new ProductListWindow().Show();
           
        }

        private void OrdersList_Click(object sender, RoutedEventArgs e)//Orders List Button
        {
            new OrderListWindow().Show();
            
        }



        private void Simulator_Click(object sender, RoutedEventArgs e)//Simulator Button
        {
            SimulatorWindow window = new SimulatorWindow();
            window.Show();

        }

        private void Client_Click(object sender, RoutedEventArgs e)//Client Button
        {
            New_Order.Visibility = Visibility.Visible;//Show butten of New_Order
            Order_Tracking.Visibility = Visibility.Visible;//Show butten of  Order_Tracking
            Orders_List.Visibility = Visibility.Hidden;//hide butten of Orderslist
            Products_List.Visibility = Visibility.Hidden;//hide butten of Productslist
        }

        private void StopMusic_Click(object sender, RoutedEventArgs e)// Stop Music Button
        {

          player.Stop();


        }

        private void StartMusic_Click(object sender, RoutedEventArgs e)// Start Music Button
        {
            player.Play();
        }
    }
}

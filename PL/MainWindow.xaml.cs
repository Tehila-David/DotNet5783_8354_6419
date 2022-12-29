
using PL.Order;
using System.Windows;


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
        }
        BlApi.IBl? bl = BlApi.Factory.Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
            
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().Show();
        }

        private void OrderTracking_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

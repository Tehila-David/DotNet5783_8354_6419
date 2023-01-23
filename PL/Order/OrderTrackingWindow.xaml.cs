
using BO;
using DO;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public OrderTrackingWindow()
        {
            InitializeComponent();
            
        }
        public int id { get; set; }
        private void OrderTracking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                OrderTracking orderTracking = bl.Order.followOrder(id);
                new ShowTrackingWindow(id).Show();
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}

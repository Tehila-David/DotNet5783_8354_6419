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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public OrderListWindow()
        {
            InitializeComponent();
            OrdersList.ItemsSource = bl.Order.GetListedOrders();
        }

        private void OrdersList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            BO.OrderForList order = new BO.OrderForList();
            order = listBox.SelectedItem as BO.OrderForList;
            new OrderWindow(order.ID).Show();
            Close();
        }

        
    }
}

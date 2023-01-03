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
using System.Xml.Linq;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        int OrderId = 0;
        int OrderItemId = 0;
        BlApi.IBl bl = BlApi.Factory.Get()!;
        public OrderWindow(int id)
        {
            InitializeComponent();
            OrderID.Text = bl.Order.GetByID(id).ID.ToString();
            ClientName.Text = bl.Order.GetByID(id).CustomerName.ToString();
            ClientEmail.Text = bl.Order.GetByID(id).CustomerEmail.ToString();
            ClientAddress.Text = bl.Order.GetByID(id).CustomerAddress.ToString();
            OrderDate.Text = bl.Order.GetByID(id).OrderDate.ToString();
            DeliveryDate.Text = bl.Order.GetByID(id).DeliveryDate.ToString();
            ShipDate.Text = bl.Order.GetByID(id).ShipDate.ToString();
            TotalPrice.Text = bl.Order.GetByID(id).TotalPrice.ToString();
            OrderStatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            OrderStatusSelector.Text=bl.Order.GetByID(id).Status.ToString();
            ItemsList.ItemsSource = bl.Order.GetByID(id).Items;
            OrderId = id;

        }

     
        private void Add_newItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //BO.Order order = bl.Order.GetByID(OrderId);
                bl.Order.GetByID(OrderId).ID = int.Parse(OrderID.Text);
                bl.Order.GetByID(OrderId).CustomerName = ClientName.Text;
                bl.Order.GetByID(OrderId).CustomerEmail = ClientEmail.Text;
                bl.Order.GetByID(OrderId).CustomerAddress = ClientAddress.Text;
                bl.Order.GetByID(OrderId).ShipDate = DateTime.Parse(ShipDate.Text);
                bl.Order.GetByID(OrderId).DeliveryDate = DateTime.Parse(DeliveryDate.Text);
                bl.Order.GetByID(OrderId).Status = (BO.OrderStatus)OrderStatusSelector.SelectedItem;
                new OrderItemWindow(OrderId).Show();
                Close();
            }
            //catch (FormatException)
            //{
            //    MessageBox.Show("Check your input and try again");
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            
        }

        private void ItemsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bl.Order.GetByID(OrderId).ID = int.Parse(OrderID.Text);
            bl.Order.GetByID(OrderId).CustomerName = ClientName.Text;
            bl.Order.GetByID(OrderId).CustomerEmail = ClientEmail.Text;
            bl.Order.GetByID(OrderId).CustomerAddress = ClientAddress.Text;
            bl.Order.GetByID(OrderId).OrderDate = DateTime.Parse(OrderDate.Text);
            bl.Order.GetByID(OrderId).ShipDate = DateTime.Parse(ShipDate.Text);
            bl.Order.GetByID(OrderId).DeliveryDate = DateTime.Parse(DeliveryDate.Text);
            bl.Order.GetByID(OrderId).Status = (BO.OrderStatus)OrderStatusSelector.SelectedItem;

            ListBox listBox = sender as ListBox;
            BO.OrderItem orderItem = new BO.OrderItem();
            orderItem = listBox.SelectedItem as BO.OrderItem;
            new OrderItemWindow(OrderId,orderItem).Show();
            Close();
        }

        
    }
}

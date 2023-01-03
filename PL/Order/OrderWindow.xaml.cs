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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
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

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

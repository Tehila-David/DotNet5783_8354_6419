using DalApi;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
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
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get()!;

        int OrderID = 0;


        public static readonly DependencyProperty OrderItemDependency =
       DependencyProperty.Register(nameof(OrderItem),
                              typeof(BO.OrderItem),
                              typeof(OrderItemWindow));
        public BO.OrderItem? OrderItem
        {
            get => (BO.OrderItem)GetValue(OrderItemDependency);
            private set => SetValue(OrderItemDependency, value);
        }



        /// <summary>
        /// constructor for Update items of Order
        /// </summary>
        /// <param name="OrderID"></param>
        public OrderItemWindow(int OrderId)
        {
            InitializeComponent();
            OrderID = OrderId;
        }


        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                bl.Order.GetByID(OrderID).Items.RemoveAll(item => item.ID == OrderItem.ID);
                OrderItem.Name = bl.Product.GetById(OrderItem.ProductID).Name;
                OrderItem.Price = bl.Product.GetById(OrderItem.ProductID).Price;
                OrderItem.TotalPrice = OrderItem.Amount * OrderItem.Price;
                bl.Order.GetByID(OrderID).Items.Add(OrderItem);

            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            new OrderWindow(OrderID).Show();
            Close();




        }
    }
}


       

        
    


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
       DependencyProperty.Register(nameof(Order),
                              typeof(BO.Order),
                              typeof(OrderItemWindow));
        public BO.Order? Order
        {
            get => (BO.Order)GetValue(OrderItemDependency);
            private set => SetValue(OrderItemDependency, value);
        }




        public BO.OrderItem OrderItem = new BO.OrderItem();
        /// <summary>
        /// constructor for Adding new item to Order
        /// </summary>
        /// <param name="OrderID"></param>
        public OrderItemWindow(int OrderId)
        {

            InitializeComponent();
            OrderID = OrderId;
            Order = bl.Order.GetByID(OrderId);
            UpdateItem.Visibility = Visibility.Collapsed;
            DeleteItem.Visibility = Visibility.Collapsed;

        }
        public OrderItemWindow(int OrderId, int OrderItemId) //!!!???? לא מצליחה לעדכן מוצר שקיים בהזמנה איך עושים את זה     
        {
            InitializeComponent();
            OrderID = OrderId;
            OrderItem = bl.Order.GetByID(OrderID).Items.FirstOrDefault(item => item.ID == OrderItemId);
            Order = bl.Order.GetByID(OrderId);

        }

        public void UpdateItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                BO.Order order = new BO.Order();
                OrderItem = bl.Order.UpdateItems(bl.Order.GetByID(OrderID), OrderItem.ProductID, OrderItem.Amount,false);
                //Order1.Items.RemoveAll(item => item.ID == OrderItem.ID);
                //Order1.Items.Add(OrderItem);
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();


        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}









































//private void UpdateItem_Click(object sender, RoutedEventArgs e)
//{
//    BO.Product product = new BO.Product();
//    product = bl.Product.GetById(OrderItem.ProductID);
//    try
//    {
//        if (product.InStock >= OrderItem.Amount)
//        {
//            Order1.Items.RemoveAll(item => item.ID == OrderItem.ID);
//            OrderItem.Name = bl.Product.GetById(OrderItem.ProductID).Name;
//            OrderItem.Price = bl.Product.GetById(OrderItem.ProductID).Price;
//            OrderItem.TotalPrice = OrderItem.Amount * OrderItem.Price;
//            Order1.Items.Add(OrderItem);
//            product.InStock-=OrderItem.Amount-firstAmount;
//            bl.Product.Update(product);

//        }
//        else
//        {
//            throw new Exception("The amount of  products is not available");
//        }
//    }
//    catch (FormatException)
//    {
//        MessageBox.Show("Check your input and try again");
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }

//    //new OrderWindow(OrderID).Show();
//    Close();




//}


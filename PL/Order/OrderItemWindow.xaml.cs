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


        public static readonly DependencyProperty productIdDependency =
        DependencyProperty.Register(nameof(productId),
                              typeof(int),
                              typeof(OrderItemWindow));
        public int productId
        {
            get => (int)GetValue(productIdDependency);
            private set => SetValue(productIdDependency, value);
        }

        public static readonly DependencyProperty AmountDependency =
       DependencyProperty.Register(nameof(Amount),
                             typeof(int),
                             typeof(OrderItemWindow));
        public int Amount
        {
            get => (int)GetValue(AmountDependency);
            private set => SetValue(AmountDependency, value);
        }

        public static readonly DependencyProperty OrderDependency =
        DependencyProperty.Register(nameof(Order1),
                              typeof(BO.Order),
                              typeof(OrderItemWindow));
        public BO.Order? Order1
        {
            get => (BO.Order)GetValue(OrderDependency);
            private set => SetValue(OrderDependency, value);
        }

        
        /// <summary>
        /// constructor for Adding new item to Order
        /// </summary>
        /// <param name="OrderID"></param>
        public OrderItemWindow(int OrderId)
        {

            InitializeComponent();
            OrderID = OrderId;
            Order1 = bl.Order.GetByID(OrderId);
            UpdateItem.Visibility = Visibility.Hidden;
            DeleteItem.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// constructor for Updating exsits item from Order
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="OrderItemId"></param>
        public OrderItemWindow(int OrderId, int OrderItemId) //!!!???? לא מצליחה לעדכן מוצר שקיים בהזמנה איך עושים את זה     
        {
            InitializeComponent();
            Order1 = bl.Order.GetByID(OrderId);
            BO.OrderItem OrderItem = Order1.Items.FirstOrDefault(item => item.ID == OrderItemId);
            Amount = OrderItem?.Amount ?? 0;
            productId = OrderItem?.ProductID ?? 0;
            AddNewItem.Visibility=Visibility.Hidden;

        }

        public void UpdateItem_Click(object sender, RoutedEventArgs e)// Update button
        {

            try
            {
                BO.Order order = new BO.Order();
                Order1= bl.Order.UpdateItems(Order1, productId, Amount ,true);
                Order1 = bl.Order.GetByID(Order1.ID);

            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            new OrderWindow(Order1.ID).Show();
            Close();


        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)//Add Button
        {
            try
            {
                Order1 = bl.Order.UpdateItems(Order1, productId, Amount, false);
                Order1 = bl.Order.GetByID(Order1.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            new OrderWindow(Order1.ID).Show();
            Close();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)//delete Button
        {
            try
            {
                bl.Order.UpdateItems(Order1, productId,0, true);
                Order1 = bl.Order.GetByID(Order1.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            new OrderWindow(Order1.ID).Show();
            Close();
        }
    }
}











































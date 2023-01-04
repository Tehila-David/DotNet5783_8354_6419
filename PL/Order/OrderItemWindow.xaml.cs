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
        int IdOfOrder = 0;
        int IdOfOrderItem = 0;
        BlApi.IBl bl = BlApi.Factory.Get()!;
        /// <summary>
        /// constructor for add new item to order
        /// </summary>
        /// <param name="OrderID"></param>
        public OrderItemWindow(int OrderID)
        {
            InitializeComponent();
            UpdateItem.Visibility = Visibility.Hidden;
            IdOfOrder = OrderID;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ProductID = int.Parse(txtProductID.Text);
                orderItem.Amount = int.Parse(txtAmount.Text);
                orderItem.ID = int.Parse(txtOrderItemID.Text);
                orderItem.Name = bl.Product.GetById(orderItem.ProductID).Name;
                orderItem.Price = bl.Product.GetById(orderItem.ProductID).Price;
                orderItem.TotalPrice = orderItem.Amount * orderItem.Price;
                bl.Order.GetByID(IdOfOrder).Items.Add(orderItem);
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            new OrderWindow(IdOfOrder).Show();
            Close();
        }
        /// <summary>
        /// constructor for update item from list of items
        /// </summary>
        /// <param name="OrderItemID"></param>
        /// <param name="flag"></param>
        public OrderItemWindow(int OrderID, BO.OrderItem orderItem)
        {
            InitializeComponent();
            AddItem.Visibility = Visibility.Hidden;
            IdOfOrder = OrderID;
            IdOfOrderItem = orderItem.ID;
            txtOrderItemID.Text = orderItem.ID.ToString();
            txtProductID.Text = orderItem.ProductID.ToString();
            txtAmount.Text = orderItem.Amount.ToString();

        }

        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                BO.OrderItem orderItem = new BO.OrderItem();

                orderItem.ProductID = int.Parse(txtProductID.Text);
                orderItem.Amount = int.Parse(txtAmount.Text);
                orderItem.ID = int.Parse(txtOrderItemID.Text);
                orderItem.Name = bl.Product.GetById(orderItem.ProductID).Name;
                orderItem.Price = bl.Product.GetById(orderItem.ProductID).Price;
                orderItem.TotalPrice = orderItem.Amount * orderItem.Price;


                var item = bl.Order.GetByID(IdOfOrder).Items.FirstOrDefault(item => item.ID == IdOfOrderItem);
                item = orderItem;
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            new OrderWindow(IdOfOrder).Show();
            Close();




        }
    }
}

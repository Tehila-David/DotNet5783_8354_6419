using BlApi;
using BlImplementation;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private IBl bl = new Bl();

        /// <summary>
        ///constructor of Add product
        /// </summary>
        public AddProductWindow()
        {
            InitializeComponent();
          
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Update.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// constructor of Update product
        /// </summary>
        /// <param name="product"></param>
        public AddProductWindow(int ID)
        {
            InitializeComponent();
            Add.Visibility = Visibility.Hidden;
            txtID.Visibility = Visibility.Hidden;
            BO.Product product=new BO.Product();
            product.ID = ID;
            product.Name = txtName.Text;
            product.InStock = int.Parse(txtInStock.Text);
            product.Price = int.Parse(txtPrice.Text);
            product.Category = (BO.Category)CategorySelector.SelectedItem;
            bl.Product.Update(product);

        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {

            BO.Product product = new BO.Product();
            product.ID = int.Parse(txtID.Text);
            product.Name = txtName.Text;
            product.InStock = int.Parse(txtInStock.Text);
            product.Price = int.Parse(txtPrice.Text);
            product.Category = (BO.Category)CategorySelector.SelectedItem;
            bl.Product.Add(product);
        }
    }
}
/*private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)//הכנסת רק מספרים לתיבת הטקסט
    {
        TextBox text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;
        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;
        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
        e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
        || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
            return;
        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
        //allow control system keys
        if (Char.IsControl(c)) return;
        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox
                        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other
        return;
    }*/
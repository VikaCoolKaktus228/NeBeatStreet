using NeBeatStreet.AppData;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NeBeatStreet.Pages;

namespace NeBeatStreet.Pages
{
    /// <summary>
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        int idusercart = Convert.ToInt32(App.Current.Properties["Id"].ToString());
        public Cart()
        {
            InitializeComponent();
            var orderobj = Entities4.GetContext().Order
                               .Where(x => x.IdUsers == idusercart)
                               .Select(x => x.IdOrder)
                               .ToList();

            var cartobj = Entities4.GetContext().CartTable
                               .Where(c => orderobj.Contains(c.OrderId))
                               .Select(x => x.ShoeId)
                               .ToList();
            var shoesincart = Entities4.GetContext().Shoes
                                         .Where(x => cartobj.Contains(x.IdShoes))
                                         .ToList();
            CartList.ItemsSource = shoesincart;

            if (CartList.Items.Count <= 0)
            {
                OrderButton.IsEnabled = false;
            }
            else { OrderButton.IsEnabled = true; }
        }
        private void removecart()
        {
            int userId = Convert.ToInt32(App.Current.Properties["Id"]);
            var order = Entities4.GetContext().Order.FirstOrDefault(o => o.IdUsers == userId && o.IdStatus == 2);

            var cartItems = Entities4.GetContext().CartTable.Where(c => c.OrderId == order.IdOrder).ToList();
            Entities4.GetContext().CartTable.RemoveRange(cartItems);
            Entities4.GetContext().SaveChanges();
            CartList.ItemsSource = new List<Cart>();

        }
        private void addmanagerorder()
        {
            try
            {
                var orderobj = Entities4.GetContext().Order
                .Where(x => x.IdUsers == idusercart)
                .Select(x => x.IdOrder)
                .ToList();

                var cartobj = Entities4.GetContext().CartTable
                                   .Where(c => orderobj.Contains(c.OrderId))
                                   .ToList();

                foreach (var item in cartobj)
                {
                    int idUsers = Convert.ToInt32(App.Current.Properties["Id"].ToString());
                    var order = Entities4.GetContext().Order.FirstOrDefault(o => o.IdUsers == idUsers);
                    var cartnew = new OrderManager()
                    {
                        IdOrder = order.IdOrder,
                        ShoesId = item.ShoeId
                    };
                    Entities4.GetContext().OrderManager.Add(cartnew);
                    Entities4.GetContext().SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {

            CartList.ItemsSource = Entities4.GetContext().CartTable.ToList(); 
            Button b = sender as Button;
            int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text); 
            Console.WriteLine(ID);
            AppConnect.shoesmodel.CartTable.Remove(AppConnect.shoesmodel.CartTable.Where(x => x.IdCart == ID).First());
            AppConnect.shoesmodel.SaveChanges();
            AppFrame.MainFraim.GoBack();
            AppFrame.MainFraim.Navigate(new Cart());
        }
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите сформировать заказ?", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    addmanagerorder();
                    removecart();
                    MessageBox.Show("Заказ успешно сформирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.MainFraim.Navigate(new UserList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new UserList());
        }
    }
}

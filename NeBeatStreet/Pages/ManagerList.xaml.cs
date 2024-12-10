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

namespace NeBeatStreet.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManagerList.xaml
    /// </summary>
    public partial class ManagerList : Page
    {
        public ManagerList()
        {
            InitializeComponent();
            List<Order> ordersman = AppConnect.shoesmodel.Order.ToList();
            OrderList.ItemsSource = ordersman;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Authorization());
        }

        private void AboutOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var moreorder = OrderList.SelectedItems.Cast<Order>().ToList();
            if (moreorder.Count > 0)
            {
                AppFrame.MainFraim.Navigate(new AboutOrderPage((sender as Button).DataContext as Order));
            }
            else
            {
                MessageBox.Show("выберите заказ!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

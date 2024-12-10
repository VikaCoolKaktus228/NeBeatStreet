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
    /// Логика взаимодействия для AboutOrderPage.xaml
    /// </summary>
    public partial class AboutOrderPage : Page
    {
        private Order currentorder = new Order();
        public AboutOrderPage(Order selectedorder)
        {
            InitializeComponent();
            var curentorderid = selectedorder.IdOrder;
            var curentorderuser = selectedorder.IdUsers;
            var curentorderstatus = selectedorder.IdStatus;
            var statusOrder = Entities4.GetContext().Status.FirstOrDefault(s => s.Order.Any(o => o.IdOrder == curentorderid));
            InitializeComponent();

            var moreorder = Entities4.GetContext().OrderManager
                               .Where(m => m.IdOrder == curentorderid)
                               .Select(m => m.ShoesId)
                               .ToList();
            var goodsInorder = Entities4.GetContext().Shoes
                                         .Where(x => moreorder.Contains(x.IdShoes))
                                         .ToList();
            AboutOrderList.ItemsSource = goodsInorder;

            if (selectedorder != null)
            {
                currentorder = selectedorder;
            }

            var userlogin = Entities4.GetContext().User
                               .FirstOrDefault(s => s.IdUser == curentorderuser);
            labeluser.Content = userlogin.Login;

            labelId.Content = selectedorder.IdOrder;

            var statusorder = Entities4.GetContext().Status
                               .FirstOrDefault(s => s.StatusId == curentorderstatus);

            orderstatuscombo.ItemsSource = Entities4.GetContext().Status.ToList();

            DataContext = currentorder;
        }

        private void changestatusbttn_Click(object sender, RoutedEventArgs e)
        {
            if (currentorder.IdOrder != 0)
            {
                try
                {
                    int Id = currentorder.IdOrder;
                    var StatusToUpdate = AppConnect.shoesmodel.OrderManager.FirstOrDefault(u => u.IdOrder == Id);
                    if (orderstatuscombo.SelectedItem != null)
                    {
                        StatusToUpdate.Order.IdStatus = Convert.ToInt32(orderstatuscombo.SelectedIndex + 1);

                        AppConnect.shoesmodel.SaveChanges();
                        MessageBox.Show("Статус заказа успешно изменен!",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("выберите статус!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при изменении статуса!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new ManagerList());
        }

        private void orderstatuscombo_DropDownOpened(object sender, EventArgs e)
        {
            changestatusbttn.IsEnabled = true;
        }
    }
}

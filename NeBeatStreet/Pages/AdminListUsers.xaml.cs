using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NeBeatStreet.AppData;
namespace NeBeatStreet.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminListUsers.xaml
    /// </summary>
    public partial class AdminListUsers : Page
    {
        public AdminListUsers()
        {
            InitializeComponent();
            List<User> uusers = AppConnect.shoesmodel.User.ToList();
            userlist.ItemsSource = uusers;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AdminList());
        }

        private void adduserbttn_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AddEditUsers(null));
        }

        private void deleteuser_Click(object sender, RoutedEventArgs e)
        {
            var usersfordeleting = userlist.SelectedItems.Cast<User>().ToList();
            if (usersfordeleting.Count > 0)
            {
                if (MessageBox.Show($"Вы точно хотите удалить следующие {usersfordeleting.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Entities2.GetContext().User.RemoveRange(usersfordeleting);
                        Entities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");

                        userlist.ItemsSource = Entities2.GetContext().User.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("выберите пользователя!");
            }
        }

        private void changeuser_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AddEditUsers((sender as Button).DataContext as User));
        }
    }
}

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
using NeBeatStreet.AppData;

namespace NeBeatStreet.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditUsers.xaml
    /// </summary>
    public partial class AddEditUsers : Page
    {
        private User thisuser = new User();
        public AddEditUsers(User selecteduser)
        {
            InitializeComponent();
            if (selecteduser != null)
            {
                thisuser = selecteduser;
            }
            ComboRole.Items.Clear();
            ComboRole.ItemsSource = AppConnect.shoesmodel.Role.ToList();
            DataContext = thisuser;
            FirstNameTb.MaxLength = 49;
            PhoneTb.MaxLength = 15;
            EmailTb.MaxLength = 50;
            LoginTb.MaxLength = 50;
            PasswordTb.MaxLength = 50;

        }
        private void AddUser()
        {
            try
            {
                thisuser = new User()
                {
                    FirstName = FirstNameTb.Text,
                    UserRole = ComboRole.SelectedIndex + 1,
                    Login = LoginTb.Text,
                    Email = EmailTb.Text,
                    Phone = PhoneTb.Text,
                    Password = PasswordTb.Text

                };
                AppConnect.shoesmodel.User.Add(thisuser);
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new AdminListUsers());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void UpdateUser()
        {
            try
            {
                if (thisuser.IdUser != null)
                {
                    thisuser.FirstName = FirstNameTb.Text;
                    thisuser.UserRole = ComboRole.SelectedIndex + 1;
                    thisuser.Login = LoginTb.Text;
                    thisuser.Email = EmailTb.Text;
                    thisuser.Phone = PhoneTb.Text;
                    thisuser.Password = PasswordTb.Text;
                }
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new AdminListUsers());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AdminListUsers());
        }

        private void AddEditButtonUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameTb.Text) || string.IsNullOrEmpty(EmailTb.Text) || string.IsNullOrEmpty(PhoneTb.Text)
                || string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PasswordTb.Text)
                || ComboRole.SelectedIndex < 0)
            {
                MessageBox.Show("Заполните все данные",
                       "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (EmailTb.Text.Contains("@") && EmailTb.Text.Contains("."))
            {
                if (PhoneTb.Text.Length < 10 || !PhoneTb.Text.Contains("+") || PhoneTb.Text.Length > 15)
                {
                    MessageBox.Show("Неверный формат телефона!",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Неверный формат почты",
                       "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (thisuser.IdUser == 0)
            {
                AddUser();
            }
            else
            {
                UpdateUser();
            }
        }
    }
}

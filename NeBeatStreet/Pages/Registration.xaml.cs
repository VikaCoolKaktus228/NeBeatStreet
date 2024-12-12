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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            EmailTb.MaxLength = 100;
            PhoneTb.MaxLength = 15;
            LoginTb.MaxLength = 50;
            PasswordTb.MaxLength = 20;
            RepeatPasswordTb.MaxLength = 20;
        }

        private void RegistrateButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Authorization());
        }

        private void RegistrateButton1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Authorization());
        }

        private void RegistrateButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(FirstNameTb.Text)|| string.IsNullOrEmpty(EmailTb.Text) || string.IsNullOrEmpty(PhoneTb.Text) 
                || string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PasswordTb.Password) 
                || string.IsNullOrEmpty(RepeatPasswordTb.Password))
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
            if (AppConnect.shoesmodel.User.Count(x => x.Login == LoginTb.Text) > 0)
            {
                MessageBox.Show("user already exist");
            }
            //Reg(FirstNameTb.Text, EmailTb.Text, PhoneTb.Text, LoginTb.Text, PasswordTb.Password, 1);

            try
            {
                User reguser = new User()
                {
                    FirstName = FirstNameTb.Text,
                    Email = EmailTb.Text,
                    Phone = PhoneTb.Text,
                    Login = LoginTb.Text,
                    Password = PasswordTb.Password,
                    UserRole = 1

                };
                AppConnect.shoesmodel.User.Add(reguser);
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new Authorization());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool Reg(string firstname, string email, string phone, string login, string password, int role)
        {
            try
            {
                User reguser = new User()
                {
                    FirstName = firstname,
                    Email = email,
                    Phone = phone,
                    Login = login,
                    Password = password,
                    UserRole = role

                };
                AppConnect.shoesmodel.User.Add(reguser);
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new Authorization());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                return true;
            
        }

        private void FirstNameTb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key >= Key.D1 && e.Key <= Key.D9)
            {
                e.Handled = true;
            }
        }

        private void PhoneTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) ||
             e.Key == Key.Back ||
             (e.Key == Key.Add && Keyboard.IsKeyDown(Key.LeftShift))))
            {
                e.Handled = true;
            }
        }

        private void RepeatPasswordTb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(RepeatPasswordTb.Password != PasswordTb.Password)
            {
                RegistrateButton.IsEnabled = false;
            }
            else
            {
                RegistrateButton.IsEnabled = true;
            }
        }

        private void PasswordTb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepeatPasswordTb.Password != PasswordTb.Password)
            {
                RegistrateButton.IsEnabled = false;
            }
            else
            {
                RegistrateButton.IsEnabled = true;
            }
        }
    }
}

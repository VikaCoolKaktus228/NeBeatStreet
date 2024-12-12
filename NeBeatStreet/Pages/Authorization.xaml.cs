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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Registration());
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            var userobj = AppConnect.shoesmodel.User.FirstOrDefault(x => x.Login == LoginTb.Text && x.Password == PasswordTb.Password);
            if (userobj != null)
            {
                if (userobj.Password != PasswordTb.Password)
                {
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                try
                {
                    switch (userobj.UserRole)
                    {
                        case 1:
                            App.Current.Properties["Id"] = userobj.IdUser;
                            MessageBox.Show("Здравствуйте, пользователь " + userobj.FirstName, "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFraim.Navigate(new UserList());
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, администратор " + userobj.FirstName, "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFraim.Navigate(new AdminList());
                            break;
                        case 3:
                            MessageBox.Show("Здравствуйте, менеджер " + userobj.FirstName, "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFraim.Navigate(new ManagerList());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Auth(LoginTb.Text, PasswordTb.Password);
        }
    }
    //    public bool Auth(string login, string password)
    //    {
    //        try
    //        {
    //            var userobj = AppConnect.shoesmodel.User.FirstOrDefault(x => x.Login == login && x.Password == password);
    //            if (userobj != null)
    //            {
    //                if (userobj.Password != password)
    //                {
    //                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    //                    return true;
    //                }
    //                try
    //                {
    //                    switch (userobj.UserRole)
    //                    {
    //                        case 1:
    //                            App.Current.Properties["Id"] = userobj.IdUser;
    //                            MessageBox.Show("Здравствуйте, пользователь " + userobj.FirstName, "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
    //                            AppFrame.MainFraim.Navigate(new UserList());
    //                            break;
    //                        case 2:
    //                            MessageBox.Show("Здравствуйте, администратор " + userobj.FirstName, "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
    //                            AppFrame.MainFraim.Navigate(new AdminList());
    //                            break;
    //                        case 3:
    //                            MessageBox.Show("Здравствуйте, менеджер " + userobj.FirstName, "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
    //                            AppFrame.MainFraim.Navigate(new ManagerList());
    //                            break;
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    //                }
    //            }
    //            else
    //            {
    //                MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    //                return true;
    //            }
    //            return true;
    //        }
    //        catch(Exception ex)
    //        {
    //            MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
    //        }
    //        return true;
    //    }
    //}
        
}

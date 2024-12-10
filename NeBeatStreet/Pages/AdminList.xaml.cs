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
    /// Логика взаимодействия для AdminList.xaml
    /// </summary>
    public partial class AdminList : Page
    {
        public AdminList()
        {
            InitializeComponent();
            List<Shoes> listshoes = AppConnect.shoesmodel.Shoes.ToList();
            ShoesList.ItemsSource = listshoes;
            ComboSort.Items.Add("По уменьшению цены");
            ComboSort.Items.Add("По возрастанию цены");
            ComboFilter.Items.Add("цена от 0 до 1000");
            ComboFilter.Items.Add("цена от 1000 до 5000");
            ComboFilter.Items.Add("цена от 5000");
        }
        Shoes[] FindShoes()
        {
            List<Shoes> oneshoes = AppConnect.shoesmodel.Shoes.ToList();
            var shoesall = oneshoes;
            if (SearchTb != null)
            {
                oneshoes = oneshoes.Where(x => x.ShoesName.ToLower().Contains(SearchTb.Text.ToLower())).ToList();
            }

            if (ComboFilter.SelectedIndex >= 0)
            {
                switch (ComboFilter.SelectedIndex)
                {

                    case 0:
                        oneshoes = oneshoes.Where(x => x.Price >= 0 && x.Price < 1000).ToList();
                        break;
                    case 1:
                        oneshoes = oneshoes.Where(x => x.Price >= 1000 && x.Price < 5000).ToList();
                        break;
                    case 2:
                        oneshoes = oneshoes.Where(x => x.Price > 5000).ToList();
                        break;
                }
            }
            if (ComboSort.SelectedIndex >= 0)
            {
                switch (ComboSort.SelectedIndex)
                {
                    case 0:
                        oneshoes = oneshoes.OrderByDescending(x => x.Price).ToList<Shoes>();
                        break;
                    case 1:
                        oneshoes = oneshoes.OrderBy(x => x.Price).ToList();
                        break;
                }
            }
            ShoesList.ItemsSource = oneshoes;
            return oneshoes.ToArray();

        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedshoe = ShoesList.SelectedItems.Cast<Shoes>().ToList();
            if (selectedshoe.Count > 0)
            {
                AppFrame.MainFraim.Navigate(new AddEditShoes((sender as Button).DataContext as Shoes));
            }
            else
            {
                MessageBox.Show("выберите товар!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var shoesfordeleting = ShoesList.SelectedItems.Cast<Shoes>().ToList();
            if (shoesfordeleting.Count > 0)
            {
                if (MessageBox.Show($"Вы точно хотите удалить следующие {shoesfordeleting.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Entities2.GetContext().Shoes.RemoveRange(shoesfordeleting);
                        Entities2.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");

                        ShoesList.ItemsSource = Entities2.GetContext().Shoes.ToList();
                        FindShoes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("выберите товар!");
            }
        }

        private void AddShoesButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AddEditShoes(null));

        }

        private void ToUsersButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AdminListUsers());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Authorization());
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindShoes();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindShoes();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindShoes();
        }
    }
}

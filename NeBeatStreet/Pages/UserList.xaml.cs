﻿using System;
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
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Page
    {
        public UserList()
        {
            InitializeComponent();
            List<Shoes> listshoes = AppConnect.shoesmodel.Shoes.ToList();
            ShoesList.ItemsSource = listshoes;
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

        private void ToCartButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Cart());
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Cart());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindShoes();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindShoes();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindShoes();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new Authorization());
        }
    }
}
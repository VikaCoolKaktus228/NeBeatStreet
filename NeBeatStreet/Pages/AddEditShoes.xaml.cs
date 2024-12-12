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
    /// Логика взаимодействия для AddEditShoes.xaml
    /// </summary>
    public partial class AddEditShoes : Page
    {
        private Shoes thisshoes = new Shoes();
        public AddEditShoes(Shoes selectedshoes)
        {
            InitializeComponent();
            if (selectedshoes != null)
            {
                thisshoes = selectedshoes;
            }
            PriceTb.MaxLength = 5;
            DescriptionTb.MaxLength = 100;
            NameShoesTb.MaxLength = 50;
            ArticleTb.MaxLength = 10;
            ComboType.Items.Clear();
            ComboType.ItemsSource = Entities5.GetContext().ShoesType.ToList();
            ComboMaterial.Items.Clear();
            ComboMaterial.ItemsSource = Entities5.GetContext().ShoesMaterial.ToList();
            ComboColor.Items.Clear();
            ComboColor.ItemsSource = Entities5.GetContext().ShoesColor.ToList();
            ComboFirm.Items.Clear();
            ComboFirm.ItemsSource = Entities5.GetContext().ShoesFirm.ToList();
            DataContext = thisshoes;

        }
        private void AddShoes()
        {
            try
            {
                thisshoes = new Shoes()
                {
                    ShoesName = NameShoesTb.Text,
                    Color = ComboColor.SelectedIndex + 1,
                    TypeOfShoes = ComboType.SelectedIndex + 1,
                    Firm = ComboFirm.SelectedIndex + 1,
                    Material = ComboMaterial.SelectedIndex + 1,
                    Price = Convert.ToInt32(PriceTb.Text),
                    Description = DescriptionTb.Text,
                    Article = ArticleTb.Text,
                    Photo = null
                };
                AppConnect.shoesmodel.Shoes.Add(thisshoes);
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Товар успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new AdminList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public bool Add(string nameshoes, int colcom, int typecom, int firmcom, int matcom, decimal pri, string desc, string art, string phot)
        {
            try
            {
                thisshoes = new Shoes()
                {
                    ShoesName = nameshoes,
                    Color = colcom,
                    TypeOfShoes = typecom,
                    Firm = firmcom,
                    Material = matcom,
                    Price = pri,
                    Description = desc,
                    Article = art,
                    Photo = phot
                };
                AppConnect.shoesmodel.Shoes.Add(thisshoes);
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Товар успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new AdminList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true;
        }
        private void UpdateShoes()
        {
            try
            {
                if (thisshoes.IdShoes != null)
                {
                    thisshoes.ShoesName = NameShoesTb.Text;
                    thisshoes.Color = ComboColor.SelectedIndex + 1;
                    thisshoes.TypeOfShoes = ComboType.SelectedIndex + 1;
                    thisshoes.Firm = ComboFirm.SelectedIndex + 1;
                    thisshoes.Material = ComboMaterial.SelectedIndex + 1;
                    thisshoes.Price = Convert.ToDecimal(PriceTb.Text);
                    thisshoes.Description = DescriptionTb.Text;
                    thisshoes.Article = ArticleTb.Text;
                    thisshoes.Photo = thisshoes.Photo;
                }
                AppConnect.shoesmodel.SaveChanges();
                MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFraim.Navigate(new AdminList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //public bool Edit(string nameshoes, int colcom, int typecom, int firmcom, int matcom, decimal pri, string desc, string art, string phot)
        //{
        //    try
        //    {
        //        if (thisshoes.IdShoes != null)
        //        {
        //            thisshoes.ShoesName = nameshoes;
        //            thisshoes.Color = colcom;
        //            thisshoes.TypeOfShoes = typecom;
        //            thisshoes.Firm = firmcom;
        //            thisshoes.Material = matcom;
        //            thisshoes.Price = pri;
        //            thisshoes.Description = desc;
        //            thisshoes.Article = art;
        //            thisshoes.Photo = phot;
        //        }
        //        AppConnect.shoesmodel.SaveChanges();
        //        MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        //        AppFrame.MainFraim.Navigate(new AdminList());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    return true;
        //}

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFraim.Navigate(new AdminList());
        }

        private void AddEditButton_Click_1(object sender, RoutedEventArgs e)
        {


            if (thisshoes.IdShoes == 0)
            {
                decimal.TryParse(PriceTb.Text, out decimal price);
                if (string.IsNullOrEmpty(NameShoesTb.Text) || string.IsNullOrEmpty(DescriptionTb.Text)
    || string.IsNullOrEmpty(ArticleTb.Text) || ComboColor.SelectedIndex < 0 || ComboFirm.SelectedIndex < 0 || ComboMaterial.SelectedIndex < 0
    || ComboType.SelectedIndex < 0 || string.IsNullOrEmpty(PriceTb.Text) || string.IsNullOrWhiteSpace(PriceTb.Text) || price == 0/*|| Convert.ToInt32(PriceTb.Text) <= 0*/)
                {

                    MessageBox.Show("Корректно заполните все данные!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                AddShoes();
            }

            else
            {
                decimal.TryParse(PriceTb.Text, out decimal price);
                if (string.IsNullOrEmpty(NameShoesTb.Text) || string.IsNullOrEmpty(DescriptionTb.Text)
    || string.IsNullOrEmpty(ArticleTb.Text) || ComboColor.SelectedIndex < 0 || ComboFirm.SelectedIndex < 0 || ComboMaterial.SelectedIndex < 0
    || ComboType.SelectedIndex < 0 || string.IsNullOrEmpty(PriceTb.Text) || string.IsNullOrWhiteSpace(PriceTb.Text) || price == 0/*|| Convert.ToInt32(PriceTb.Text) <= 0*/)
                {
                    
                    MessageBox.Show("Корректно заполните все данные!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                UpdateShoes();
            }
        }

        private void PriceTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }
    }
}

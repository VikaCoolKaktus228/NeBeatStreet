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
using NeBeatStreet.Pages;

namespace NeBeatStreet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.shoesmodel = new Entities5();
            AppFrame.MainFraim = mainframe;
            mainframe.Navigate(new Authorization());
        }

        private void N_Closed(object sender, EventArgs e)
        {
            var dbContext = Entities5.GetContext();

            try
            {
                if (dbContext.CartTable.Any())
                {
                    var allCartRecords = dbContext.CartTable.ToList();
                    dbContext.CartTable.RemoveRange(allCartRecords);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

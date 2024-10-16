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

namespace Хазиев_AutoService
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        

        public ServicePage()
        {
            InitializeComponent();

            var currentServices = Хазиев_АвтосервисEntities.GetContext().Service.ToList();

            ServiceListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;

            UpdateServices();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void UpdateServices()
        {

            var currentsServices = Хазиев_АвтосервисEntities.GetContext().Service.ToList();

            if (ComboType.SelectedIndex == 0)
            {
                currentsServices = currentsServices.Where(propa => (Convert.ToInt32(propa.Discount) >= 0 && Convert.ToInt32(propa.Discount) <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentsServices = currentsServices.Where(propa => (Convert.ToInt32(propa.Discount) >= 0 && Convert.ToInt32(propa.Discount) <= 5)).ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentsServices = currentsServices.Where(propa => (Convert.ToInt32(propa.Discount) >= 5 && Convert.ToInt32(propa.Discount) <= 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentsServices = currentsServices.Where(propa => (Convert.ToInt32(propa.Discount) >= 15 && Convert.ToInt32(propa.Discount) <= 30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentsServices = currentsServices.Where(propa => (Convert.ToInt32(propa.Discount) >= 30 && Convert.ToInt32(propa.Discount) <= 70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentsServices = currentsServices.Where(propa => (Convert.ToInt32(propa.Discount) >= 70 && Convert.ToInt32(propa.Discount) <= 100)).ToList();
            }

            currentsServices=currentsServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            ServiceListView.ItemsSource= currentsServices.ToList();

            if(RButtonDown.IsChecked.Value)
            {
                ServiceListView.ItemsSource= currentsServices.OrderByDescending(p => p.Cost).ToList();
            }
            if(RButtonUp.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentsServices.OrderBy(p => p.Cost).ToList();

            }
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class1.MainFrame.Navigate(new AddEditPage(null));
        }

       

       

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Class1.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Class1.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Service));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Хазиев_АвтосервисEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ServiceListView.ItemsSource = Хазиев_АвтосервисEntities.GetContext().Service.ToList();

            }
        }

        private void EditButton_Click_1(object sender, RoutedEventArgs e)
        {
            Class1.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Service));
        }
    }
}

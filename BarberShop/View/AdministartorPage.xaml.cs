using BarberShop.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BarberShop.View
{
    /// <summary>
    /// Логика взаимодействия для AdministartorPage.xaml
    /// </summary>
    public partial class AdministartorPage : Page
    {
        public ObservableCollection<Services> Services { get; set; }
        public Services SelectedService { get; set; }
        public AdministartorPage()
        {
            Services = new ObservableCollection<Services>();
            UpdateLists();
            DataContext = this;
            InitializeComponent();
        }
        void UpdateLists()
        {
            Services.Clear();
            foreach (var x in StaticInfo.services)
            {
                Services.Add(x);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Добавить
        {
            try
            {
                var checkExistingService = StaticInfo.services.FirstOrDefault(x => x.Title == TitleTextBox.Text);
                if (checkExistingService != null) { MessageBox.Show("Услуга с таким названием уже существует"); return; }
                if (TypeTextBox.Text == "") { MessageBox.Show("Вид услуги не выбран"); return; }
                Services newService = new Services();
                try { newService.Price = Convert.ToInt32(PriceTextBox.Text); }
                catch { MessageBox.Show("Цена не число!"); return; }
                newService.Title = TitleTextBox.Text;
                newService.Type = TypeTextBox.Text;
                newService.ImagePath = serviceIMGPath;
                newService.Description = DescriptionTextBox.Text;
                StaticInfo.services.Add(newService);
                StaticInfo.ServicesSerialize();
                MessageBox.Show("Услуга успешно добавлена");
                UpdateLists();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении услуги"); return;
            }
        }
        string serviceIMGPath = "";
        private void Button_Click_2(object sender, RoutedEventArgs e)//выбрать
        {
            try
            {   //открытие диалоговог окна
                OpenFileDialog openwnd = new OpenFileDialog
                {
                    Filter = "Image files(*.png)|*.png|Image files(*.jpg)|*.jpg"
                };
                openwnd.ShowDialog();
                ServiceImage.Source = new BitmapImage(new Uri(openwnd.FileName));
                serviceIMGPath = openwnd.FileName;
            }
            catch
            {
                ServiceImage.Source = new BitmapImage(new Uri("/Images/ScissorsBlack.png", UriKind.Relative));
                return;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//удалить
        {
            try
            {
                if (SelectedService == null) { return; }
                StaticInfo.services.Remove(SelectedService);
                StaticInfo.ServicesSerialize();
                MessageBox.Show("Услуга успешно удалена");
                UpdateLists();
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении услуги");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//выход
        {
            StaticInfo.mainWindow.MainFrame.Content = new ClientPage();
        }
    }
}

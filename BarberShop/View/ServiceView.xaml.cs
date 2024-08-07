using BarberShop.Model;
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

namespace BarberShop.View
{
    /// <summary>
    /// Логика взаимодействия для ServiceView.xaml
    /// </summary>
    public partial class ServiceView : UserControl
    {
        public Services service { get; set; }
        public bool IsInBasket { get; set; } = false;
        public ServiceView(Services service)
        {
            this.service = service;
            InitializeComponent();
            TitleBox.Text = service.Title;
            PriceLabel.Content = service.Price;
            TypeLabel.Content = service.Type;
            ServiceImage.Source = StaticInfo.TryLoadPicFromPath(service.ImagePath);
            UpdateBasketButton();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new DescriptionWindow(service);
            wnd.ShowDialog();
        }
        void UpdateBasketButton()
        {
            if(IsInBasket) {
                BasketButton.BorderBrush = Brushes.LightGreen;
                BasketButton.Foreground = Brushes.LightGreen;
                BasketButton.Content = "Из корзины";
                return;
            }
            if(!IsInBasket)
            {
                BasketButton.BorderBrush = Brushes.Yellow;
                BasketButton.Foreground = Brushes.Yellow;
                BasketButton.Content = "В корзину";
                return;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsInBasket)
            {
                IsInBasket = false;
                UpdateBasketButton();
                return;
            }
            if (!IsInBasket)
            {
                IsInBasket = true;
                UpdateBasketButton();
                return;
            }
        }
    }
}

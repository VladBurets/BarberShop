using BarberShop.Model;
using BarberShop.View;
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

namespace BarberShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            StaticInfo.mainWindow = this;
            InitializeComponent();
            LoadData();
            MainFrame.Content = new ClientPage();
        }

        void LoadData()
        {
            if (!StaticInfo.ServicesDeserialize() || StaticInfo.services == null)
            {
                StaticInfo.services = new List<Services>();
            }
        }
    }
}

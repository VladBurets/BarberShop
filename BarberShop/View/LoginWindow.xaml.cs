using BarberShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BarberShop.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                if (PasswordBox.Password == "barbershop" && LoginBox.Text == "barbershop")
                {
                    StaticInfo.mainWindow.MainFrame.Content = new AdministartorPage();
                    this.Close();
                }
                else { MessageBox.Show("Неверный логин или пароль"); }
            
        }
    }
}

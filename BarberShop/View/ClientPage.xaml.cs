using BarberShop.Model;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.MessageBox;
using Page = System.Windows.Controls.Page;

namespace BarberShop.View
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ObservableCollection<ServiceView> Services { get; set; }
        public List<ServiceView> staticList { get; set; }
        public List<ServiceView> helpListServices { get; set; }
        public ClientPage()
        {
            Services = new ObservableCollection<ServiceView>();
            helpListServices = new List<ServiceView>();
            staticList = new List<ServiceView>();
            DataContext = this;          
            UpdateList();
            InitializeComponent();
            TypeTextBox.SelectedItem = TypeTextBox.Items[0];
            Search();
        }
        void UpdateList()
        {
            foreach(var X in StaticInfo.services)
            {
                Services.Add(new ServiceView(X));
            }
            foreach(var x in Services)
            {
                staticList.Add(x);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var wnd = new LoginWindow();
            wnd.ShowDialog();
        }
        void Search()
        {
            int min = 0;
            int max = 0;
            try
            {
                if(MaxPriceTextBox.Text !="")
               max  = Convert.ToInt32(MaxPriceTextBox.Text);
                if (MinPriceTextBox.Text != "")
                    min = Convert.ToInt32(MinPriceTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Максимальная цена или минимальная не число");
            }
            Services.Clear();
            Regex reg = new Regex(SearchTextBox.Text);
            foreach (var x in staticList)
            {
                if (x.service.Price <= max || MaxPriceTextBox.Text == "")
                {
                    if (x.service.Price >= min || MinPriceTextBox.Text == "")
                    {
                        if (x.service.Type == (TypeTextBox.SelectedItem as Label).Content.ToString())
                        {
                            if (reg.IsMatch(x.service.ToString()))
                            {

                                Services.Add(x);
                                if(OnlyInBasket.IsChecked == true)
                                SelectBasketItems();
                            }
                        }
                    }
                }              
            }
            if (Services.Count == 0)
            {
                MessageBox.Show("Услуг по заданным критериям не найдено");
            }
        }
        void SelectBasketItems()
        {
            foreach(var x in Services)
            {
                helpListServices.Add(x);
            }
            Services.Clear();
            foreach(var x in helpListServices)
            {
                if (x.IsInBasket == true)
                {
                    Services.Add(x);
                }

            }
            helpListServices.Clear();
        }
        string GetExportString()
        {
            string result = "";
            foreach(var x in staticList)
            {
                if (x.IsInBasket)
                {
                    result += x.service.ToExportString();
                }
            }
            return result;
        }
        bool IsHaveItemsInBasket()
        {
            foreach(var x in staticList)
            {
                if (x.IsInBasket)
                {
                    return true;
                }
            }
            return false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)//поиск
        {
            Search();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//пдф
        {
            if (!IsHaveItemsInBasket())
            {
                MessageBox.Show("В корзине нет элементов для экспорта");
                return;
            }
            try
            {
                FolderBrowserDialog openwnd = new FolderBrowserDialog();
                openwnd.ShowDialog();
                ApplicationClass app = new ApplicationClass();
                app.Documents.Add();
                app.ActiveDocument.Select(); 
                app.Selection.FormattedText.Text = GetExportString() + "\nОбщая стоимость услуг: " + GetBasketSumm();
                app.ActiveDocument.SaveAs2(openwnd.SelectedPath + "\\ExportBasket1" + DateTime.Now.ToShortDateString() + ".docx");
                app.ActiveDocument.Close();
                var doc = new Aspose.Words.Document(openwnd.SelectedPath + "\\ExportBasket1" + DateTime.Now.ToShortDateString() + ".docx");
                File.Delete(openwnd.SelectedPath + "\\ExportBasket1" + DateTime.Now.ToShortDateString() + ".docx");
                doc.Save(openwnd.SelectedPath + "\\ExportBasket1" + DateTime.Now.ToShortDateString() + ".pdf");
                MessageBox.Show("Данные экспортированы успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка экспорта");
                return;
            }
        }
        string GetBasketSumm()
        {
            int result = 0;
            foreach(var x in staticList)
            {
                if(x.IsInBasket)
                {
                    result += x.service.Price;
                }
            }
            return Convert.ToString(result);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//ворд
        {
            if (!IsHaveItemsInBasket())
            {
                MessageBox.Show("В корзине нет элементов для экспорта");
                return;
            }
            try
            {
                FolderBrowserDialog openwnd = new FolderBrowserDialog();
                openwnd.ShowDialog();
                ApplicationClass app = new ApplicationClass();
                app.Documents.Add(); 
                app.ActiveDocument.Select(); 
                app.Selection.FormattedText.Text = GetExportString() + "\nОбщая стоимость услуг: " + GetBasketSumm();
                app.ActiveDocument.SaveAs2(openwnd.SelectedPath + "\\ExportBasket" + DateTime.Now.ToShortDateString() + ".docx");
                app.ActiveDocument.Close();
                MessageBox.Show("Данные экспортированы успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка экспорта");
                return;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//текст
        {
            if (!IsHaveItemsInBasket())
            {
                MessageBox.Show("В корзине нет элементов для экспорта");
                return;
            }
            try
            {
                FolderBrowserDialog openwnd = new FolderBrowserDialog();
                openwnd.ShowDialog();
                using (StreamWriter writer = new StreamWriter(openwnd.SelectedPath + "\\ExportBasket" + DateTime.Now.ToShortDateString() + ".txt", false))
                {
                    writer.WriteLineAsync(GetExportString() + "\nОбщая стоимость услуг: " + GetBasketSumm());
                }
                MessageBox.Show("Данные экспортированы успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка экспорта");
                return;
            }
        }
    }
}

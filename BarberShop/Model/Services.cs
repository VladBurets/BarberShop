using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Model
{
    [Serializable]
    public class Services
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
        {
            return Title + " " + Type + " " + Description + " "+ Price;
        }
        public string ToExportString()
        {
            return $"Название услуги: {Title}\nВид услуги: {Type}\nОписание услуги: {Description}\nЦена услуги(руб): {Price}\n\n\n";
        }
    }
}

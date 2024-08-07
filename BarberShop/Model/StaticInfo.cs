using BarberShop.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace BarberShop.Model
{
    public static class StaticInfo
    {
        public static MainWindow mainWindow { get; set; }
        public static List<Services> services { get; set; }
        public static string servicesDataPath { get; set; } = "Data\\ServicesData.xml";
        public static bool ServicesSerialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Services>));
                using (FileStream fs = new FileStream(servicesDataPath, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, services);
                }
                return true;
            }
            catch { return false; }
        }
        public static bool ServicesDeserialize()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Services>));
                using (FileStream fs = new FileStream(servicesDataPath, FileMode.OpenOrCreate))
                {
                    services = xmlSerializer.Deserialize(fs) as List<Services>;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static BitmapImage TryLoadPicFromPath(string path)
        {
            try
            {
                return new BitmapImage(new Uri(path));
            }
            catch { return new BitmapImage(new Uri("/Images/ScissorsBlack.png", UriKind.Relative)); }
        }

    }
}

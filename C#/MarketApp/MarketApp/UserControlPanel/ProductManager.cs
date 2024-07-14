using MarketLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserPanel.UserControlPanel
{
    internal class ProductManager
    {
        public static List<Product> Products { get; set; }

        static ProductManager()
        {
            string fileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Ismay_ne14\\Desktop\\MarketApp\\AdminPanel\\bin\\Debug\\net8.0");
            string filePath = Path.Combine(fileDirectory, "products.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var listOfProducts = JsonSerializer.Deserialize<List<Product>>(json);
                if (listOfProducts is not null) Products = listOfProducts;
            }
            Products ??= new List<Product>();
        }

         
        }
}

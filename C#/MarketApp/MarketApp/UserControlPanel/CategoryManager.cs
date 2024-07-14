using MarketLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserPanel.UserControlPanel
{
    internal class CategoryManager
    {


        public static List<Category> Categories { get; set; }

        static CategoryManager()
        {


            string fileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Ismay_ne14\\Desktop\\MarketApp\\AdminPanel\\bin\\Debug\\net8.0");
            string filePath = Path.Combine(fileDirectory, "categories.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var listOfCategories = JsonSerializer.Deserialize<List<Category>>(json);
                if (listOfCategories is not null) Categories = listOfCategories;
            }
            Categories ??= new List<Category>();


        }
    }
}

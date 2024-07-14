using MarketLibrary;
using System.Text.Json;
using System.Xml;
namespace AdminPanel.StockControlPanel
{
    internal class CategoryManager
    {
        public static List<Category> Categories { get; set; }

        static CategoryManager()
        {
            if (File.Exists("categories.json"))
            {
                var json = File.ReadAllText("categories.json");
                var listOfCategories = JsonSerializer.Deserialize<List<Category>>(json);
                if (listOfCategories is not null) Categories = listOfCategories;
            }
            Categories ??= new List<Category>();
        }

        public void SaveCategories(List<Category> categories)
        {
 
            var json=JsonSerializer.Serialize(categories);
            File.WriteAllText("categories.json", json);
        }
        public static void AddCategory(string categoryName)
        {
            var category = new Category
            {
                CategoryID=Guid.NewGuid().ToString(),
                Name = categoryName
            };
            Categories.Add(category);
            var json = JsonSerializer.Serialize(Categories);
            File.WriteAllText("categories.json", json);
        }

    }
}

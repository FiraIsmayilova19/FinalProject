using MarketLibrary;
using System.Text.Json;


namespace AdminPanel.StockControlPanel
{
    internal class ProductManager
    {
        public static List<Product> Products { get; set; }

        static ProductManager()
        {
            if (File.Exists("products.json"))
            {
                var json = File.ReadAllText("products.json");
                var listOfProducts = JsonSerializer.Deserialize<List<Product>>(json);
                if (listOfProducts is not null) Products = listOfProducts;
            }
            Products ??= new List<Product>();
        }
        public void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText("products.json", json);
        }
        public static void AddProduct(string name, string description,string categoryId, double price, int quantity)

        {
            var product = new Product
            {
               ProductID = categoryId,
                Name = name,
                Description = description,
                Price = price,
                Quantity = quantity
            };
            Products.Add(product);
           var json = JsonSerializer.Serialize(Products);
            File.WriteAllText("products.json", json);


           
            
        }

        public static void UpdateProduct(Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.Name == updatedProduct.Name);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
               
                File.WriteAllText("products.json", JsonSerializer.Serialize(Products));
            }
         
         

        }
        public static void UpdateProductName(string oldName, string updatedProductName)
        {
            var product = Products.FirstOrDefault(p => p.Name == oldName);
            if (product != null)
            {
                product.Name = updatedProductName;

                File.WriteAllText("products.json", JsonSerializer.Serialize(Products));
            }
        }
        public  static void UpdateProductPrice(string updatedProductName, double updatedProductPrice)
        {
            var product = Products.FirstOrDefault(p => p.Name == updatedProductName);
            if (product != null)
            {
                product.Price = updatedProductPrice;

                File.WriteAllText("products.json", JsonSerializer.Serialize(Products));
            }
        }
        public static void UpdateProductDescription(string updatedProductName, string updatedProductDescription)
        {
            var product = Products.FirstOrDefault(p => p.Name == updatedProductName);
            if (product != null)
            {
                product.Description = updatedProductDescription;

                File.WriteAllText("products.json", JsonSerializer.Serialize(Products));
            }
        }
        public static void UpdateProductQuantity(string updatedProductName, int updatedProductQuantity)
        {
            var product = Products.FirstOrDefault(p => p.Name == updatedProductName);
            if (product != null)
            {
                product.Quantity = updatedProductQuantity   ;

                File.WriteAllText("products.json", JsonSerializer.Serialize(Products));
            }
        }

    }

    }

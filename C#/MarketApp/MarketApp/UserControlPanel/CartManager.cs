using MarketLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserPanel.UserControlPanel
{
    public static class CartManager
    {

        public static List<Cart> Products { get; set; } = new List<Cart>();

        static CartManager()
        {
            string fileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Ismay_ne14\\Desktop\\MarketApp\\AdminPanel\\bin\\Debug\\net8.0");
            string filePath = Path.Combine(fileDirectory, "cart.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var listOfProduct = JsonSerializer.Deserialize<List<Cart>>(json);
                if (listOfProduct is not null) Products = listOfProduct;
            }
            Products ??= new List<Cart>();

        }

        public static void AddtoCart(Product product, int quantity)
        {
            var cart = Products.FirstOrDefault(c => c.Product.ProductID == product.ProductID);
            if (cart != null)
            {
                cart.Quantity += quantity;
            }
            else
            {
                Products.Add(new Cart { Product = product, Quantity = quantity });
            }
            var json = JsonSerializer.Serialize(Products);
            File.WriteAllText("cart.json", json);

        }

        public static void RemoveProduct(Product product)
        {
            var cartItem = Products.FirstOrDefault(ci => ci.Product.ProductID == product.ProductID);
            if (cartItem != null)
            {
                Products.Remove(cartItem);

                var json = JsonSerializer.Serialize(Products);
                File.WriteAllText("cart.json", json);
            }
        }

        public static double Checkout(double payAmount)
        {
            double total = Products.Sum(ci => ci.Product.Price * ci.Quantity);
            if (payAmount >= total)
            {
                Products.Clear();
                var json = JsonSerializer.Serialize(Products);
                File.WriteAllText("cart.json", json);
            }
            return payAmount - total;

        }
    }

}


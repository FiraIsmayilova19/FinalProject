using AdminPanel.StockControlPanel;
using MarketLibrary;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
  Login:
        string adminUsername = "AdminFira";
        string adminPassword = "Admin1234";

        
        Console.Write("İstifadechi adi: ");
        string username = Console.ReadLine();

        Console.Write("Parol: ");
  string password = Console.ReadLine();


        if (username == adminUsername && password == adminPassword)
        {
            Console.WriteLine("Ugurlu girish!");
          
        }
        else
        {
            Console.WriteLine("Yanlish istifadechi adi ve ya parol.");
            goto Login;
        }
        AdminControlPanel();
    }
    public static void AdminControlPanel()
    {
        while (true)
        {
            
            Console.WriteLine("1.Add category\n2.Add product\n3.Update product details\n4.View products\n5.View Reports\n6.Exit.");
            Console.WriteLine("Choose:");
            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    AddProduct();
                    break;
                case "3":
                    UpdateProductDetails();
                   
                    break;
                case "4":
                    ViewProducts();
                    break;
                case "5":
                    ViewReports();
                    break;
                case "6":

                    return;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }

        }
        static void AddCategory()
        {
            Console.WriteLine("Enter category name: ");
            var name = Console.ReadLine();
            CategoryManager.AddCategory(name!);
        }
        static void AddProduct()
        {

            Console.WriteLine("Enter product name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter product description: ");
            var description = Console.ReadLine();
            Console.WriteLine("Enter product price: ");
            var price = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter product quantity: ");
            var quantity = int.Parse(Console.ReadLine());
            var Category = new List<Category>();
            if (File.Exists("categories.json"))
            {
                var json = File.ReadAllText("categories.json");
                var ListCategories = JsonSerializer.Deserialize<List<Category>>(json);
                if (ListCategories is not null) Category = ListCategories;
            }
            Console.WriteLine("Enter product category: ");
            var category = Console.ReadLine();
            var SelectCategory = Category.FirstOrDefault(c => c.Name == category);
            var categoryId = SelectCategory.CategoryID;
            ProductManager.AddProduct(name!, description!,categoryId!, price!, quantity!);
        }
        static void UpdateProductDetails()
        {
            Console.WriteLine("Enter product name: ");
            var name = Console.ReadLine();
            var products=ProductManager.Products;
            var SelectProduct=products.FirstOrDefault(p=>p.Name==name);
            Console.WriteLine("Product which would be changed: ", SelectProduct.Name,SelectProduct.Description, SelectProduct.Price, SelectProduct.Quantity);
            Console.WriteLine("select what you want to change\n1.Name\n2.Description\n3.Price\n4.Quantity");

            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Console.WriteLine("Enter new name: ");
                    var newName = Console.ReadLine();
                    ProductManager.UpdateProductName(SelectProduct.Name, newName!);
                    break;
                case "2":
                    Console.WriteLine("Enter new description: ");
                    var newDescription = Console.ReadLine();
                    ProductManager.UpdateProductDescription(SelectProduct.Name,newDescription!);
                    break;
                case "3":
                    Console.WriteLine("Enter new price: ");
                    var newPrice = double.Parse(Console.ReadLine());
                    ProductManager.UpdateProductPrice(SelectProduct.Name, newPrice!);
                    break;
                case "4":
                    Console.WriteLine("Enter new quantity: ");
                    var newQuantity = int.Parse(Console.ReadLine());
                    ProductManager.UpdateProductQuantity(SelectProduct.Name, newQuantity!);
                    break;
            }
}
        static void ViewProducts()
        {
            var products = ProductManager.Products;
            foreach (var product in products)
            {
                Console.WriteLine($"Product:  \nProductID: {product.ProductID}\nName: {product.Name}\nDescription={product.Description}\nPrice: {product.Price}\nQuantity: {product.Quantity}");
            }
        }
        static void ViewReports()
        {
        }
    }
}


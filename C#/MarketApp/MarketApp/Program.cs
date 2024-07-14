using UserPanel.UserControlPanel;
    using MarketLibrary;
using System.Xml.Linq;
using UserPanel.Services;
using MarketLibrary;
public class Program { 
public static void RegisterPage()
{
Register:
    Console.Clear();
    Console.WriteLine("Sign Up");
    Console.Write("Name: ");
    var name = Console.ReadLine();
    Console.Write("Surname: ");
    var surname = Console.ReadLine();
    Console.Write("Date of birth (dd.MM.yyyy): ");
    var date = Console.ReadLine();
    Console.Write("Email: ");
    var email = Console.ReadLine(); 
    Console.Write("Password: ");
    var password = Console.ReadLine();

    

    try
    {
        UserManager.Register(name!, surname!, email!.ToLower().Trim(), password!, date!);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Thread.Sleep(2000);
        goto Register;
    }
        
}

public static void LoginPage()
{
Login:
    Console.Clear();
    Console.WriteLine("Sign In");
    Console.Write("Email: ");
    var email = Console.ReadLine();
    Console.Write("Password: ");
    var password = Console.ReadLine();
    try
    {
        UserManager.Login(email!, password!);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Thread.Sleep(2000);
        goto Login;
    }
}

public static void MainPage()
{
    Console.Clear();
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
}

private static void Main(string[] args)
{


        while (true)
        {
            if (UserManager.User is null)
            {
                MainPage();
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": RegisterPage(); break;
                    case "2": LoginPage(); break;
                }
            }
            if (UserManager.User is not null)
            {
                Console.WriteLine($"Welcome, {UserManager.User.Name}");

                break;
            }
        }
        UserControlPanel();
        
    }
    public static void UserControlPanel()
    {
        while (true)
        {
            Console.WriteLine("1. Categories");
            Console.WriteLine("2. Show Cart");
            Console.WriteLine("3. Profil");
            Console.WriteLine("4. Exit");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowCategoies();
                    break;
                case "2":
                    ViewCart();
                    break;
                case "3":
                    ShowProfil();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
            static void ShowCategoies()
            {
                Console.WriteLine("Categories:");

                var categories = CategoryManager.Categories;

                foreach (var item in categories)
                {
                    Console.WriteLine($"{item.Name}");

                }
                Console.WriteLine("Enter Category Name to see products:  ");
                var choice = Console.ReadLine();
                var category = categories.FirstOrDefault(c => c.Name == choice);
                if (category is not null)
                {
                    Console.WriteLine("Products: ");
                    var products = ProductManager.Products;
                    var categoryProduct= products.Where(p => p.ProductID == category.CategoryID);
                    foreach (var product in categoryProduct)
                    {
                        if (product.Quantity != 0)
                        {
                            Console.WriteLine($"Product:\n  ProductID: {product.ProductID}\n Name: {product.Name}\n Description: {product.Description}\nPrice: {product.Price}\n Quantity: {product.Quantity}");
                        }

                    }
                    Console.WriteLine("Enter Product Name to add to cart:  ");
                    var choice2 = Console.ReadLine();
                    Console.WriteLine("Enter Quantity:  ");
                    var choice3 = Console.ReadLine();
                    var CartProduct = products.FirstOrDefault(c => c.Name == choice2);
                   
                    if (CartProduct is not null)
                    {
                        CartManager.AddtoCart(CartProduct, int.Parse(choice3));
                        
                    }
                }
             
            
                else
                {
                    Console.WriteLine("Wrong input");
                }
            }
            static void ViewCart()
            {
               Console.WriteLine("Cart: ");
                var cartProducts = CartManager.Products;
                foreach (var cartItem in cartProducts)
                {
                    Console.WriteLine($"Product: {cartItem.Product.Name}");
                    Console.WriteLine($"Quantity: {cartItem.Quantity}");
                }
                double total = cartProducts.Sum(ci => ci.Product.Price * ci.Quantity);
                Console.WriteLine($"Total: {total}");
                Console.WriteLine("1. Remove an item");
                Console.WriteLine("2. Checkout");
                Console.WriteLine("3. Return Menu");
                var choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter product name to remove: ");
                        var productName = Console.ReadLine()!;
                        var products = ProductManager.Products;
                        var product = products.FirstOrDefault(p => p.Name == productName);
                        if (product is not null)
                        {
                            CartManager.RemoveProduct(product);
                        }
                        break;
                    case "2":
                        Console.Write("Enter payment amount: ");
                        var payment = double.Parse(Console.ReadLine()!);
                        double change = CartManager.Checkout(payment);
                        Console.WriteLine("Change: " + change);
                      
                        break;
                        case "3":
                        UserControlPanel();
                        break;
                }   
                
            }
            static void ShowProfil()
            {
                Console.WriteLine($"Name: {UserManager.User.Name}");
                Console.WriteLine($"Surname: {UserManager.User.Surname}");
                Console.WriteLine($"Email: {UserManager.User.Email}");
                Console.WriteLine("1. Edit profile");
                Console.WriteLine("2. Change password");
                Console.WriteLine("3. Logout");

                var choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter new name: ");
                        var name = Console.ReadLine();
                        Console.Write("Enter new surname: ");
                        var surname = Console.ReadLine();
                        Console.Write("Enter new date of birth (dd.MM.yyyy): ");
                        var date = Console.ReadLine();
                        UserManager.UpdateProfile(name!, surname!, DateOnly.ParseExact(date!, "dd.MM.yyyy"));
                        break;
                    case "2":
                        Console.Write("Enter new password: ");
                        var newPassword = Console.ReadLine();
                        UserManager.ChangePassword(newPassword!);
                        break;
                    case "3":
                        UserManager.Logout();
                        
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            }
        }
    }
}
    
 

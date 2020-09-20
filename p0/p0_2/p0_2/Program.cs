using p0_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace p0_2
{
  class Program
  {
    public static Customer LoggedInCustomer = new Customer();
    static void Main(string[] args)
    {
      using (var context = new DatabaseContext())
      {

        //Seed the Stores Table
        //SeedStores(context);
        //Console.ReadLine();

        StartMenu();
        int startMenuChoice;

        // Sign Up and login loop
        do
        {
          startMenuChoice = StartMenuInput();
          // Sign up and login UI inputs
          if (startMenuChoice == 1) //Login
          {
            Login(context);
          }
          else if (startMenuChoice == 2) //SignUp
          {
            Console.WriteLine("Signing up");
            Customer newCustomer = SignUp();
            AddCustomerToDB(newCustomer, context);
          }
          else if (startMenuChoice == 3) Environment.Exit(0);


          int loggedInMenuInput;
          int locationMenuChoice;
          do
          {
            // Logged in display UI inputs
            loggedInMenuInput = LoggedInMenuInput(context);
            if (loggedInMenuInput == 1)
            {
              // Locations display UI inputs
              locationMenuChoice = LocationsMenuInput(context);

              // Products of location UI inputs
              int productsMenuChoice = ProductsMenuInput(context, locationMenuChoice);
            }
            else if (loggedInMenuInput == 2)
            {
              GetCart(context);
            }
            else if (loggedInMenuInput == 3)
            {
              GetCustomers(context);
            }
            else if (loggedInMenuInput == 4)
            {
              PlaceOrder(context);
            }
            else if (loggedInMenuInput == 5)
            {
              OrdersMenuInput(context);
            }
            else if (loggedInMenuInput == 6)
            {
              LoggedInCustomer = new Customer();
            }

          } while (LoggedInCustomer.UserName != null || loggedInMenuInput > 6);

        } while (LoggedInCustomer.UserName == null);

      }
    }

    static void StartMenu()
    {
      Console.WriteLine("Welcome to Bookopolis!\nWe have the greatest selection of books at the best prices");
    }

    static Customer SignUp()
    {
      string firstname;
      string lastname;
      string username;
      string password;
      Customer c = new Customer();

      do
      {
        Console.WriteLine("Enter your first name");
        firstname = Console.ReadLine();
        firstname = firstname.Trim();
      } while (firstname.Length == 0);

      do
      {
        Console.WriteLine("Enter your last name");
        lastname = Console.ReadLine();
        lastname = lastname.Trim();
      } while (lastname.Length == 0);

      do
      {
        Console.WriteLine("Enter your username");
        username = Console.ReadLine();
        username = username.Trim();
      } while (username.Length == 0);

      do
      {
        Console.WriteLine("Enter your password");
        password = Console.ReadLine();
        password = password.Trim();
      } while (password.Length == 0);

      c.FirstName = firstname;
      c.LastName = lastname;
      c.UserName = username;
      c.Password = password;

      return c;
    }

    static void AddCustomerToDB(Customer newCustomer, DatabaseContext context)
    {
      if (context.Customers.ToList().Count > 0)
      {
        foreach (var db_customers in context.Customers.ToList())
        {
          if (db_customers.UserName == newCustomer.UserName)
          {
            Console.WriteLine($"The username {newCustomer.UserName} is already taken.");
            string username;
            do
            {
              Console.WriteLine("Enter a new username");
              username = Console.ReadLine();
              username = username.Trim();
            } while (username.Length == 0 || username == newCustomer.UserName);
            newCustomer.UserName = username;
            ShoppingCart newShoppingCart = new ShoppingCart();
            newCustomer.ShoppingCart = newShoppingCart;
            context.Customers.Add(newCustomer);
            context.SaveChanges();
          }
          else
          {
            ShoppingCart newShoppingCart = new ShoppingCart();
            newCustomer.ShoppingCart = newShoppingCart;
            context.Customers.Add(newCustomer);
            context.SaveChanges();
            var c = context.Customers.Where(c => c.UserName == newCustomer.UserName).FirstOrDefault();
            LoggedInCustomer = c;
            break;
          }
        }
      }
      else
      {
        ShoppingCart newShoppingCart = new ShoppingCart();
        newCustomer.ShoppingCart = newShoppingCart;
        context.Customers.Add(newCustomer);
        context.SaveChanges();
        var c = context.Customers.Where(c => c.UserName == newCustomer.UserName).FirstOrDefault();
        LoggedInCustomer = c;
      }
    }

    static void Login(DatabaseContext context)
    {
      string username;
      string password;

      do
      {
        Console.WriteLine("Enter your username");
        username = Console.ReadLine();
        username = username.Trim();
      } while (username.Length == 0);

      do
      {
        Console.WriteLine("Enter your password");
        password = Console.ReadLine();
        password = password.Trim();
      } while (password.Length == 0);

      var customer = context.Customers
        .FirstOrDefault(c => c.UserName == username
                     && c.Password == password);

      if (customer != null)
      {
        LoggedInCustomer = customer;
        Console.WriteLine("You have successfully logged in");
      }
      else
      {
        Console.WriteLine("Incorrect username or password");
      }
    }

    static void SeedCustomers(DatabaseContext context)
    {
      Customer customer = new Customer()
      {
        FirstName = "a",
        LastName = "a",
        UserName = "a",
        Password = "a",
      };
      context.Customers.Add(customer);
      context.SaveChanges();
    }

    static void SeedStores(DatabaseContext context)
    {
      List<Store> stores = new List<Store>()
      {
        new Store() {
          StreetAddress = "1701 W 133rd St",
          City = "Kansas City",
          State = "Missouri",
          ZIP = "64145"
        },
          new Store() {
          StreetAddress = "6185 Retail Rd",
          City = "Dallas",
          State = "Texas",
          ZIP = "75231"
        },
          new Store() {
          StreetAddress = "1400 Hilltop Mall Rd",
          City = "Richmond",
          State = "California",
          ZIP = "94806"
        },
          new Store() {
          StreetAddress = "3201 E Platte Ave",
          City = "Colorado Springs",
          State = "Colorado",
          ZIP = "80909"
        },
      };

      List<Product> store1Products = new List<Product>()
      {
        new Product()
        {
          Title = "Catch-22",
          Author = "Joseph Heller",
          Description = "Catch-22 is a satirical war novel by American author Joseph Heller.",
          Price = 12.50
        },
        new Product()
        {
          Title = "The Grapes of Wrath",
          Author = "John Steinbeck",
          Description = "The Grapes of Wrath is an American realist novel written by John Steinbeck.",
          Price = 12.50
        },
        new Product()
        {
          Title = "Midnight's Children",
          Author = "Salman Rushdie",
          Description = "Midnight's Children is a 1981 novel by author Salman Rushdie",
          Price = 12.50
        },
        new Product()
        {
          Title = "Ulysses",
          Author = "James Joyce",
          Description = "Ulysses is a modernist novel by Irish writer James Joyce.",
          Price = 15.50
        },
      };

      List<Product> store2Products = new List<Product>()
      {
        new Product()
        {
          Title = "The Sound and the Fury",
          Author = "William Faulkner",
          Description = "The Sound and the Fury is a novel by the American author William Faulkner.",
          Price = 10.50
        },
        new Product()
        {
          Title = "On the Road",
          Author = "Jack Kerouac",
          Description = "On the Road is a 1957 novel by American writer Jack Kerouac, based on the travels of Kerouac and his friends across the United States",
          Price = 11.50
        },
        new Product()
        {
          Title = "The Sun Also Rises",
          Author = "Ernest Hemingway",
          Description = "The Sun Also Rises is a 1926 novel by American writer Ernest Hemingway that portrays American and British expatriates who travel from Paris to the Festival of San Fermín in Pamplona to watch the running of the bulls and the bullfights.",
          Price = 16.50
        },
        new Product()
        {
          Title = "I, Claudius",
          Author = "Robert Graves",
          Description = "I, Claudius is a historical novel by English writer Robert Graves, published in 1934.",
          Price = 10.50
        },
      };

      stores[0].Products = store1Products;
      stores[0].Inventory = store1Products.Count;
      stores[1].Products = store2Products;
      stores[1].Inventory = store2Products.Count;

      foreach (var store in stores)
      {
        context.Stores.Add(store);
      }
      context.SaveChanges();
    }

    //static void SeedProducts(DatabaseContext context)
    //{
    //  List<Product> store1Products = new List<Product>()
    //  {
    //    new Product()
    //    {
    //      Title = "Catch-22",
    //      Author = "Joseph Heller",
    //      Description = "Catch-22 is a satirical war novel by American author Joseph Heller.",
    //      Price = 12.50
    //    },
    //    new Product()
    //    {
    //      Title = "The Grapes of Wrath",
    //      Author = "John Steinbeck",
    //      Description = "The Grapes of Wrath is an American realist novel written by John Steinbeck.",
    //      Price = 12.50
    //    },
    //    new Product()
    //    {
    //      Title = "Midnight's Children",
    //      Author = "Salman Rushdie",
    //      Description = "Midnight's Children is a 1981 novel by author Salman Rushdie",
    //      Price = 12.50
    //    },
    //    new Product()
    //    {
    //      Title = "Ulysses",
    //      Author = "James Joyce",
    //      Description = "Ulysses is a modernist novel by Irish writer James Joyce.",
    //      Price = 15.50
    //    },

    //  };

    //  foreach (var product in store1Products)
    //  {
    //    context.Products.Add(product);
    //  }
    //  context.SaveChanges();
    //}

    static int LoggedInMenuInput(DatabaseContext context)
    {
      int choice;
      bool inputInt;
      do
      {
        Console.WriteLine($"\nWelcome {LoggedInCustomer.UserName}, \n1) View Locations \n2) View Shopping Cart \n3) View Customers \n4) Place Order \n5) View Orders \n6) Logout");

        string input = Console.ReadLine();
        inputInt = int.TryParse(input, out choice);
      } while (!inputInt || choice <= 0 || choice > 6);
      return choice;

    }

    static int LocationsMenuInput(DatabaseContext context)
    {
      int choice;
      bool inputInt;
      do
      {
        Console.WriteLine($"\nChoose a location to view its products.");

        for (int i = 0; i < context.Stores.ToList().Count; i++)
        {
          Console.WriteLine($"Locations {i + 1}) {context.Stores.ToList()[i].State}");
        }

        string input = Console.ReadLine();
        inputInt = int.TryParse(input, out choice);
      } while (!inputInt || choice <= 0 || choice > 4);
      return choice;
    }

    static int ProductsMenuInput(DatabaseContext context, int locationMenuChoice)
    {
      List<int> IDRange = new List<int>();

      if (locationMenuChoice == 1)
      {
        var nameOfLocation = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var listOfvalues = context.Products.Where(p => p.StoreId == locationMenuChoice);
        Console.WriteLine($"Here are the books available in the {nameOfLocation.State} location");
        foreach (var i in listOfvalues)
        {
          Console.WriteLine($"\nTo select the book below, enter {i.ProductId}");
          IDRange.Add(i.ProductId);
          Console.WriteLine(i.ToString());
        }
      }
      else if (locationMenuChoice == 2)
      {
        var nameOfLocation = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var listOfvalues = context.Products.Where(p => p.StoreId == locationMenuChoice);
        Console.WriteLine($"Here are the books available in the {nameOfLocation.State} location");
        foreach (var i in listOfvalues)
        {
          Console.WriteLine($"\nTo select the book below, enter {i.ProductId}");
          IDRange.Add(i.ProductId);
          Console.WriteLine(i.ToString());
        }
      }
      else if (locationMenuChoice == 3)
      {
        var nameOfLocation = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var listOfvalues = context.Products.Where(p => p.StoreId == locationMenuChoice);
        Console.WriteLine($"Here are the books available in the {nameOfLocation.State} location");
        foreach (var i in listOfvalues)
        {
          Console.WriteLine($"\nTo select the book below, enter {i.ProductId}");
          IDRange.Add(i.ProductId);
          Console.WriteLine(i.ToString());
        }
      }
      else if (locationMenuChoice == 4)
      {
        var nameOfLocation = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var listOfvalues = context.Products.Where(p => p.StoreId == locationMenuChoice);
        Console.WriteLine($"Here are the books available in the {nameOfLocation.State} location");
        foreach (var i in listOfvalues)
        {
          Console.WriteLine($"\nTo select the book below, enter {i.ProductId}");
          IDRange.Add(i.ProductId);
          Console.WriteLine(i.ToString());
        }
      }

      int choice;
      bool inputInt;
      do
      {
        Console.WriteLine("Enter the number for the book you want");
        string input = Console.ReadLine();
        inputInt = int.TryParse(input, out choice);
      } while (!inputInt || !IDRange.Contains(choice));

      var productToAdd = context.Products
        .Where(p => p.ProductId == choice).FirstOrDefault();

      var shoppingCart = context.ShoppingCarts
        .Where(sh => sh.ShoppingCartId == LoggedInCustomer.ShoppingCartId).FirstOrDefault();

      if (productToAdd.ShoppingCartId == 0)
      {
        productToAdd.ShoppingCartId = LoggedInCustomer.ShoppingCartId;
        shoppingCart.AmountOfProducts++;
        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("That book is already reserved for another customer");
      }

      return choice;
    }

    static void OrdersMenuInput(DatabaseContext context)
    {
      int orderChoice;
      bool orderInputInt;
      do
      {
        Console.WriteLine("1) View order details \n2) View order history of location\n3) View order history of customer");

        string input = Console.ReadLine();
        orderInputInt = int.TryParse(input, out orderChoice);
      } while (!orderInputInt || orderChoice <= 0 || orderChoice > 3);


      if (orderChoice == 1)
      {
        var orders = context.Orders.Where(o => o.CustomerId == LoggedInCustomer.CustomerId).ToList();
        Console.WriteLine($"Displaying order details for {LoggedInCustomer.UserName}");
        foreach (var o in orders)
        {
          Console.WriteLine($" OrderId {o.OrderId}\n TimeOfOrder {o.TimeOfOrder}\n StoreAddress {o.StoreAddress}");
        }
      }
      else if (orderChoice == 2)
      {
        int locationChoice;
        bool locationInputInt;

        do
        {
          Console.WriteLine($"\nChoose a location to view its order history.");

          for (int i = 0; i < context.Stores.ToList().Count; i++)
          {
            Console.WriteLine($"Locations {i + 1}) {context.Stores.ToList()[i].State}");
          }

          string input = Console.ReadLine();
          locationInputInt = int.TryParse(input, out locationChoice);
        } while (!locationInputInt || locationChoice <= 0 || locationChoice > 4);

        var store = context.Stores.Where(s => s.StoreId == locationChoice).FirstOrDefault();
        var orders = context.Orders.Where(o => o.StoreAddress == store.ToString());

        foreach (var o in orders)
        {
          Console.WriteLine($" OrderId{o.OrderId}\n TimeOfOrder {o.TimeOfOrder}");
        }
      }
      else if (orderChoice == 3)
      {
        List<int> IDRange = new List<int>();
        int customerChoice;
        bool customerInputInt;
        var customers = context.Customers.ToList();

        do
        {
          Console.WriteLine($"\nChoose a customer to view their order history.");

          for (int i = 0; i < customers.Count; i++)
          {
            IDRange.Add(i + 1);
            Console.WriteLine($"Customer {i + 1}) {customers[i].UserName}");
          }

          string input = Console.ReadLine();
          customerInputInt = int.TryParse(input, out customerChoice);
        } while (!customerInputInt || !IDRange.Contains(customerChoice));

        var orders = context.Orders.Where(o => o.CustomerId == customerChoice);

        Console.WriteLine($"Here is the order history for {customers[customerChoice - 1].UserName}");
        foreach (var o in orders.ToList())
        {
          Console.WriteLine($" OrderId {o.OrderId}\n TimeOfOrder{o.TimeOfOrder}");
        }
      }

    }

    static int StartMenuInput()
    {
      int choice;
      bool inputInt;
      do
      {
        Console.WriteLine("\nStart Menu\n1) Login\n2) Sign Up\n3) Exit");
        string input = Console.ReadLine();
        inputInt = int.TryParse(input, out choice);
      } while (!inputInt || choice <= 0 || choice > 3);
      return choice;
    }

    static void GetCart(DatabaseContext context)
    {
      var shoppingCart = context.ShoppingCarts.Where(c => c.ShoppingCartId == LoggedInCustomer.ShoppingCartId).FirstOrDefault();
      Console.WriteLine($"you have {shoppingCart.AmountOfProducts} item(s) in your cart.");
    }

    static void GetCustomers(DatabaseContext context)
    {
      string name;
      do
      {
        Console.WriteLine("Enter the name of the customer to search.");
        name = Console.ReadLine();
        name = name.Trim();
      } while (name.Length == 0);
      var customer = context.Customers.Where(c => c.FirstName == name).ToList();
      if (customer.Count == 0)
      {
        Console.WriteLine("Didn't find any customers by that name.");
      }
      else
      {
        Console.WriteLine("Here are the customer(s) with that name.");
        foreach (var c in customer)
        {
          Console.WriteLine(c.ToString());
        }
      }
    }

    static void PlaceOrder(DatabaseContext context)
    {
      Order order = new Order();
      List<OrderProduct> orderProducts = new List<OrderProduct>();
      var productsToAddToOrder = context.Products.Where(p => p.ShoppingCartId == LoggedInCustomer.ShoppingCartId);
      var storeOfProducts = context.Stores.Where(s => s.StoreId == productsToAddToOrder.ToList()[0].StoreId).FirstOrDefault();
      var shoppingCart = context.ShoppingCarts.Where(sh => sh.ShoppingCartId == LoggedInCustomer.ShoppingCartId).FirstOrDefault();

      foreach (var p in productsToAddToOrder)
      {
        OrderProduct orderProduct = new OrderProduct();
        orderProduct.ProductId = p.ProductId;
        orderProducts.Add(orderProduct);
      }

      order.TimeOfOrder = DateTime.Now;
      order.OrderProducts = orderProducts;
      order.CustomerId = LoggedInCustomer.CustomerId;
      order.StoreAddress = $"{storeOfProducts.StreetAddress}, {storeOfProducts.City}, {storeOfProducts.State} {storeOfProducts.ZIP}";

      foreach (var p in productsToAddToOrder)
      {
        Console.WriteLine($"products to add to order {p.Title}");
        p.ShoppingCartId = 0;
      }

      storeOfProducts.Inventory -= productsToAddToOrder.ToList().Count;
      shoppingCart.AmountOfProducts = 0;

      context.Orders.Add(order);
      context.SaveChanges();
    }
  }
}

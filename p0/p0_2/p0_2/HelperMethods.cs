using System;
using p0_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p0_2
{
  class ShoppingCart : IShoppingCart
  {
    public List<Inventory> Inventories { get; set; } = new List<Inventory>();
    public double Total { get; set; } = 0;

    public bool IsEmpty() => Inventories.Count == 0;
  }

  interface IShoppingCart
  {
    bool IsEmpty();
  }

  class HelperMethods
  {
    public static void StartMenu()
    {
      Console.WriteLine("Welcome to Bookopolis!\nWe have the greatest selection of books at the best prices");
    }

    public static int StartMenuInput()
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

    public static Customer SignUp()
    {
      string firstname, lastname, username, password;
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

    public static void Login(DatabaseContext context, ref Customer LoggedInCustomer)
    {
      string username, password;

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
      }
      else
      {
        Console.WriteLine("Incorrect username or password");
        LoggedInCustomer = new Customer();
      }
    }

    public static void AddCustomerToDB(Customer newCustomer, DatabaseContext context, ref Customer LoggedInCustomer)
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
            context.Customers.Add(newCustomer);
            context.SaveChanges();
            var c = context.Customers.Where(c => c.UserName == newCustomer.UserName).FirstOrDefault();
            LoggedInCustomer = c;
          }
          else
          {
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
        context.Customers.Add(newCustomer);
        context.SaveChanges();
        var c = context.Customers.Where(c => c.UserName == newCustomer.UserName).FirstOrDefault();
        LoggedInCustomer = c;
      }
    }

    public static int LoggedInMenuInput(Customer LoggedInCustomer)
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

    public static int LocationsMenuInput(DatabaseContext context)
    {
      int choice;
      bool inputInt;
      do
      {
        Console.WriteLine($"\nChoose a location to view its products.");
        var stores = context.Stores.ToList();
        for (int i = 0; i < stores.Count; i++) Console.WriteLine($"Locations {i + 1}) {stores[i].State}");

        string input = Console.ReadLine();
        inputInt = int.TryParse(input, out choice);
      } while (!inputInt || choice <= 0 || choice > 4);
      return choice;
    }

    public static void ProductsMenuInput(DatabaseContext context, int locationMenuChoice, ref ShoppingCart ShoppingCart)
    {
      List<int> IDRange = new List<int>();

      if (locationMenuChoice == 1)
      {
        var stores = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var inventories = context.Inventories.Where(i => i.StoreId == locationMenuChoice).OrderBy(pid => pid.ProductId).ToList();
        var products = context.Products.ToList();

        Console.WriteLine($"Here are the books available in the {stores.State} location");

        foreach (var inv in inventories)
        {
          Console.WriteLine(inv.ToString());
        }

        for (int i = 0; i < products.Count; i++)
        {
          Console.WriteLine($"\nTo select the book below, enter {products[i].ProductId}. There are {inventories[i].Amount} available.");
          IDRange.Add(products[i].ProductId);
          Console.WriteLine(products[i].ToString());
        }
      }
      else if (locationMenuChoice == 2)
      {
        var stores = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var inventories = context.Inventories.Where(i => i.StoreId == locationMenuChoice).OrderBy(pid => pid.ProductId).ToList();
        var products = context.Products.ToList();

        Console.WriteLine($"Here are the books available in the {stores.State} location");

        foreach (var inv in inventories)
        {
          Console.WriteLine(inv.ToString());
        }

        for (int i = 0; i < products.Count; i++)
        {
          Console.WriteLine($"\nTo select the book below, enter {products[i].ProductId}. There are {inventories[i].Amount} available.");
          IDRange.Add(products[i].ProductId);
          Console.WriteLine(products[i].ToString());
        }
      }
      else if (locationMenuChoice == 3)
      {
        var stores = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var inventories = context.Inventories.Where(i => i.StoreId == locationMenuChoice).OrderBy(pid => pid.ProductId).ToList();
        var products = context.Products.ToList();

        Console.WriteLine($"Here are the books available in the {stores.State} location");

        foreach (var inv in inventories)
        {
          Console.WriteLine(inv.ToString());
        }

        for (int i = 0; i < products.Count; i++)
        {
          Console.WriteLine($"\nTo select the book below, enter {products[i].ProductId}. There are {inventories[i].Amount} available.");
          IDRange.Add(products[i].ProductId);
          Console.WriteLine(products[i].ToString());
        }
      }
      else if (locationMenuChoice == 4)
      {
        var stores = context.Stores.Where(s => s.StoreId == locationMenuChoice).FirstOrDefault();
        var inventories = context.Inventories.Where(i => i.StoreId == locationMenuChoice).OrderBy(pid => pid.ProductId).ToList();
        var products = context.Products.ToList();

        Console.WriteLine($"Here are the books available in the {stores.State} location");

        foreach (var inv in inventories)
        {
          Console.WriteLine(inv.ToString());
        }

        for (int i = 0; i < products.Count; i++)
        {
          Console.WriteLine($"\nTo select the book below, enter {products[i].ProductId}. There are {inventories[i].Amount} available.");
          IDRange.Add(products[i].ProductId);
          Console.WriteLine(products[i].ToString());
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
      var inventoryOfProd = context.Inventories.Where(i => i.ProductId == choice && i.StoreId == locationMenuChoice).FirstOrDefault();

      if (inventoryOfProd.Amount == 0)
      {
        Console.WriteLine("There are no copies left for that book.");
      }
      else if (ShoppingCart.IsEmpty())
      {
        inventoryOfProd.Amount--;
        ShoppingCart.Inventories.Add(inventoryOfProd);
        ShoppingCart.Total += productToAdd.Price;
      }
      else
      {
        for (int i = 0; i < ShoppingCart.Inventories.Count; i++)
        {
          if (ShoppingCart.Inventories[i].InventoryId == inventoryOfProd.InventoryId)
          {
            int c = ShoppingCart.Inventories.Count(inv => inv.InventoryId == inventoryOfProd.InventoryId);
            Console.WriteLine($"You're adding same prod from same store im counting {c} so far");
            inventoryOfProd.Amount--;
            ShoppingCart.Inventories.Add(inventoryOfProd);
            ShoppingCart.Total += productToAdd.Price;
            break;
          }
          else
          {
            inventoryOfProd.Amount--;
            ShoppingCart.Inventories.Add(inventoryOfProd);
            ShoppingCart.Total += productToAdd.Price;
            break;
          }
        }
      }
    }

    public static void OrdersMenuInput(DatabaseContext context, Customer LoggedInCustomer)
    {
      int orderChoice;
      bool orderInputInt;
      do
      {
        Console.WriteLine($"1) View order details for {LoggedInCustomer.UserName} \n2) View order history of location\n3) View order history of customer");

        string input = Console.ReadLine();
        orderInputInt = int.TryParse(input, out orderChoice);
      } while (!orderInputInt || orderChoice <= 0 || orderChoice > 3);


      if (orderChoice == 1)
      {

        //var orders = context.Orders.Where(o => o.CustomerId == LoggedInCustomer.CustomerId).ToList();
        var orderProducts = context.OrderProducts.Where(op => op.CustomerId == LoggedInCustomer.CustomerId).ToList();
        //var order = context.Orders.Where(o => o.Cu == orderProducts[0].OrderId).ToList();

        var addresses = (from s in context.Stores
                         from op in context.OrderProducts
                         where op.CustomerId == LoggedInCustomer.CustomerId && s.StoreId == op.StoreId
                         select new { s.StreetAddress, s.City, s.State, op.OrderId }).Distinct();

        var totals = (from op in context.OrderProducts
                      from o in context.Orders
                      where op.CustomerId == LoggedInCustomer.CustomerId && o.OrderId == op.OrderId
                      select new { o.OrderId, o.Total, o.TimeOfOrder }).Distinct();

        Console.WriteLine($"Store locations for Orders of {LoggedInCustomer.UserName}\n");

        foreach (var add in addresses.ToList())
        {
          Console.WriteLine($"OrderId {add.OrderId} Store location {add.StreetAddress} {add.City} {add.State}");
        }

        Console.WriteLine("\nTotal amounts and dates of Orders\n");
        foreach (var t in totals)
        {
          Console.WriteLine($"OrderId {t.OrderId} Total price ${t.Total} Time of order {t.TimeOfOrder}");
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
        //var orders = context.Orders.Where(o => o.StoreAddress == store.ToString());
        var orderProducts = context.OrderProducts.Where(op => op.StoreId == store.StoreId).ToList();

        foreach (var op in orderProducts)
        {
          Console.WriteLine($" OrderId {op.OrderId}\n Store location {store.State}\n ProductId {op.ProductId}");
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

        var orderProducts = context.OrderProducts.Where(op => op.CustomerId == customerChoice).ToList();

        Console.WriteLine($"Here is the order history for {customers[customerChoice - 1].UserName}");
        foreach (var op in orderProducts)
        {
          Console.WriteLine($" OrderId {op.OrderId}\n StoreId {op.StoreId}\n  ProductId {op.ProductId}");
        }
      }

    }

    public static void PlaceOrder(DatabaseContext context, ShoppingCart ShoppingCart, Customer LoggedInCustomer)
    {
      if (ShoppingCart.IsEmpty())
      {
        Console.WriteLine("Your shopping cart is empty, select some items to place an order.");
        return;
      }
      else
      {
        Order order = new Order();
        List<OrderProduct> orderProducts = new List<OrderProduct>();

        foreach (var sh in ShoppingCart.Inventories)
        {
          OrderProduct orderProduct = new OrderProduct();
          orderProduct.ProductId = sh.ProductId;
          orderProduct.StoreId = sh.StoreId;
          orderProduct.CustomerId = LoggedInCustomer.CustomerId;
          orderProducts.Add(orderProduct);
        }

        Console.WriteLine($"the total is {ShoppingCart.Total}");

        order.TimeOfOrder = DateTime.Now;
        order.OrderProducts = orderProducts;
        order.Total = ShoppingCart.Total;

        context.Orders.Add(order);
        context.SaveChanges();
      }
    }

    public static void GetCart(ShoppingCart ShoppingCart)
    {
      if (ShoppingCart.IsEmpty()) Console.WriteLine("Your shopping cart is empty");
      else
      {
        Console.WriteLine($"you have {ShoppingCart.Inventories.Count} item(s) in your cart. Total price is ${ShoppingCart.Total}");
        foreach (var sh in ShoppingCart.Inventories) Console.WriteLine(sh.ToString());
      }
    }

    public static void GetCustomers(DatabaseContext context)
    {
      string name;
      do
      {
        Console.WriteLine("Enter the name of the customer to search.");
        name = Console.ReadLine();
        name = name.Trim();
      } while (name.Length == 0);

      var customer = context.Customers.Where(c => c.FirstName == name).ToList();
      if (customer.Count == 0) Console.WriteLine("Didn't find any customers by that name.");
      else
      {
        Console.WriteLine("Here are the customer(s) with that name.");
        foreach (var c in customer) Console.WriteLine(c.ToString());
      }
    }

    public static void SeedStores(DatabaseContext context)
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

      List<Product> products = new List<Product>()
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

      foreach (var prod in products) context.Products.Add(prod);
      context.SaveChanges();

      var prodContext = context.Products.ToList();

      foreach (var store in stores)
      {
        List<Inventory> inventories = new List<Inventory>();
        foreach (var prod in prodContext)
        {
          Inventory inventory = new Inventory();
          inventory.ProductId = prod.ProductId;
          inventory.Amount = 2;
          inventories.Add(inventory);
        }
        store.Inventories = inventories;
      }

      foreach (var store in stores) context.Stores.Add(store);
      context.SaveChanges();
    }
  }
}

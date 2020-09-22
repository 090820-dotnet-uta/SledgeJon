using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using p0_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace p0_2.Tests
{
  public class UnitTest1
  {
    [Fact]
    public void StartMenuInputReturnsCorrectNum()
    {
      using (var sw = new StringWriter())
      {
        using (var sr = new StringReader("3"))
        {
          Console.SetOut(sw);
          Console.SetIn(sr);

          // Act statement ...
          HelperMethods helperMethods = new HelperMethods();
          int result = helperMethods.StartMenuInput();

          // Assert statement ...
          Assert.InRange(result, 1, 3);
        }
      }
    }

    [Fact]
    public void SignUpReturnsCustomer()
    {
      using (var sw = new StringWriter())
      {
        using (var sr = new StringReader("Saul\nGoodman\nBetterCallMe\nimagoodlawyer"))
        {
          Console.SetOut(sw);
          Console.SetIn(sr);

          // Act statement ...
          HelperMethods helperMethods = new HelperMethods();


          Customer result = helperMethods.SignUp();

          // Assert statement ...
          Assert.Equal("Saul", result.FirstName);
          Assert.Equal("Goodman", result.LastName);
          Assert.Equal("BetterCallMe", result.UserName);
          Assert.Equal("imagoodlawyer", result.Password);
        }
      }
    }

    [Fact]
    public void LoginHasCorrectUsernameAndPassword()
    {
      var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "LoginHasCorrectUsernameAndPassword")
            .Options;

      using (var context = new DatabaseContext(options))
      {
        context.Customers.Add(new Customer() { CustomerId = 1, FirstName = "Saul", LastName = "Goodman", UserName = "BetterCallMe", Password = "imagoodlawyer" });
        context.SaveChanges();
      }

      using (var context = new DatabaseContext(options))
      {

        HelperMethods helperMethods = new HelperMethods();
        Customer customer = context.Customers.Where(c => c.CustomerId == 1).FirstOrDefault();
        Customer loggedInCustomer = new Customer();


        using (var sw = new StringWriter())
        {
          using (var sr = new StringReader("BetterCallMe\nimagoodlawyer"))
          {
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act statement ...
            helperMethods.Login(context, ref loggedInCustomer);

            // Assert statement ...
            Assert.Equal("BetterCallMe", loggedInCustomer.UserName);
            Assert.Equal("imagoodlawyer", loggedInCustomer.Password);
          }
        }
      }
    }

    [Fact]
    public void LoginHasIncorrectUsernameOrPassword()
    {
      var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "LoginHasIncorrectUsernameOrPassword")
            .Options;

      using (var context = new DatabaseContext(options))
      {
        context.Customers.Add(new Customer() { CustomerId = 1, FirstName = "Saul", LastName = "Goodman", UserName = "BetterCallMe", Password = "imagoodlawyer" });
        context.SaveChanges();
      }

      using (var context = new DatabaseContext(options))
      {

        HelperMethods helperMethods = new HelperMethods();
        Customer customer = context.Customers.Where(c => c.CustomerId == 1).FirstOrDefault();
        Customer loggedInCustomer = new Customer();


        using (var sw = new StringWriter())
        {
          using (var sr = new StringReader("BetterCallMe\nimnotagoodlawyer"))
          {
            Console.SetOut(sw);
            Console.SetIn(sr);
            // Act statement ...
            helperMethods.Login(context, ref loggedInCustomer);

            // Assert statement ...
            Assert.Equal("Enter your username\r\nEnter your password\r\nIncorrect username or password\r\n", sw.ToString());

          }
        }
      }
    }

    [Fact]
    public void LoggedInMenuInputIsInRange()
    {
      HelperMethods helperMethods = new HelperMethods();
      Customer loggedInCustomer = new Customer() { CustomerId = 1, FirstName = "Saul", LastName = "Goodman", UserName = "BetterCallMe", Password = "imagoodlawyer" };

      using (var sw = new StringWriter())
      {
        using (var sr = new StringReader("5"))
        {
          Console.SetOut(sw);
          Console.SetIn(sr);

          // Act statement ...
          int result = helperMethods.LoggedInMenuInput(loggedInCustomer);

          // Assert statement ...
          Assert.InRange(result, 1, 6);

        }
      }
    }

    [Fact]
    public void LocationsMenuInputIsInRange()
    {
      var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "LocationsMenuInputIsInRange")
            .Options;
      HelperMethods helperMethods = new HelperMethods();

      using (var context = new DatabaseContext(options))
      {
        using (var sw = new StringWriter())
        {
          using (var sr = new StringReader("3"))
          {
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act statement ...
            int result = helperMethods.LocationsMenuInput(context);

            // Assert statement ...
            Assert.InRange(result, 1, 4);

          }
        }
      }
    }

    [Fact]
    public void PlaceOrderWithEmptyShoppingCart()
    {
      var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "PlaceOrderWithEmptyShoppingCart")
            .Options;
      HelperMethods helperMethods = new HelperMethods();
      ShoppingCart shoppingCart = new ShoppingCart();
      Customer loggedInCustomer = new Customer();

      using (var context = new DatabaseContext(options))
      {
        using (var sw = new StringWriter())
        {
          Console.SetOut(sw);

          // Act statement ...
          helperMethods.PlaceOrder(context, ref shoppingCart, loggedInCustomer);

          // Assert statement ...
          Assert.Equal("Your shopping cart is empty, select some items to place an order.\r\n", sw.ToString());
        }
      }
    }

    [Fact]
    public void PlaceOrderWritesTotal()
    {
      var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "PlaceOrderWritesTotal")
            .Options;
      HelperMethods helperMethods = new HelperMethods();
      ShoppingCart shoppingCart = new ShoppingCart();
      List<Inventory> InvList = new List<Inventory>()
        {
          new Inventory() { InventoryId = 1, ProductId = 1, StoreId = 1},
          new Inventory() { InventoryId = 2, ProductId = 2, StoreId = 2}
        };
      shoppingCart.Inventories = InvList;
      shoppingCart.Total = 5.50;


      Customer loggedInCustomer = new Customer();

      using (var context = new DatabaseContext(options))
      {
        using (var sw = new StringWriter())
        {
          Console.SetOut(sw);

          // Act statement ...
          helperMethods.PlaceOrder(context, ref shoppingCart, loggedInCustomer);

          // Assert statement ...
          Assert.Equal("the total is 5.5\r\n", sw.ToString());
        }
      }
    }

    [Fact]
    public void GetsCartWithEmptyShoppingCart()
    {
      HelperMethods helperMethods = new HelperMethods();
      ShoppingCart shoppingCart = new ShoppingCart();

      using (var sw = new StringWriter())
      {
        Console.SetOut(sw);

        // Act statement ...
        helperMethods.GetCart(shoppingCart);

        // Assert statement ...
        Assert.Equal("Your shopping cart is empty\r\n", sw.ToString());
      }
    }

    [Fact]
    public void GetCustomerDoesntFindCustomer()
    {
      var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "GetCustomerDoesntFindCustomer")
            .Options;
      HelperMethods helperMethods = new HelperMethods();

      using (var context = new DatabaseContext(options))
      {
        context.Customers.Add(new Customer() { CustomerId = 1, FirstName = "Saul", LastName = "Goodman", UserName = "BetterCallMe", Password = "imagoodlawyer" });
        context.SaveChanges();
      }

      using (var context = new DatabaseContext(options))
      {
        using (var sw = new StringWriter())
        {
          using (var sr = new StringReader("Saul"))
          {
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act statement ...
            helperMethods.GetCustomers(context);

            // Assert statement ...
            Assert.Equal("Enter the name of the customer to search.\r\nHere are the customer(s) with that name.\r\n1\nSaul\nGoodman\nBetterCallMe\r\n", sw.ToString());
          }
        }
      }
    }


    [Fact]
    public void StartMenuHasCorrectOutput()
    {
      using (var sw = new StringWriter())
      {
        Console.SetOut(sw);

        // Act statement ...
        HelperMethods helperMethods = new HelperMethods();
        helperMethods.StartMenu();

        // Assert statement ...
        Assert.Equal("Welcome to Bookopolis!\nWe have the greatest selection of books at the best prices\r\n", sw.ToString());
      }
    }

  }
}

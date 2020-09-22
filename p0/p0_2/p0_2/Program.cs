using Microsoft.EntityFrameworkCore;
using p0_2.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace p0_2
{
  class Program
  {

    static void Main(string[] args)
    {
      using (var context = new DatabaseContext())
      {

        Customer LoggedInCustomer = new Customer();
        ShoppingCart ShoppingCart = new ShoppingCart();
        HelperMethods helperMethods = new HelperMethods();
        //UNCOMMENT TO SEED THE DATABASE
        //helperMethods.SeedStores(context);
        //Console.ReadLine();

        helperMethods.StartMenu();
        int startMenuChoice;
        do
        {
          startMenuChoice = helperMethods.StartMenuInput();
          switch (startMenuChoice)
          {
            case 1:
              helperMethods.Login(context, ref LoggedInCustomer);
              Console.WriteLine(LoggedInCustomer.UserName);
              break;
            case 2:
              Console.WriteLine("Signing up");
              Customer newCustomer = helperMethods.SignUp();
              helperMethods.AddCustomerToDB(newCustomer, context, ref LoggedInCustomer);
              break;
            case 3:
              Environment.Exit(0);
              break;

            default:
              break;
          }

          // If initial login fails, catch with this loop
          while (LoggedInCustomer.UserName == null)
          {
            startMenuChoice = helperMethods.StartMenuInput();
            switch (startMenuChoice)
            {
              case 1:
                helperMethods.Login(context, ref LoggedInCustomer);
                break;
              case 2:
                Console.WriteLine("Signing up");
                Customer newCustomer = helperMethods.SignUp();
                helperMethods.AddCustomerToDB(newCustomer, context, ref LoggedInCustomer);
                break;
              case 3:
                Environment.Exit(0);
                break;

              default:
                break;
            }
          }

          int loggedInMenuInput;
          int locationMenuChoice;
          do
          {
            loggedInMenuInput = helperMethods.LoggedInMenuInput(LoggedInCustomer);
            switch (loggedInMenuInput)
            {
              case 1:
                locationMenuChoice = helperMethods.LocationsMenuInput(context);
                helperMethods.ProductsMenuInput(context, locationMenuChoice, ref ShoppingCart);
                break;
              case 2:
                helperMethods.GetCart(ShoppingCart);
                break;
              case 3:
                helperMethods.GetCustomers(context);
                break;
              case 4:
                helperMethods.PlaceOrder(context, ref ShoppingCart, LoggedInCustomer);
                break;
              case 5:
                helperMethods.OrdersMenuInput(context, LoggedInCustomer);
                break;
              case 6:
                LoggedInCustomer = new Customer();
                if (ShoppingCart.IsEmpty()) ShoppingCart = new ShoppingCart();
                else
                {
                  context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
                  ShoppingCart = new ShoppingCart();
                }
                break;

              default:
                break;
            }
          } while (LoggedInCustomer.UserName != null || loggedInMenuInput > 6);
        } while (LoggedInCustomer.UserName == null);
      }
    }
  }
}

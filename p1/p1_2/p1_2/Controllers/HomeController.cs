using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using p1_2.Data;
using p1_2.Models;

namespace p1_2.Controllers
{
  public class HomeController : Controller
  {
    private readonly BookopolisDbContext _db;

    public HomeController(BookopolisDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      //SeedStores(_db);

      ViewData["IsLoggedIn"] = "false";
      return View("Index");
    }

    public IActionResult Privacy()
    {
      return View("Privacy");
    }

    [HttpPost]
    public IActionResult HandlePost([FromBody] Customer customer)
    {
      var x = customer;
      return RedirectToAction("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public void SeedStores(BookopolisDbContext context)
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

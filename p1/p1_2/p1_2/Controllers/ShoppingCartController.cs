using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using p1_2.Data;
using p1_2.Models;

namespace p1_2.Controllers
{
  public class ShoppingCartController : Controller
  {
    private IMemoryCache _cache;//must set this for DI in Startup.cs
    private readonly BookopolisDbContext _db;
    List<ShoppingCart> shoppingCart;
    public ShoppingCartController(IMemoryCache cache, BookopolisDbContext db)
    {
      _cache = cache;
      _db = db;

      if (!_cache.TryGetValue("shoppingCart", out shoppingCart))
      {
        _cache.Set("shoppingCart", new List<ShoppingCart>());
        _cache.TryGetValue("shoppingCart", out shoppingCart);
      }
    }

    public IActionResult Delete(ShoppingCart sh)
    {
      ShoppingCart x = shoppingCart.Find(s => s.ProductId == sh.ProductId && s.StoreId == sh.StoreId);
      shoppingCart.Remove(x);
      _cache.Set("shoppingCart", shoppingCart);
      return RedirectToAction("Index", "Store");

    }

    public IActionResult Checkout()
    {
      if (Util.Util.IsLoggedIn(_cache))
      {
        return RedirectToAction("Login", "Customer");
      }

      var x = shoppingCart.Sum(s => s.Price);
      ViewData["CheckoutTotal"] = x;

      CheckoutView checkoutView = new CheckoutView();
      checkoutView.ShoppingCarts = shoppingCart;
      checkoutView.CustomerAddress = new CustomerAddress();


      return View(checkoutView);
    }

    [HttpPost]
    public IActionResult PlaceOrder([Bind("StreetAddress,Country,City,State,ZIP")] CustomerAddress customerAddress)
    {
      Order order = new Order();
      List<OrderProduct> orderProducts = new List<OrderProduct>();
      Customer tempCust = (Customer)_cache.Get("LoggedInCustomer");
      Customer customer = _db.Customers.FirstOrDefault(c => c.CustomerId == tempCust.CustomerId);

      // TODO Check to see if customer already has any CustomerAddresses

      List<CustomerAddress> customerAddresses = new List<CustomerAddress>();

      Dictionary<int, int> myDict = new Dictionary<int, int>();


      foreach (var sh in shoppingCart)
      {
        OrderProduct orderProduct = new OrderProduct();
        orderProduct.ProductId = sh.ProductId;
        orderProduct.StoreId = sh.StoreId;
        orderProduct.CustomerId = customer.CustomerId;
        orderProducts.Add(orderProduct);
      }

      for (int i = 0; i < shoppingCart.Count; i++)
      {
        myDict[shoppingCart[i].Inventory.InventoryId] = shoppingCart.Count(s => s.Inventory.InventoryId == shoppingCart[i].Inventory.InventoryId);
      }

      // modify inventory amount values
      for (int i = 0; i < myDict.Keys.Count; i++)
      {
        for (int j = 0; j < _db.Inventories.ToList().Count; j++)
        {
          if (myDict.Keys.ToList()[i] == _db.Inventories.ToList()[j].InventoryId)
          {
            _db.Inventories.ToList()[j].Amount -= myDict[_db.Inventories.ToList()[j].InventoryId];
          }
        }
      }

      order.TimeOfOrder = DateTime.Now;
      order.OrderProducts = orderProducts;
      order.Total = shoppingCart.Sum(sh => sh.Price);

      customerAddresses.Add(customerAddress);

      //add new address to customer
      customer.CustomerAddresses = customerAddresses;


      _db.Orders.Add(order);
      _db.SaveChanges();

      _cache.Set("shoppingCart", new List<ShoppingCart>());
      shoppingCart = new List<ShoppingCart>();
      return RedirectToAction("Orders");

    }

    public IActionResult Orders()
    {
      var orders = _db.Orders;
      return View(orders);
    }

    public IActionResult Index()
    {
      if (Util.Util.IsLoggedIn(_cache))
      {
        return RedirectToAction("Login", "Customer");
      }
      return View(shoppingCart);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using p1_2.Data;
using p1_2.Models;

namespace p1_2.Controllers
{
  public class OrderController : Controller
  {
    private IMemoryCache _cache;//must set this for DI in Startup.cs
    private readonly BookopolisDbContext _db;
    List<ShoppingCart> shoppingCart;
    public OrderController(IMemoryCache cache, BookopolisDbContext db)
    {
      _cache = cache;
      _db = db;

      if (!_cache.TryGetValue("shoppingCart", out shoppingCart))
      {
        _cache.Set("shoppingCart", new List<ShoppingCart>());
        _cache.TryGetValue("shoppingCart", out shoppingCart);
      }
    }

    public IActionResult TestAction(int? id)
    {
      return View("MyOrders");
    }

    public IActionResult SelectStore(int? id)
    {
      ViewData["StoreOrders"] = "active";
      var orderProducts = _db.OrderProducts.Where(op => op.StoreId == id).Distinct();
      var stores = _db.Stores.ToList();
      if (orderProducts.ToList().Count == 0)
      {
        OrderView orderViewNone = new OrderView();
        orderViewNone.EmptyMessage = "There are no orders for that state";
        orderViewNone.IsEmpty = true;
        orderViewNone.Stores = stores;
        return View("StoreOrders", orderViewNone);
      }

      var orders = _db.Orders.Where(o => orderProducts.All(op => op.OrderId == o.OrderId)).ToList();
      OrderView orderView = new OrderView();
      orderView.Orders = orders;
      orderView.Stores = stores;


      return RedirectToAction("StoreOrders", new { id });
      //return View(orderView);

    }

    public IActionResult StoreOrders(int id)
    {
      ViewData["StoreOrders"] = "active";
      ViewData["CurrentStore"] = "Missouri";
      var orderProducts = _db.OrderProducts.Where(op => op.StoreId == id).Distinct();
      var stores = _db.Stores.ToList();
      if (orderProducts.ToList().Count == 0)
      {
        OrderView orderViewNone = new OrderView();
        orderViewNone.EmptyMessage = "There are no orders for that state";
        orderViewNone.IsEmpty = true;
        orderViewNone.Stores = stores;
        return View(orderViewNone);
      }

      var orders = _db.Orders.Where(o => orderProducts.All(op => op.OrderId == o.OrderId)).ToList();


      OrderView orderView = new OrderView();
      orderView.Orders = orders;
      orderView.Stores = stores;


      return View(orderView);
    }

    public IActionResult MyOrders()
    {
      ViewData["MyOrders"] = "active";
      Customer tempCust = (Customer)_cache.Get("LoggedInCustomer");
      var orderProducts = _db.OrderProducts.Where(op => op.CustomerId == tempCust.CustomerId);

      var orders = _db.Orders.Where(o => orderProducts.All(op => op.OrderId == o.OrderId)).ToList();

      var stores = _db.Stores.ToList();


      OrderView orderView = new OrderView();
      orderView.Orders = orders;
      orderView.Stores = stores;
      return View(orderView);
    }

    public IActionResult CustomerOrders()
    {
      ViewData["CustomerOrders"] = "active";

      return View();
    }
  }
}

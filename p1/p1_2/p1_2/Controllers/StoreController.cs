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
  public class StoreController : Controller
  {
    private readonly BookopolisDbContext _db;
    private IMemoryCache _cache;//must set this for DI in Startup.cs

    public StoreController(BookopolisDbContext db, IMemoryCache cache)
    {
      _db = db;
      _cache = cache;
    }

    public IActionResult LoadLocalStorage()
    {
      //send username to local store GET request here!
      string s = _cache.Get("UserName").ToString();

      return new JsonResult(s);
    }

    public IActionResult Index()
    {
      if (Util.Util.IsLoggedIn(_cache))
      {
        return RedirectToAction("Login", "Customer");
      }

      IEnumerable<Store> storeList = _db.Stores;
      return View(storeList);
    }

    public IActionResult LoadCustomer(Customer c)
    {
      _cache.Set("UserName", c.UserName);
      return RedirectToAction("Index");
    }

    public IActionResult StoreProducts(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      Store store = _db.Stores.FirstOrDefault(s => s.StoreId == id);

      if (store == null)
      {
        return NotFound();
      }

      return RedirectToAction("Index", "Product", new { id });
    }

  }
}

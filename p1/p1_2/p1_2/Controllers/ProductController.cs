using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using p1_2.Data;
using p1_2.Models;
using Microsoft.Extensions.Caching.Memory;

namespace p1_2.Controllers
{
  public class ProductController : Controller
  {
    private readonly BookopolisDbContext _db;
    private IMemoryCache _cache;
    List<ShoppingCart> shoppingCartProducts;
    public ProductController(BookopolisDbContext db, IMemoryCache cache)
    {
      _cache = cache;
      _db = db;

      if (!_cache.TryGetValue("shoppingCart", out shoppingCartProducts))
      {
        _cache.Set("shoppingCart", new List<ShoppingCart>());
        _cache.TryGetValue("shoppingCart", out shoppingCartProducts);
      }
    }
    public IActionResult Index(int? id)
    {
      if (Util.Util.IsLoggedIn(_cache))
      {
        return RedirectToAction("Login", "Customer");
      }

      List<ShoppingCart> x = (List<ShoppingCart>)_cache.Get("shoppingCart");

      _cache.Set("StoreId", id);
      var store = _db.Stores.FirstOrDefault(s => s.StoreId == id);
      _cache.Set("Store", store);
      var inv = _db.Inventories.Where(i => i.StoreId == id).OrderBy(pid => pid.ProductId).ToList();
      var prodList = _db.Products.ToList();
      List<ProductView> prodViews = new List<ProductView>();

      for (int i = 0; i < x.Count; i++)
      {
        for (int j = 0; j < inv.Count; j++)
        {
          if (x[i].Inventory.InventoryId == inv[j].InventoryId)
          {
            inv[j].Amount = x[i].StockAmount;
          }
        }
      }

      for (int i = 0; i < inv.Count; i++)
      {
        ProductView productView = new ProductView()
        {
          Amount = inv[i].Amount,
          Author = prodList[i].Author,
          Description = prodList[i].Description,
          Price = prodList[i].Price,
          ProductId = prodList[i].ProductId,
          Title = prodList[i].Title
        };
        prodViews.Add(productView);
      }

      IEnumerable<ProductView> EProdViews = prodViews;

      return View(EProdViews);
    }

    public IActionResult Details(int? id)
    {
      if (Util.Util.IsLoggedIn(_cache))
      {
        return RedirectToAction("Login", "Customer");
      }
      if (id == null)
      {
        return NotFound();
      }

      var inv = _db.Inventories.FirstOrDefault(i => i.ProductId == id && i.StoreId == (int)_cache.Get("StoreId"));
      Product prod = _db.Products.FirstOrDefault(p => p.ProductId == id);

      var x = shoppingCartProducts.Where(sh => sh.StoreId == (int)_cache.Get("StoreId") && sh.ProductId == id).ToList();
      ProductView productView = new ProductView();
      if (x.Count > 0)
      {
        productView.Amount = x[x.Count - 1].StockAmount;
        productView.Author = prod.Author;
        productView.Description = prod.Description;
        productView.Price = prod.Price;
        productView.ProductId = prod.ProductId;
        productView.Title = prod.Title;
        productView.IsInCart = (x.Count() == 2);

      }
      else
      {
        productView.Amount = inv.Amount;
        productView.Author = prod.Author;
        productView.Description = prod.Description;
        productView.Price = prod.Price;
        productView.ProductId = prod.ProductId;
        productView.Title = prod.Title;
        productView.IsInCart = (x.Count() == 2);

      }

      if (prod == null)
      {
        return NotFound();
      }

      return View(productView);
    }

    public IActionResult AddToCart(ProductView p)
    {
      int temp = (int)_cache.Get("StoreId");
      Store store = (Store)_cache.Get("Store");
      p.Amount--;
      ShoppingCart sh = new ShoppingCart()
      {
        StockAmount = p.Amount,
        Author = p.Author,
        Title = p.Title,
        Price = p.Price,
        StoreId = temp,
        ProductId = p.ProductId,
        State = store.State
      };
      var inv = _db.Inventories.FirstOrDefault(i => i.ProductId == sh.ProductId && i.StoreId == (int)_cache.Get("StoreId"));

      sh.Inventory = inv;
      shoppingCartProducts.Add(sh);

      _cache.Set("shoppingCart", shoppingCartProducts);

      return RedirectToAction("Index", "Store");
    }

  }
}

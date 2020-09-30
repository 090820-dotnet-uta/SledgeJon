using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1_2.Models
{
  public class ShoppingCart
  {
    public int ShoppingCartId { get; set; }
    public Product Product { get; set; }
    public int StockAmount { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int StoreId { get; set; }
    public int ProductId { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public double Price { get; set; }
    public int ShoppingCartAmount { get; set; }
    public Inventory Inventory { get; set; }
  }
}

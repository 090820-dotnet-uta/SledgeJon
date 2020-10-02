using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1_2.Models
{
  public class OrderView
  {
    public int OrderViewId { get; set; }
    public List<ShoppingCart> ShoppingCarts { get; set; }
    public List<Order> Orders { get; set; }
    public List<Store> Stores { get; set; }
    public bool IsEmpty { get; set; }
    public string EmptyMessage { get; set; }
  }
}

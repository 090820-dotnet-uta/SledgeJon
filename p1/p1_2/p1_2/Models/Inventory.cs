using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1_2.Models
{
  public class Inventory
  {
    public int InventoryId { get; set; }
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public int Amount { get; set; }
  }
}

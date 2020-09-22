using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  class Inventory
  {
    public int InventoryId { get; set; }
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public int Amount { get; set; }
    public override string ToString()
    {
      return $"InvId {InventoryId} ProdId {ProductId} StoreId {StoreId} Amount {Amount}";
    }
  }
}

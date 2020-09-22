using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  public class Store
  {
    public int StoreId { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZIP { get; set; }
    public List<Inventory> Inventories { get; set; }

    public override string ToString()
    {
      return $"{StreetAddress}, {City}, {State} {ZIP}";
    }
  }
}

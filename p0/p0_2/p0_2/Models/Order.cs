using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  class Order
  {
    public int OrderId { get; set; }
    public string StoreAddress { get; set; }
    public Customer Customer { get; set; }
    public int CustomerId { get; set; }
    public DateTime TimeOfOrder { get; set; }
    public List<Product> Products { get; set; }
  }
}

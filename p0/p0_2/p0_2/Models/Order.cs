using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  class Order
  {
    public int OrderId { get; set; }
    public DateTime TimeOfOrder { get; set; }
    //public List<Product> Products { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
  }
}

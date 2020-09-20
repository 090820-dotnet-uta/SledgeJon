﻿using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  class Store
  {
    public int StoreId { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZIP { get; set; }
    public int Inventory { get; set; } = 0;
    public List<Product> Products { get; set; }

    public override string ToString()
    {
      return $"{StreetAddress}, {City}, {State} {ZIP}";
    }
  }
}

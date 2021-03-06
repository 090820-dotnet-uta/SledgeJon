﻿using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  public class Customer
  {
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public override string ToString()
    {
      return $"{CustomerId}\n{FirstName}\n{LastName}\n{UserName}";
    }

  }
}

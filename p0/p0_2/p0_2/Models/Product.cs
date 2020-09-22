using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
      return $"Title - {Title}\nAuthor - {Author}\nDescription - {Description}\nPrice - {Price}";
    }
  }
}

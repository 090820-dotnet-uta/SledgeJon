using Microsoft.EntityFrameworkCore;
using p1_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1_2.Data
{
  public class BookopolisDbContext : DbContext
  {
    public BookopolisDbContext(DbContextOptions<BookopolisDbContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
  }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace p0_2.Models
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
      : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Inventory> Inventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-EQEIR3A;Initial Catalog=p0_3;Integrated Security=True");
      }
      base.OnConfiguring(optionsBuilder);
    }
  }
}

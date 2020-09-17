using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPS_Game_Refactored.Models
{
  class DbContextClass : DbContext
  {
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Round> Rounds { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-EQEIR3A;Initial Catalog=RPS_DB_2;Integrated Security=True");
      }

      base.OnConfiguring(optionsBuilder);
    }
  }
}

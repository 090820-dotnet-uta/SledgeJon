using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todorestapi.Models;

namespace todorestapi.Models
{
  public class TodoDbContext : DbContext
  {
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
    : base(options)
    {
    }

    public DbSet<TodoItemDTO> TodoItems { get; set; }

  }
}

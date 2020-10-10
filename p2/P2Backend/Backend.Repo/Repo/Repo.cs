using Backend.Repo.Data;
using Backend.Repo.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Repo
{
  public class Repo<T> : IRepo<T> where T : class
  {
    private readonly AppDbContext _db;
    internal DbSet<T> dbSet;

    public Repo(AppDbContext db)
    {
      _db = db;
      this.dbSet = _db.Set<T>();
    }

    public T Get(int id)
    {
      return dbSet.Find(id);
    }
    public ValueTask<T> GetAsync(int id)
    {
      return dbSet.FindAsync(id);
    }

    public async ValueTask<List<T>> GetAllAsync()
    {
      return await dbSet.ToListAsync();
    }
  }
}

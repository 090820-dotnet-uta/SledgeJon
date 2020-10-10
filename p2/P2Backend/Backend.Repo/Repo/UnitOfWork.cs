using Backend.Repo.Data;
using Backend.Repo.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repo.Repo
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db)
    {
      _db = db;
      Post = new PostRepo(_db);
    }

    public IPostRepo Post { get; private set; }

    public void Dispose()
    {
      _db.Dispose();
    }

    public void Save()
    {
      _db.SaveChanges();
    }
  }
}

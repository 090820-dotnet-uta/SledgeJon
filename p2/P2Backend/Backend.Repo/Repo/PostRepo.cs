using Backend.Models;
using Backend.Repo.Data;
using Backend.Repo.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repo.Repo
{
  public class PostRepo : Repo<Post>, IPostRepo
  {
    private readonly AppDbContext _db;

    public PostRepo(AppDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(Post post)
    {
      throw new NotImplementedException();
    }
  }
}

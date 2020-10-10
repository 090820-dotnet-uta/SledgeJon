using Backend.Repo.Data;
using Backend.Repo.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Backend.Test
{
  public class UnitTest1
  {
    private readonly IUnitOfWork _unitOfWork;

    [Fact]
    public void GetPostGetsPost()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "GetPostGetsPost")
        .Options;

      using (var context = new AppDbContext(options))
      {

      }
    }
  }
}

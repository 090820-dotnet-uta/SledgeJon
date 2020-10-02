using System;
using Xunit;
using p1_2.Controllers;
using Microsoft.EntityFrameworkCore;
using p1_2.Data;
using Microsoft.AspNetCore.Mvc;

namespace p1_2.Test
{
  public class UnitTest1
  {
    private HomeController _homeController;
    public UnitTest1()
    {
      var options = new DbContextOptionsBuilder<BookopolisDbContext>()
      .UseInMemoryDatabase(databaseName: "HomeIndexViewIsReturned")
      .Options;

      using (var context = new BookopolisDbContext(options))
      {
        _homeController = new HomeController(context);
      }
    }


    [Fact]
    public void HomeIndexViewIsReturned()
    {
      var result = _homeController.Privacy() as ViewResult;
      Console.WriteLine(result);
      Assert.Equal("Privacy", result.ViewName);
    }
  }
}

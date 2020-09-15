using System;
using System.IO;
using Xunit;

namespace RPS_Game_Refactored.Tests
{
  public class UnitTest1
  {
    [Fact]
    public void Test1()
    {
      // Arrange

      // Act
      Choice result = RpsGameMethods.GetRandomChoice();


      // Assert
      Assert.IsType<Choice>(result);

    }

    [Fact]
    public void GetUsersIntentReturnsOneOrTwo()
    {
      // Arrange
      // not needed

      // Act
      using (var sw = new StringWriter())
      {
        using (var sr = new StringReader("2"))
        {
          Console.SetOut(sw);
          Console.SetIn(sr);
          int intent = RpsGameMethods.GetUserIntent();
          Assert.Equal(2, intent);
        }
      }
    }

  }
}

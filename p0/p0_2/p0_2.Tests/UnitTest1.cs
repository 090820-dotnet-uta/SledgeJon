using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.IO;
using Xunit;

namespace p0_2.Tests
{
  public class UnitTest1
  {
    [Fact]
    public void GetStartMenuInputReturnsNum()
    {
      using (var sw = new StringWriter())
      {
        using (var sr = new StringReader("3"))
        {
          Console.SetOut(sw);
          Console.SetIn(sr);

          // Act statement ...
          HelperMethods helperMethods = new HelperMethods();
          int result = helperMethods.StartMenuInput();

          // Assert statement ...
          Assert.InRange(result, 1, 3);
        }
      }

    }
  }
}

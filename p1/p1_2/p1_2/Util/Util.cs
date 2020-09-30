using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace p1_2.Util
{
  public class Util
  {
    public static bool IsLoggedIn(IMemoryCache cache)
    {
      return false;
      //return (cache.Get("UserName") == null);
    }

  }
}

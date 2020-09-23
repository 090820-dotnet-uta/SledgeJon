using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RPSChallenge.Models;

namespace RPSChallenge.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private IMemoryCache _cache;//must set this for DI in Startup.cs
    List<Player> players;

    public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
    {
      _logger = logger;
      _cache = cache;

      //this happends on the first call to HomeController
      if (!_cache.TryGetValue("players", out players))
      {
        _cache.Set("players", new List<Player>());
        _cache.TryGetValue("players", out players);
      }
    }

    /// <summary>
    /// set the cache version of players to the now altered version.
    /// </summary>
    public void SaveChanges()
    {
      _cache.Set("players", players);
    }


    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// the route to the model is .../home/CreatePlayer
    /// </summary>
    /// <returns></returns>
    public IActionResult CreatePlayer()
    {
      return View();
    }

    public IActionResult PlayerList()
    {
      return View(players);
    }

    [HttpPost]
    public IActionResult CreatePlayer(Player player)
    {
      _logger.LogInformation("this is a test");//log for fun
      Player p1 = new Player()//create a player into which to transfer the user input and/or verify
      {
        Name = player.Name,
        PlayerId = player.PlayerId,
        Losses = player.Losses,
        Wins = player.Wins
      };
      players.Add(p1);//add tehe new player to the players List<>
      SaveChanges();//save the list to the cache for next call.
      Player p2 = players.FirstOrDefault();
      _logger.LogInformation($"The player info is {p2.Name} with {p2.Wins} wins and {p2.Losses} losses");
      return View("Index");
    }


  }
}

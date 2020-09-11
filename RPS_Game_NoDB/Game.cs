using System.Collections.Generic;

namespace RPS_Game_NoDB
{
  public class Game
  {
    public Player Player1 { get; set; }
    public Player Computer { get; set; }
    public List<Round> rounds = new List<Round>();
    public Player winner = new Player();

  }
}
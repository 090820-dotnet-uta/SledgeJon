using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPS_Game_Refactored
{
  public class Player
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlayerId { get; set; }
    public string Name { get; set; } = "null";
    public List<Game> games = new List<Game>();
    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;
  }
}
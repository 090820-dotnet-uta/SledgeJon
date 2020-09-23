using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPSChallenge.Models
{
  public class Player
  {
    public int PlayerId { get; set; }
    [Required]
    public string Name { get; set; } = "null";
    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;
  }
}

using RPS_Game_Refactored.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPS_Game_Refactored
{
  public static class RpsGameMethods
  {

    /// <summary>
    /// Get a choice from the user. 1 (play) or 2 (quit)
    /// </summary>
    /// <returns>
    /// int
    /// </returns>
    public static int GetUserIntent()
    {
      int choice;
      bool inputInt;
      do
      {
        System.Console.WriteLine("Please choose 1 for Play or 2 for Quit");
        string input = Console.ReadLine();
        inputInt = int.TryParse(input, out choice);
      } while (!inputInt || choice <= 0 || choice >= 3);

      return choice;
    }

    /// <summary>
    /// Gets the players name from the user and returns a string
    /// </summary>
    /// <returns>string</returns>
    public static string GetPlayerName()
    {
      System.Console.WriteLine("What is your name?");
      string playerName = Console.ReadLine();
      playerName = playerName.Trim();//take off beginning or ending white space
      return playerName;
    }

    /// <summary>
    /// checks the list of players to see if the player is returning. If not, creates a new player and adds him to the List<Player>
    /// </summary>
    /// <returns></returns>
    public static Player VerifyPlayer(List<Player> players, string playerName)
    {
      Player p1 = new Player();

      // check the list of players to see if this player is a returning player.
      foreach (Player item in players)
      {
        if (item.Name == playerName)
        {
          p1 = item;
          System.Console.WriteLine("You are a returning player.");
          break;//end the foreach loop
        }
      }

      if (p1.Name == "null")//means the players name was not found above
      {
        p1.Name = playerName;
        players.Add(p1);
      }
      return p1;
    }

    /// <summary>
    /// Returns a random Choice of Rock,Paper, Scissors
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    public static Choice GetRandomChoice()
    {
      Random rand = new Random();
      return (Choice)rand.Next(3);
    }

    /// <summary>
    /// Returns a player Choice of Rock,Paper, Scissors
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    public static Choice GetPlayerChoice()
    {
      bool playerGameChoiceBool;
      int playerGameChoiceInt;
      do//prompt loop
      {
        Console.WriteLine("Which do you choose? 0)Rock 1)Paper 2)Scissors");
        string playerGameChoice = Console.ReadLine();
        playerGameChoiceBool = int.TryParse(playerGameChoice, out playerGameChoiceInt);
      } while (!playerGameChoiceBool || playerGameChoiceInt < 0 || playerGameChoiceInt > 2);//end of promt loop
      return (Choice)playerGameChoiceInt;
    }

    /// <summary>
    /// Gets the winner of the round
    /// </summary>
    /// <param name="round"></param>
    public static void GetRoundWinner(Round round)
    {
      // DIctionary for outputting Player and Computer choices
      Dictionary<int, string> RPS_Choices = new Dictionary<int, string>()
        {
            {0, "Rock"},
            {1, "Paper"},
            {2, "Scissors"}
        };

      if (round.p1Choice == round.ComputerChoice)
      {
        round.Outcome = 0; // it’s a tie . the default is 0 so this line is unnecessary.
        Console.WriteLine($"\tPlayer 1 chose {RPS_Choices[(int)round.p1Choice]}");
        Console.WriteLine($"\tComputer chose {RPS_Choices[(int)round.ComputerChoice]}");
        Console.WriteLine("\tthis round was a tie");
      }
      else if ((int)round.p1Choice == ((int)round.ComputerChoice + 1) % 3)
      { //If users pick is one more than the computer’s, user wins
        round.Outcome = 1;
        Console.WriteLine($"\tPlayer 1 chose {RPS_Choices[(int)round.p1Choice]}");
        Console.WriteLine($"\tComputer chose {RPS_Choices[(int)round.ComputerChoice]}");
        Console.WriteLine("\tThe user has won");
      }
      else
      { //If it’s not a tie and p1 didn’t win, then computer wins.
        round.Outcome = 2;
        Console.WriteLine($"\tPlayer 1 chose {RPS_Choices[(int)round.p1Choice]}");
        Console.WriteLine($"\tComputer chose {RPS_Choices[(int)round.ComputerChoice]}");
        Console.WriteLine("\tThe computer has won");
      }
    }

    /// <summary>
    /// Detemine who won each game
    /// </summary>
    /// <param name="game"></param>
    /// <param name="p1"></param>
    /// <param name="computer"></param>
    /// <param name="whoWon"></param>
    public static void WhoWon(Game game, Player p1, Player computer, int whoWon)
    {
      if (whoWon == 1)
      {
        game.winner = p1;
        p1.Wins++;//increments wins and losses.
        computer.Losses++;//increments wins and losses.
        Console.WriteLine($"The winner of this game was Player1\n");
      }
      else if (whoWon == 2)
      {
        game.winner = computer;
        p1.Losses++;//increments wins and losses.
        computer.Wins++;//increments wins and losses.
        Console.WriteLine($"The winner of this game was computer\n");
      }
    }

    /// <summary>
    /// determines if either player has won 2 rounds yet.
    /// </summary>
    /// <param name="game"></param>
    /// <returns>int</returns>
    public static int GetWinner(Game game)
    {
      int numP1Wins = game.rounds.Count(x => x.Outcome == 1);//get nhow many rounds p1 has won.
      int numComputerWins = game.rounds.Count(x => x.Outcome == 2);//get nhow many rounds p1 has won.

      Console.WriteLine($"\tPlayer1 wins => {numP1Wins} \n\tComputer wins => {numComputerWins}\n");
      // get how many rounds p1 has won.
      if (game.rounds.Count(x => x.Outcome == 1) == 2) { return 1; }
      else if (game.rounds.Count(x => x.Outcome == 2) == 2) { return 2; }// get how many rounds computer has won.
      else return 0;
    }

    /// <summary>
    /// This method prints the results of the game after it is over.
    /// </summary>
    /// <param name="games"></param>
    /// <param name="players"></param>
    /// <param name="rounds"></param>
    public static void PrintAllCurrentData(List<Game> games, List<Player> players, List<Round> rounds)
    {
      Dictionary<int, string> RPS_Outcomes = new Dictionary<int, string>()
        {
            {0, "Tie"},
            {1, "Player Wins"},
            {2, "Computer Wins"}
        };
      Console.WriteLine("Game record\n");

      // Game counter for outputting which game number's information is being displayed
      int gameCount = 1;
      foreach (var game in games)
      {
        // Console.WriteLine($"Player1 Name => {game.Player1.Name}\ncomputer Name => {game.Computer.Name}\nThe winner is => {game.winner.Name}");
        Console.WriteLine($" --- Here are the games rounds --- \n");
        Console.WriteLine($"Game number {gameCount}");
        foreach (Round round in game.rounds)
        {
          Console.WriteLine($"player1 => {round.player1.Name}, Player 1 choice => {round.p1Choice}");
          Console.WriteLine($"player2 => {round.Computer.Name}, Computer choice => {round.ComputerChoice}");
          Console.WriteLine($"the Outcome of this round is =>{RPS_Outcomes[round.Outcome]}\n");
        }
        gameCount++;
        // Output the winner of each game
        Console.WriteLine($"The winner of this game was {game.winner.Name}");
      }
      Console.WriteLine("Here is the list of players.\n");
      foreach (var player in players)
      {
        Console.WriteLine($"This players nasme is {player.Name} and he has {player.Wins} wins and {player.Losses} losses\n");
      }
    }


  }
}
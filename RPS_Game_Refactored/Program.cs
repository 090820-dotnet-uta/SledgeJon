using System;
using System.Collections.Generic;

namespace RPS_Game_Refactored
{
  public enum Choice
  {
    Rock,//can be accessed with a 0
    Paper,//can be accessed with a 1
    Scissors//can be accessed with a 2
  }
  class Program
  {
    static void Main(string[] args)
    {
      List<Player> players = new List<Player>();
      List<Game> games = new List<Game>();
      List<Round> rounds = new List<Round>();
      int choice;
      Player computer = new Player() { Name = "Computer" };//instantiate a Player and give a value to the Name all at once.
      players.Add(computer);
      int gameCounter = 1;

      do//game loop
      {
        choice = RpsGameMethods.GetUserIntent();
        if (choice == 2) break;

        Console.WriteLine($"\n\tThis is game #{gameCounter++}\n");

        //get the player name
        string playerName = RpsGameMethods.GetPlayerName();

        // check the list of players to see if this payer is a returning player.
        Player p1 = RpsGameMethods.VerifyPlayer(players, playerName);

        Game game = new Game();// create a game
        game.Player1 = p1;//
        game.Computer = computer;//

        Random rand = new Random();

        //play rounds till one player has 2 wins
        //assign the winner to the game and check that property to break out of the loop.
        while (game.winner.Name == "null")
        {
          Round round = new Round();//declare a round for this iteration
          round.game = game;// add the game to this round
          round.player1 = p1;// add user (p1) to this round
          round.Computer = computer;// add computer to this round

          // Player choice for game
          round.p1Choice = RpsGameMethods.GetPlayerChoice();
          // Random player choice for game 
          round.ComputerChoice = RpsGameMethods.GetRandomChoice();

          //check the choices to see who won.
          RpsGameMethods.GetRoundWinner(round);

          game.rounds.Add(round);//add this round to the games List of rounds

          //search the game.rounds List<> to see if one player has 2 wins
          //if not loop to another round
          int whoWon = RpsGameMethods.GetWinner(game);

          //assign the winner to the game and increment wins and losses for both
          RpsGameMethods.WhoWon(game, p1, computer, whoWon);
        }//end of rounds loop

        games.Add(game);
      } while (choice != 2);//end of game loop

      // Print current game data
      RpsGameMethods.PrintAllCurrentData(games, players, rounds);

    }//end of main

  }//end of program
}//end of namaespace

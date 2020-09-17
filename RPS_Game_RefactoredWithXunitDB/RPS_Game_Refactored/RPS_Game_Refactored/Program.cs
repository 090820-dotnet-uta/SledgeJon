using Microsoft.EntityFrameworkCore.Query.Internal;
using RPS_Game_Refactored.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

      using (var context = new DbContextClass())
      {
        int choice;
        Player computer = new Player() { Name = "Computer" };//instantiate a Player and give a value to the Name all at once.
        context.Players.Add(computer);
        int gameCounter = 1;

        do//game loop
        {
          choice = RpsGameMethods.GetUserIntent();
          if (choice == 2) break;

          Console.WriteLine($"\n\tThis is game #{gameCounter++}\n");

          //get the player name
          string playerName = RpsGameMethods.GetPlayerName();

          // check the list of players to see if this payer is a returning player.
          Player p1 = RpsGameMethods.VerifyPlayer(context.Players.ToList(), playerName);

          Game game = new Game();// create a game
          game.Player1 = p1;//
          game.Computer = computer;//
          //context.Add(p1);
          //context.SaveChanges();

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
            context.Rounds.Add(round);

            //search the game.rounds List<> to see if one player has 2 wins
            //if not loop to another round
            int whoWon = RpsGameMethods.GetWinner(game);

            //assign the winner to the game and increment wins and losses for both
            //RpsGameMethods.WhoWon(game, p1, computer, whoWon);
            if (whoWon == 1)
            {
              game.winner = p1;
              p1.Wins++;//increments wins and losses.
              computer.Losses++;//increments wins and losses.
              Console.WriteLine($"The winner of this game was Player1\n");
              context.Add(round);
              //context.SaveChanges();
            }
            else if (whoWon == 2)
            {
              game.winner = computer;
              p1.Losses++;//increments wins and losses.
              computer.Wins++;//increments wins and losses.
              Console.WriteLine($"The winner of this game was computer\n");
              context.Add(round);
              //context.SaveChanges();
            }

          }//end of rounds loop

          context.Games.Add(game);
          context.SaveChanges();
        } while (choice != 2);//end of game loop
        // Print current game data
        RpsGameMethods.PrintAllCurrentData(context.Games.ToList(), context.Players.ToList(), context.Rounds.ToList());

      }

    }//end of main

  }//end of program
}//end of namaespace

using System;

namespace _4_MethodsChallenge
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //1
      string name = GetName();
      GreetFriend(name);

      //2
      double result1 = GetNumber();
      double result2 = GetNumber();
      int action1 = GetAction();
      double result3 = DoAction(result1, result2, action1);

      System.Console.WriteLine($"The result of your mathematical operation is {result3}.");


    }

    public static string GetName()
    {
      Console.WriteLine("Enter your name");
      string name = Console.ReadLine();
      return name;
      throw new NotImplementedException();
    }

    public static void GreetFriend(string name)
    {
      //Greeting should be: Hello, nameVar. You are my friend
      //Ex: Hello, Jim. You are my friend
      Console.WriteLine($"Hello, {name}. You are my friend");

      // this exception is being thrown for some reason
      // throw new NotImplementedException();
    }

    public static double GetNumber()
    {
      //Should throw FormatException if the user did not input a number
      Console.WriteLine("Enter a double");
      double num;
      string number = Console.ReadLine();
      if (!double.TryParse(number, out num))
      {
        throw new FormatException();
      }
      else return num;
      throw new NotImplementedException();
    }

    public static int GetAction()
    {
      Console.WriteLine("Choose an arithmetic operation 1)add, 2)subtract, 3)multiply, or 4)divide");
      string input = Console.ReadLine();
      int num;
      int.TryParse(input, out num);

      while (num < 1 || num > 4)
      {
        Console.WriteLine("Enter a num between 1 and 4");
        input = Console.ReadLine();
        int.TryParse(input, out num);
      }

      Console.WriteLine($"got the input {num}");
      return num;

      throw new NotImplementedException();
    }

    public static double DoAction(double x, double y, int z)
    {
      if (z == 1)
      {
        return y + x;
      }
      else if (z == 2)
      {
        return y - x;
      }
      else if (z == 3)
      {
        return y * x;
      }
      else if (z == 4)
      {
        return y / x;
      }
      else throw new FormatException();

      throw new NotImplementedException();
    }
  }
}

using System;

namespace HelloWorldDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello Jon!");
      Console.WriteLine("What's your name?");
      string input = Console.ReadLine();
      Console.WriteLine($"Hello {input}");
      Console.WriteLine("Hello {0}", input);
    }
  }
}

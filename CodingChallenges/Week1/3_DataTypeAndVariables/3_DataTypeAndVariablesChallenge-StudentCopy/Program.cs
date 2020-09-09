using System;

namespace _3_DataTypeAndVariablesChallenge
{
  public class Program
  {
    public static void Main(string[] args)
    {
      byte myByte = 21;
      Console.WriteLine(myByte);

      sbyte mySByte = -128;
      Console.WriteLine(mySByte);

      int myInt = -1147483648;
      Console.WriteLine(myInt);

      uint myUint = 123091;
      Console.WriteLine(myUint);

      short myShort = -328;
      Console.WriteLine(myShort);

      ushort myUShort = 6553;
      Console.WriteLine(myUShort);

      double myDouble = -11.123120;
      Console.WriteLine(myDouble);

      char myCharacter = 'R';
      Console.WriteLine(myCharacter);

      bool myBool = false;
      Console.WriteLine(myBool);

      string myText = "I control text";
      Console.WriteLine(myText);

      string numText = "2";
      Console.WriteLine(Text2Num(numText));
    }

    public static int Text2Num(string numText)
    {
      int temp;
      int.TryParse(numText, out temp);
      return temp;
      throw new NotImplementedException();
    }
  }
}

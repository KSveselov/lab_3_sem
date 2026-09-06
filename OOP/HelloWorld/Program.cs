using System;

namespace HelloWorld
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine(Decode("1..02.6"));
            Console.WriteLine(Decode("1010"));
        }

        static int Decode(string arg)
        {
            return int.Parse(arg.Replace(".", ""))%1024;
        }
    }
}
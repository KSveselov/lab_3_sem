using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetLastHalf("I love CSharp!"));
            Console.WriteLine(GetLastHalf("1234567890"));
            Console.WriteLine(GetLastHalf("до ре ми фа соль ля си"));
        }

        static string GetLastHalf(string text)
        {   
            int start = text.Length/2;
            int end = text.Length;

            string second_part = text.Substring(start,end-start);
            return second_part.Replace(" ", "");

        }
    }
}
using System;
using System.Globalization;

namespace HelloWorld
{
    class Program
    {
        public static void Main()
        {
            String inform = Console.ReadLine()!;
            

            Console.WriteLine(Calculate(inform));
        }

        public static double Calculate(String inform)
        {   
            String[] informArray = inform.Split(' ');
            double deposit = double.Parse(informArray[0].Trim(), CultureInfo.InvariantCulture);
            double rate = double.Parse(informArray[1].Trim(), CultureInfo.InvariantCulture);
            int duration = int.Parse(informArray[2].Trim());

            double result = deposit * Math.Pow((1 + rate/1200.0), duration);
            return result;
        }
    }
}
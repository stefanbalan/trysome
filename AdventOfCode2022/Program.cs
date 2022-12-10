using System;

namespace AdventOfCode2022
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Day day = new Day2();
            day.Execute();

            Console.WriteLine("Result1 {0}", day.Result1);
            Console.WriteLine("Result2 {0}", day.Result2);
            Console.ReadKey();
        }
    }
}

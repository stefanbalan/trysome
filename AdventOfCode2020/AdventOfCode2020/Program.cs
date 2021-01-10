using System;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = new Day4();
            var result = day.Compute1();
            Console.WriteLine(result);

            result = day.Compute2();
            Console.WriteLine(result);
        }
    }
}

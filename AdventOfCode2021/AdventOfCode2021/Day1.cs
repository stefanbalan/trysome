namespace AdventOfCode2021
{
    internal partial class Day1
    {
        public static int Compute1()
        {
            var count = 0;
            var current = input[0];
            short next = 0;

            //for (int i = 1; i < input.Length; i += 1)
            //    if (input[i] > input[i-1])
            //        count += 1;

            for (int i = 1; i < input.Length; i += 1)
            {
                next = input[i];
                if (next > current)
                    count += 1;
                current = next;
            }
            return count;
        }

        public static int Compute2()
        {
            var count = 0;
            int current = input[0] + input[1] + input[2];
            int next = 0;

            for (int i = 1; i < input.Length - 2; i += 1)
            {
                next = input[i] + input[i + 1] + input[i + 2];
                if (next > current)
                    count += 1;
                current = next;
            }
            return count;
        }
    }
}

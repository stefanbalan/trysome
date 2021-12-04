namespace AdventOfCode2021
{
    internal partial class Day2
    {
        public int Compute1()
        {
            var pos = 0;
            var depth = 0;

            var reader = new StringReader(input);
            string? commandString = null;

            while (!((commandString = reader.ReadLine()) is null))
            {
                var command = commandString.Split(' ');
                if (!int.TryParse(command[1], out var param)) return -1;

                switch (command[0])
                {
                    case "forward":
                        pos += param;
                        break;
                    case "up":
                        depth -= param;
                        break;
                    case "down":
                        depth += param;
                        break;
                }

            }

            return pos * depth;
        }

        public int Compute2()
        {

            var pos = 0;
            var depth = 0;
            var aim = 0;
            var reader = new StringReader(input);
            string? commandString = null;

            while (!((commandString = reader.ReadLine()) is null))
            {
                var command = commandString.Split(' ');
                if (!int.TryParse(command[1], out var param)) return -1;

                switch (command[0])
                {
                    case "forward":
                        pos += param;
                        depth += param * aim;
                        break;
                    case "up":
                        aim -= param;
                        break;
                    case "down":
                        aim += param;
                        break;
                }

            }

            return pos * depth;
        }
    }
}

using AdventOfCode2022;

namespace AdventOfCode2022
{
    public abstract class Day
    {
        private readonly InputReader InputReader;

        protected Day()
        {
            InputReader = new InputReader(GetType().Name);
        }

        protected abstract void LineAction(Token line);
        public int Result1 { get; protected set; } = 0;
        public int Result2 { get; protected set; } = 0;

        public virtual void Execute()
        {
            foreach (Token line in InputReader)
                LineAction(line);
        }
    }
}

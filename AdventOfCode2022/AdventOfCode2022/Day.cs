namespace AdventOfCode2022
{
    public abstract class Day
    {
        protected readonly InputReader InputReader;

        protected Day()
        {
            InputReader = new InputReader(GetType().Name);
        }

        protected abstract void LineAction(InputTextLine line);
        public int Result1 { get; protected set; } = 0;
        public int Result2 { get; protected set; } = 0;

        public virtual void Execute()
        {
            foreach (InputTextLine line in InputReader)
                LineAction(line);
        }
    }
}

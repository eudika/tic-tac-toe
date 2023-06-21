namespace TicTacToe
{
    internal class Board : IReadOnlyBoard
    {
        private static readonly int[][] Lines = new[]
        {
            // horizontal
            new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 },
            // vertical
            new[] { 0, 3, 6 }, new[] { 1, 4, 7 }, new[] { 2, 5, 8 },
            // diagonal
            new[] { 0, 4, 8 }, new[] { 2, 4, 6 },
        };

        private readonly CellMark[] marks;
        private readonly Stack<int> history;

        public CellMark NextMark { get; private set; }

        public bool IsOver => IsFull
            || IsOnesVictory(CellMark.Circle) || IsOnesVictory(CellMark.Cross);

        public bool IsFull => history.Count >= 9;

        public Board()
        {
            marks = new CellMark[9];
            history = new Stack<int>();
            NextMark = CellMark.Circle;
        }

        public CellMark GetMarkAt(int index)
        {
            return marks[index];
        }

        public IEnumerable<int> GetBlankIndices()
        {
            return Enumerable.Range(0, 9).Where(i => marks[i].IsBlank());
        }

        public void PutAt(int index)
        {
            if (index < 0 || index > 9) throw new ArgumentException($"Index out of bounds: index={index}");
            if (!marks[index].IsBlank()) throw new InvalidOperationException();

            marks[index] = NextMark;
            history.Push(index);
            NextMark = NextMark.Oppose();
        }

        public void Undo()
        {
            if (history.Count == 0) throw new InvalidOperationException();

            var index = history.Pop();
            marks[index] = CellMark.Blank;
            NextMark = NextMark.Oppose();
        }

        public bool IsOnesVictory(CellMark mark)
        {
            return Lines.Any(line => line.All(index => marks[index] == mark));
        }

        public Board Clone()
        {
            var clone = new Board();
            for (var i = 0; i < marks.Length; i++)
            {
                clone.marks[i] = marks[i];
            }
            foreach (var index in history)
            {
                clone.history.Push(index);
            }
            clone.NextMark = NextMark;
            return clone;
        }
    }
}

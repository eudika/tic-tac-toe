namespace TicTacToe
{
    internal class AIPlayer : IPlayer
    {
        public string Name { get; }

        public AIPlayer(string name)
        {
            Name = name;
        }

        public int MakeDecision(IReadOnlyBoard board)
        {
            return SearchOptimalChoice(board.Clone(), board.NextMark).Index;
        }

        private (int Index, int Score) SearchOptimalChoice(Board board, CellMark mark)
        {
            var maxScore = -1;
            var maxIndex = 0;
            foreach (var index in board.GetBlankIndices())
            {
                board.PutAt(index);
                var score = Evaluate(board, mark);
                if (score > maxScore)
                {
                    maxScore = score;
                    maxIndex = index;
                }
                board.Undo();
            }
            return (maxIndex, maxScore);
        }

        private int Evaluate(Board board, CellMark myMark)
        {
            if (board.IsOnesVictory(myMark)) return +1;
            if (board.IsOnesVictory(myMark.Oppose())) return -1;
            if (board.IsFull) return 0;

            // my score = opponent's score * -1
            return -SearchOptimalChoice(board, myMark.Oppose()).Score;
        }
    }
}

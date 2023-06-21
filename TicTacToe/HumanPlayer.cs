namespace TicTacToe
{
    internal class HumanPlayer : IPlayer
    {
        public string Name { get; }

        public HumanPlayer(string name)
        {
            Name = name;
        }

        public int MakeDecision(IReadOnlyBoard board)
        {
            var candidates = board.GetBlankIndices().ToHashSet();
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out var index) && candidates.Contains(index))
                {
                    return index;
                }
            }
        }
    }
}

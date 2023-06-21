using System.Text;

namespace TicTacToe
{
    internal class Game
    {
        private readonly Board board;
        private readonly Dictionary<CellMark, IPlayer> players;
        private IPlayer NextPlayer => players[board.NextMark];

        public Game()
        {
            board = new Board();
            players = new Dictionary<CellMark, IPlayer>();
        }

        public void RegisterPlayer(CellMark mark, IPlayer player)
        {
            if (mark.IsBlank()) throw new InvalidOperationException();
            players[mark] = player;
        }

        public void Start()
        {
            if (players.Count < 2) throw new InvalidOperationException();

            while (!board.IsOver)
            {
                PrintBoard();
                var index = NextPlayer.MakeDecision(board);
                PrintDecision(index);
                board.PutAt(index);
            }
            PrintBoard();
        }

        private static string MarkToString(CellMark mark)
        {
            return mark switch
            {
                CellMark.Circle => "o",
                CellMark.Cross => "x",
                _ => " ",
            };
        }

        private void PrintBoard()
        {
            PrintCells();
            if (board.IsOver)
            {
                PrintResult();
            }
            else
            {
                PrintTurn();
            }
        }

        private void PrintTurn()
        {
            Console.WriteLine($"{NextPlayer.Name}'s turn ({MarkToString(board.NextMark)})");
        }

        private void PrintCells()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 9; i++)
            {
                var mark = board.GetMarkAt(i);
                sb.Append(mark.IsBlank() ? "" + i : MarkToString(mark));

                if (i == 2 || i == 5)
                {
                    sb.AppendLine();
                    sb.AppendLine("-----");
                }
                else if (i != 8)
                {
                    sb.Append('|');
                }
            }
            Console.WriteLine(sb.ToString());
        }

        private void PrintResult()
        {
            var winnerMark = new[] { CellMark.Circle, CellMark.Cross }
                .FirstOrDefault(board.IsOnesVictory);

            if (winnerMark == default)
            {
                Console.WriteLine("DRAW");
            }
            else
            {
                Console.WriteLine($"{players[winnerMark].Name}'s WIN");
            }
        }

        private void PrintDecision(int index)
        {
            Console.WriteLine($"{NextPlayer.Name} chose {index}");
            Console.WriteLine();
        }
    }
}

namespace TicTacToe
{
    internal interface IReadOnlyBoard
    {
        CellMark NextMark { get; }
        bool IsOver { get; }
        bool IsFull { get; }

        bool IsOnesVictory(CellMark mark);
        CellMark GetMarkAt(int index);
        IEnumerable<int> GetBlankIndices();

        Board Clone();
    }
}
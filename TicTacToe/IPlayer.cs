namespace TicTacToe
{
    internal interface IPlayer
    {
        string Name { get; }
        int MakeDecision(IReadOnlyBoard board);
    }
}

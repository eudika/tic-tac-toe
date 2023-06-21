namespace TicTacToe
{
    internal enum CellMark
    {
        Blank = 0,
        Circle,
        Cross,
    }

    internal static class CellMarkExtensions
    {
        internal static bool IsBlank(this CellMark mark)
        {
            return mark == CellMark.Blank;
        }

        internal static CellMark Oppose(this CellMark mark)
        {
            return mark switch
            {
                CellMark.Circle => CellMark.Cross,
                CellMark.Cross => CellMark.Circle,
                _ => throw new NotImplementedException(),
            };
        }
    }
}

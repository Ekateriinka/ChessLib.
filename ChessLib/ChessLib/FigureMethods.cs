namespace ChessLib
{
    internal static class FigureMethods
    {
        public static Color GetColor(this Figure figure) =>
                (figure == Figure.none)
                ? Color.none
                : (figure == Figure.whiteKing
                    || figure == Figure.whiteQueen
                    || figure == Figure.whiteBishop
                    || figure == Figure.whiteKnight
                    || figure == Figure.whiteRook
                    || figure == Figure.whitePawn) ? Color.white : Color.black;
    }
}
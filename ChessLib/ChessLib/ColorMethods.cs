namespace ChessLib
{
    internal static class ColorMethods
    {
        public static Color FlipColor(this Color color)
        {
            if (color == Color.black)
            {
                return Color.white;
            }
            else if (color == Color.white)
            {
                return Color.black;
            }
            return Color.none;
        }
    }
}

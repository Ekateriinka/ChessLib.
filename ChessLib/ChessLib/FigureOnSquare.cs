namespace ChessLib
{
    internal class FigureOnSquare
    {
        public Figure Figure { get; }
        public Square Square { get; private set; }

        public FigureOnSquare(Figure figure,Square square)
        {
            this.Figure = figure;
            this.Square = square;
        }
    }
}

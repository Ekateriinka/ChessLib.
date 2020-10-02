using System;

namespace ChessLib
{
    internal class FigureMoving
    {
        public Figure Figure { get; }
        public Square From { get; private set; }
        public Square To { get; private set; }
        public Figure Promotion { get; }

        public FigureMoving(FigureOnSquare fs, Square to, Figure promotion = Figure.none)
        {
            Figure = fs.Figure;
            From = fs.Square;
            To = to;
            Promotion = promotion;
        }
        public FigureMoving(string move)
        {
            Figure = (Figure)move[0];
            From = new Square(move.Substring(1, 2));
            To = new Square(move.Substring(3, 2));
            Promotion = (move.Length == 6) ? (Figure)move[5] : Figure.none;
        }
        public int DeltaX => To.X - From.X;
        public int DeltaY => To.Y - From.Y;

        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);

        public int SignX => Math.Sign(DeltaX);
        public int SignY => Math.Sign(DeltaY);

        public override string ToString()
        {
            string text = (char)Figure + From.Name + To.Name;
            if (Promotion == Figure.none)
                text += (char)Promotion;
            return text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLib
{
    internal class Board
    {
        public string Fen { get; private set; }
        private readonly Figure[,] figures;
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public Board(string fen)
        {
            this.Fen = fen ?? throw new System.ArgumentNullException(nameof(fen));
            figures = new Figure[8, 8];
            Init();
        }
        private void Init()
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

            string[] parts = Fen.Split();
            if (parts.Length != 6) return;
            InitFigures(parts[0]);
            MoveColor = (parts[1] == "b") ? Color.black : Color.white;
            MoveNumber = int.Parse(parts[5]);
        }

        private void InitFigures(string data)
        {
            for(int j = 8; j>=2;j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for(int y = 7; y>=0;y--)
            {
                for(int x= 0;x<8;x++)
                {
                    figures[x, y] = lines[7-y][x]=='.' ? Figure.none :
                            (Figure)lines[7 - y][x];
                }
            }
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == MoveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }
        }

        public Figure GetFigureAt(Square square) => (square.OnBoard) ? figures[square.X, square.Y] : Figure.none;

        private void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard)
            {
                figures[square.X, square.Y] = figure;
            }
        }

        private void GenerateFen() => Fen = $"{FenFigures()} {(MoveColor == Color.white ? "w" : "b")} - - 0 {MoveNumber}";

        private string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                }

                if (y > 0)
                {
                    sb.Append("/");
                }
            }
            const string eight = "11111111";
            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }
            return sb.ToString();
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(Fen);
            next.SetFigureAt(fm.From, Figure.none);
            next.SetFigureAt(fm.To,fm.Promotion == Figure.none ? fm.Figure : fm.Promotion);
            if (MoveColor == Color.black)
            {
                next.MoveNumber++;
            }
            next.MoveColor = MoveColor.FlipColor();
            next.GenerateFen();
            return next;
        }
    }
}

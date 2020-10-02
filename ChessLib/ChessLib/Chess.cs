using System.Collections.Generic;

namespace ChessLib
{
    public class Chess
    {
        private readonly Board board;
        public string Fen { get; }
        private readonly Move moves;
        List<FigureMoving> allMoves;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.Fen = fen;
            board = new Board(fen);
            moves = new Move(board);
        }
        private Chess(Board board)
        {
            this.board = board;
            this.Fen = board.Fen;
            moves = new Move(board);
        }
        public Chess Move(string move)
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return this;
            Board nextBoard = board.Move(fm);
            return new Chess(nextBoard);
        }
        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);
            if (f == Figure.none)
            {
                return '.';
            }
            else
            {
                return (char)f;
            }
        }

        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach(FigureOnSquare fs in board.YieldFigures())
                foreach(Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if(moves.CanMove(fm))
                    {
                        allMoves.Add(fm);
                    }
                }
        }
        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach (FigureMoving fm in allMoves)
            {
                list.Add(fm.ToString());
            }
            return list;
        }
    }
}

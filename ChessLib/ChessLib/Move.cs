using System;

namespace ChessLib
{
    internal class Move
    {
        private FigureMoving fm;
        private readonly Board board;
        public Move(Board board) => this.board = board;
        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;
            return
                CanMoveFrom&&
                CanMoveTo() &&
                CanFigureMove();
        }
        private bool CanMoveFrom => fm.From.OnBoard && fm.Figure.GetColor() == board.MoveColor;
        private bool CanMoveTo() => fm.To.OnBoard && fm.From != fm.To && board.GetFigureAt(fm.To).GetColor() != board.MoveColor;
        private bool CanFigureMove()
        {
            switch (fm.Figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove;

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();

                case Figure.whiteRook:
                case Figure.blackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                            CanStraightMove();

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) &&
                            CanStraightMove();

                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove;

                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();

                default: return false;
            }
        }

        private bool CanPawnMove()
        {
            if(fm.From.Y<1||fm.From.Y>6)
            {
                return false;
            }
            int stepY = fm.Figure.GetColor() == Color.white ? 1 : -1;
            return
                CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
        {
            if(board.GetFigureAt(fm.To) != Figure.none)
            {
                if(fm.AbsDeltaX == 1)
                {
                    if(fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(fm.To) == Figure.none)
            {
                if (fm.DeltaX == 0)
                {
                    if(fm.DeltaY == 2*stepY)
                    {
                        if(fm.From.Y == 1 || fm.From.Y == 6)
                        {
                            if(board.GetFigureAt(new Square(fm.From.X, fm.From.Y + stepY))==Figure.none)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if(board.GetFigureAt(fm.To)== Figure.none)
            {
                    if (fm.DeltaX == 0)
                    {
                        if(fm.DeltaY == stepY)
                        {
                            return true;
                        }
                    }
            }
            return false;
        }

        private bool CanStraightMove()
        {
            for (Square at = fm.From; at.OnBoard && board.GetFigureAt(at) == Figure.none;)
            {
                at = new Square(at.X + fm.SignX, at.Y + fm.SignY);
                if (at == fm.To)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CanKingMove => fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1;

        private bool CanKnightMove => (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2) || (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1);
    }
}

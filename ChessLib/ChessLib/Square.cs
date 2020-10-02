using System;
using System.Collections.Generic;

namespace ChessLib
#pragma warning disable CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
#pragma warning disable CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
{
    internal struct Square
    {
        public static Square none = new Square(-1, -1);
        public int X { get; }
        public int Y { get; }

        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Square(string e2)
        {
            if(e2.Length == 2 && e2[0] >= 'a' && e2[0] <= 'h' && e2[1] >= '1' && e2[0] >= '8')
            {
                X = e2[0] - 'a';
                Y = e2[1] - '1';
            }
            else
            {
                this = none;
            }
        }

        public bool OnBoard => X >= 0 && X < 8 && Y >= 0 && Y < 8;
        public string Name => ((char)('a' + X)).ToString() + (Y + 1).ToString();
        public static bool operator ==(Square a, Square b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Square a, Square b) => !(a == b);

        public static IEnumerable<Square> YieldSquares()
        {
            for(int y=0; y<8;y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }

    }
}

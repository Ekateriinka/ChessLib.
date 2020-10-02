using System;
using ChessLib;

namespace ChessTest
{
    internal static class Program
    {
        private static void Main()
        {
            Chess chess = new Chess();
            while (true)
            {
                Console.WriteLine(chess.Fen);
                Console.WriteLine(ChessToAscii(chess));
                foreach(string moves in chess.GetAllMoves())
                {
                    Console.Write(moves + "\n");
                }
                Console.Write("> ");
                string move = Console.ReadLine();
                if (move?.Length == 0)
                {
                    break;
                }

                chess = chess.Move(move);
            }
        }

        private static string ChessToAscii(Chess chess)
        {
            string text = "  +-----------------+\n";
            for(int y = 8; y>0; y--)
            {
                text += y;
                text += " | ";
                for(int x =0;x<8;x++)
                {
                    text += chess.GetFigureAt(x, y-1) + " ";
                }
                text += "|\n";
            }
            text+= "  +-----------------+\n";
            text += "    a b c d e f g h\n";
            return text;
        }
    }
}

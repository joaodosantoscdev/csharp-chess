using Board;
using System;
using csharp_chess.Board;

namespace csharp_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard brd = new ChessBoard(8, 8);

            Screen.PrintBoard(brd);

            Console.ReadLine();
        }
    }
}
